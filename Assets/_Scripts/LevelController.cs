using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

public class LevelController : Singleton<LevelController>
{
    public event Action<float> OnLevelCleared;
    const int GRID_SIZE = 8;
    private string[,] _crosswordGrid;

    [SerializeField]
    private CrosswordGridUI _crosswordGridUI;
    [SerializeField]
    private LetterLayout _letterLayout;
    [SerializeField]
    private WordChecker _wordChecker;
    [SerializeField]
    private GameObject _hintBtn;

    private float _levelTime;
    private bool _isPlaying;
    private bool _hintUsed;
    private int _currentLevel;
    private void Update()
    {
        if (!_isPlaying)
            return;

        _levelTime += Time.deltaTime;
    }
    private void OnEnable()
    {
        _wordChecker.OnLevelCleared += WordChecker_OnLevelCleared;
        _wordChecker.OnLetterAdded += WordChecker_OnLetterAdded;
        _wordChecker.OnWordChecked += WordChecker_OnWordChecked;
    }
    private void OnDisable()
    {
        _wordChecker.OnLevelCleared -= WordChecker_OnLevelCleared;
        _wordChecker.OnLetterAdded -= WordChecker_OnLetterAdded;
        _wordChecker.OnWordChecked -= WordChecker_OnWordChecked;
    }
    private void WordChecker_OnLevelCleared()
    {
        OnLevelCleared?.Invoke(_levelTime);
    }
    #region Loading actions
    public bool LoadLevel(int levelNum)
    {
        string levelToLoad = $"level_{levelNum}";

        var levelText = Resources.Load<TextAsset>($"Levels/{levelToLoad}");
        if (levelText == null)
        {
            Debug.Log($"Failed to load JSON file for level {levelNum}");
            return false;
        }
        string jsonData = levelText.text;
        CrosswordData crosswordData = JsonConvert.DeserializeObject<CrosswordData>(jsonData);

        if (crosswordData == null || crosswordData.crosswordGrid == null)
        {
            Debug.Log("Failed to deserialize JSON data");
            return false;
        }

        _crosswordGrid = new string[GRID_SIZE, GRID_SIZE];

        LoadGrid(crosswordData.crosswordGrid);
        LoadWords(crosswordData.levelWordsDict);
        LoadLetters(crosswordData.levelLetters);
        LevelStartActions(levelNum);
        return true;
    }
    private void LevelStartActions(int levelNum)
    {
        _isPlaying = true;
        _currentLevel = levelNum;
        _levelTime = 0;
        _hintUsed = false;
        _hintBtn.SetActive(true);
    }
    private void LoadLetters(List<string> levelLetters)
    {
        _letterLayout.LoadLetters(levelLetters);
    }
    private void LoadWords(Dictionary<string, List<System.Numerics.Vector2>> levelWordsDict)
    {
        _wordChecker.LoadWords(levelWordsDict);
    }
    void LoadGrid(List<List<string>> levelGrid)
    {
        for (int i = 0; i < levelGrid.Count; i++)
        {
            for (int j = 0; j < levelGrid[i].Count; j++)
            {
                _crosswordGrid[i, j] = levelGrid[i][j];
            }
        }
        _crosswordGridUI.LoadCrossword(_crosswordGrid);
    }
    public void PauseGame()
    {
        _isPlaying = false;
    }
    public void ResumeGame()
    {
        _isPlaying = true;
    }
    public bool LoadNextLevel() => LoadLevel(_currentLevel + 1);
    #endregion
    #region WordChecker
    public void AddLetter(string letter)
    {
        _wordChecker.AddLetter(letter);
        MusicManager.Instance.PlayEffect(ESFXType.pop);
    }
    public void RemoveLastLetter() => _wordChecker.RemoveLastLetter();
    public void CheckCurrentWord() => _wordChecker.CheckWord();
    public void GetHint()
    {
        if (_hintUsed) return;
        _hintUsed = true;
        _wordChecker.GetHint();
        _hintBtn.SetActive(false);
    }
    private void WordChecker_OnLetterAdded()
    {
        MusicManager.Instance.PlayEffect(ESFXType.pop);
    }
    private void WordChecker_OnWordChecked(bool correct)
    {
        if (!correct)
            MusicManager.Instance.PlayEffect(ESFXType.wrong);
        else
            MusicManager.Instance.PlayEffect(ESFXType.correct);
    }
    #endregion
    #region CrosswordGrid
    public void OpenCellList(List<System.Numerics.Vector2> cellList) => _crosswordGridUI.OpenCellList(cellList);
    public void OpenCell(System.Numerics.Vector2 cell) => _crosswordGridUI.OpenCell(cell);
    #endregion

}
