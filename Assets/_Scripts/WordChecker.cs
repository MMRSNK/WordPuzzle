using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public class WordChecker : MonoBehaviour
{
    public event Action OnLevelCleared;
    public event Action OnLetterAdded;
    public event Action<bool> OnWordChecked;

    [SerializeField]
    private CurrentWordUI _currentWordUI;

    private StringBuilder _sb;
    private Dictionary<string, List<System.Numerics.Vector2>> _levelWordsDict;
    private Dictionary<string, bool> _levelProgressDict;

    private void Start()
    {
        _sb = new StringBuilder();
        _currentWordUI.Reset();
    }
    public void AddLetter(string letter)
    {
        if (_sb.Length == 0)
            _currentWordUI.Reset();

        _sb.Append(letter);
        _currentWordUI.UpdateText(_sb.ToString());
        OnLetterAdded?.Invoke();
    }
    public void RemoveLastLetter()
    {
        _sb.Length--;
        _currentWordUI.UpdateText(_sb.ToString());
    }
    public void CheckWord()
    {
        bool correct = HasWord(_sb.ToString());

        _currentWordUI.WordChecked(correct);
        _sb.Clear();

        if (correct)
        {
            CheckLevelProgress();
        }
        OnWordChecked?.Invoke(correct);
    }
    private void CheckLevelProgress()
    {
        bool cleared = true;
        foreach (var kvp in _levelProgressDict)
        {
            if (!kvp.Value)
            {
                cleared = false;
            }
        }
        if (cleared)
        {
            OnLevelCleared?.Invoke();
        }
    }
    public void LoadWords(Dictionary<string, List<System.Numerics.Vector2>> levelWordsDict)
    {
        _levelWordsDict = new();

        foreach (var kvp in levelWordsDict)
        {
            _levelWordsDict[kvp.Key.ToUpper()] = kvp.Value;
        }

        if (_levelProgressDict == null)
            _levelProgressDict = new Dictionary<string, bool>();
        else
            _levelProgressDict.Clear();

        foreach (string key in levelWordsDict.Keys)
        {
            _levelProgressDict.Add(key.ToUpper(), false);
        }
    }
    public bool HasWord(string checkWord)
    {
        foreach (string word in _levelWordsDict.Keys)
        {
            if (word.ToUpper().Equals(checkWord.ToUpper()))
            {
                LevelController.Instance.OpenCellList(_levelWordsDict[word]);
                _levelProgressDict[word.ToUpper()] = true;
                return true;
            }
        }
        return false;
    }
    public void GetHint()
    {
        List<string> closedWords = new List<string>();

        foreach(var word in _levelProgressDict.Keys)
        {
            if (_levelProgressDict[word] == false)
                closedWords.Add(word.ToUpper());
        }
        if (closedWords.Count == 0)
            return;

        int random = UnityEngine.Random.Range(0, closedWords.Count);
        string randomWord = closedWords[random];

        LevelController.Instance.OpenCell(_levelWordsDict[randomWord][0]);
    }
}
