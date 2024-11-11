using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCompleateUI : MonoBehaviour
{
    public TextMeshProUGUI TimeTMP;

    public void LevelCompleated(float time)
    {
        TimeTMP.text = $"{time.ToString("00.0")} sec";
    }
}
