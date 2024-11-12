using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelButton2UI : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] starsArr;
    public TextMeshProUGUI LevelTMP;
    public TextMeshProUGUI RecordTMP;

    public int Level;
    private LevelButtonsUI _manager;
    public void OnPointerClick(PointerEventData eventData)
    {
        _manager.OnLevelBtn2(this);
    }

    public void Setup(LevelButtonsUI manager, int level, int stars, float timeRecord)
    {
        _manager = manager;
        Level = level;
        ActivateStars( stars );
        LevelTMP.text = $"Π³βενό {level}";
        RecordTMP.text = timeRecord > 0 ? $"{timeRecord.ToString("00.0")} sec" : "No Record";
    }

    private void ActivateStars(int stars)
    {
        for (int i = 0; i < stars; i++)
        {
            starsArr[i].SetActive(true);
        }
    }
}
