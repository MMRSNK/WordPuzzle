using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMP_ContentSizeFitter : MonoBehaviour
{    
    public float PaddingX;

    private TextMeshProUGUI tmp;
    private RectTransform tmp_rt;
    private RectTransform _rt;

    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        tmp_rt = tmp.rectTransform;
        _rt = GetComponent<RectTransform>();
    }
    private void Start()
    {
        OnTextUpdated();
    }
    public void OnTextUpdated()
    {
        if (tmp == null)
            return;

        float preferedWidth = tmp.preferredWidth;
        _rt.sizeDelta = new Vector2(preferedWidth + PaddingX, _rt.sizeDelta.y);
    }
}
