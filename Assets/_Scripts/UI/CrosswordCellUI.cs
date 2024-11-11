using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrosswordCellUI : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI TMP;

    public Color normalColor;
    public Color openedLetterColor;
    public Color disabledColor;

    private string letter;

    public void Setup(string letter) 
    {
        if (string.IsNullOrEmpty(letter) || letter == "*")
        {
            DisableCell();
        }
        else
        {
            EnableCell(letter);
        }
    }
    private void EnableCell(string letter)
    {
        Image.color = normalColor;
        TMP.text = string.Empty;
        this.letter = letter;
    }
    private void DisableCell()
    {
        Image.color = disabledColor;
        TMP.text = string.Empty;
    }
    public void OpenCell()
    {
        Image.color = openedLetterColor;
        TMP.text = letter.ToUpper();
    }
}
