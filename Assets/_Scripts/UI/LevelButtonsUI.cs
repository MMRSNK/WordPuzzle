using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonsUI : MonoBehaviour
{
    public event Action<int> OnRequestLoadLevel;

    public LevelBtnUI LevelBtnPrefab;
    public LevelButton2UI LevelButton2Prefab;
    public Transform Container;

    public LevelData[] levels;

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
    public void CreateLevelBtns2(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var levelBtn = Instantiate(LevelButton2Prefab, Container);

            //load saved data
            int stars = UnityEngine.Random.Range(0, 3);
            float record = UnityEngine.Random.Range(0, 50f);

            levelBtn.Setup(this,i +1,stars,record);
        }
    }
    public void OnLevelBtn(LevelBtnUI btn)
    {
        OnRequestLoadLevel?.Invoke(btn.Data.levelNum);
    }
    public void OnLevelBtn2(LevelButton2UI btn)
    {
        OnRequestLoadLevel?.Invoke(btn.Level);
    }
}
