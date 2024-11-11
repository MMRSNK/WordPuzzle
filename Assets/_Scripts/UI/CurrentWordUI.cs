using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWordUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _currentWordTMP;
    [SerializeField]
    private GameObject _currentWordGO;
    [SerializeField]
    private Image _currentWordBGImage;
    [SerializeField]
    private Color _currentWordBGColor;
    [SerializeField]
    private float _secondsToDisableCurentWord;
    [SerializeField]
    private TMP_ContentSizeFitter _currentWord_ContentSizeFitter;
    public void UpdateText(string text)
    {
        _currentWordTMP.text = text.ToUpper();
        _currentWordGO.SetActive(true);
        _currentWord_ContentSizeFitter.OnTextUpdated();
        _currentWord_ContentSizeFitter.OnTextUpdated();
    }
    public void Reset()
    {
        StopAllCoroutines();
        _currentWordTMP.text = "";
        _currentWordBGImage.color = _currentWordBGColor;
        _currentWordGO.SetActive(false);
    }
    public void WordChecked(bool isCorrect)
    {
        StartCoroutine(CurrentWordBackgroundCollorChange(isCorrect));
    }
    private IEnumerator CurrentWordBackgroundCollorChange(bool correctWord)
    {
        Color bgColor = correctWord ? Color.green : Color.red;

        _currentWordBGImage.color = bgColor;

        yield return new WaitForSeconds(_secondsToDisableCurentWord);

        _currentWordGO.SetActive(false);
    }
}
