using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenToLocalPosition
{
    public static Vector2 ConvertToLocalPosition(Canvas canvas, RectTransform uiElementRectTransform, Vector2 touchPosition)
    {
        // Convert screen space to local canvas space
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            touchPosition,
            Camera.main,
            out Vector2 localPosition
        );
        // Convert local canvas position to local UI element position
        Vector2 localElementPosition = localPosition - uiElementRectTransform.anchoredPosition;
        return localElementPosition;
        // Now localElementPosition is the local position within the UI element
        //Debug.Log("Local Element Position: " + localElementPosition);

    }

}
