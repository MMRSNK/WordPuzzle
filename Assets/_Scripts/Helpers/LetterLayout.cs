using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterLayout : MonoBehaviour
{
    public LetterUI LetterUIPrefab;
    public Transform Container;
    public Transform OffsetTransform;
    public float Radius;
    public float IconScale = 1f;

    private List<string> _letters;
    private ObjectPool<LetterUI> _letterUIPool;

    private void Awake()
    {
        _letterUIPool = new ObjectPool<LetterUI>(LetterUIPrefab, 1, Container);
        _letterUIPool.DeactivatePool();
    }
    public void LoadLetters(List<string> levelLetters)
    {
        _letterUIPool.DeactivatePool();
        _letters = levelLetters;

        if(_letters == null)
        {
            Debug.LogError("Null list of letters!");
            return;
        }

        float stepLength = 360f / _letters.Count;
        
        for (int i = 0; i < _letters.Count; i++)
        {
            LetterUI letterUi = _letterUIPool.GetFreeElement();
            letterUi.Setup(this, _letters[i]);

            float angle = i * stepLength;
            Vector3 position = Quaternion.Euler(0, 0, angle) * Vector3.up * Radius;

            Vector3 offset = OffsetTransform.localPosition;
            offset.z = 0;
            letterUi.transform.localPosition = position + offset;

            letterUi.transform.localScale = Vector3.one * IconScale;
        }
    }
    public void OnLetterAdd(string letter)
    {
        LevelController.Instance.AddLetter(letter);
    }
    public void OnRemoveLastLetter()
    {
        LevelController.Instance.RemoveLastLetter();
    }
    public void OnEndDrag()
    {
        LevelController.Instance.CheckCurrentWord();
    }
}
