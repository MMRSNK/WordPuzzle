using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelBtnUI  : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _btnImage;
    [SerializeField]
    private TextMeshProUGUI _btnTMP;

    private LevelButtonsUI _manager;
    public LevelData Data { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        _manager.OnLevelBtn(this);
    }
    public void Setup(LevelData levelData, LevelButtonsUI levelManager)
    {
        Data = levelData;
        _manager = levelManager;

        _btnImage.sprite = Data.sprite;
        _btnTMP.text = $"Π³βενό {Data.levelNum}";
    }
}
