using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterUI : MonoBehaviour, IPointerEnterHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private TextMeshProUGUI _tmp;
    private string _letter;
    private LetterLayout _layout;
    private void Awake()
    {
        _tmp = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Setup(LetterLayout layout, string letter)
    {
        _layout = layout;
        _letter = letter;
        _tmp.text = letter;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (LineController.Instance.AddPoint(transform.localPosition, out bool removed))
            _layout.OnLetterAdd(_letter);

        if (removed)
            _layout.OnRemoveLastLetter();
    }
    public void OnDrag(PointerEventData eventData)
    {
        LineController.Instance.isDrawing = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        LineController.Instance.isDrawing = false;
        LineController.Instance.ClearLine();
        _layout.OnEndDrag();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        LineController.Instance.isDrawing = false;
        LineController.Instance.ClearLine();
        _layout.OnEndDrag();
    }
}
