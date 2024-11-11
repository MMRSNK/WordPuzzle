using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonsUI : MonoBehaviour
{
    public event Action<int> OnRequestLoadLevel;

    public LevelBtnUI LevelBtnPrefab;
    public Transform Container;

    public LevelData[] levels;

    private void Start()
    {
        CreateLevelBtns();
    }
    public void CreateLevelBtns()
    {
        if (levels == null)
            return;

        for (int i = 0; i < levels.Length; i++)
        {
            var levelBtn = Instantiate(LevelBtnPrefab, Container);
            levelBtn.Setup(levels[i], this);
        }
    }
    public void OnLevelBtn(LevelBtnUI btn)
    {
        OnRequestLoadLevel?.Invoke(btn.Data.levelNum);
    }
}
