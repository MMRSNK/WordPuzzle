using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class LineController : Singleton<LineController>
{
    [SerializeField]
    private UILineRenderer _lineRenderer;
    [HideInInspector]
    public bool isDrawing = false;

    private RectTransform rt;
    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (!isDrawing)
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, InputReader.Instance.GetTouchPosition(), Camera.main, out Vector2 localPoint);

        _lineRenderer.points[_lineRenderer.points.Count - 1] = localPoint;
        _lineRenderer.SetAllDirty();
    }
    public bool AddPoint(Vector2 point, out bool letterRemoved)
    {
        //if it is first point add two, second to follow touch position        
        if (_lineRenderer.points.Count == 0)
        {
            _lineRenderer.points.Add(point);
            _lineRenderer.points.Add(point);
            _lineRenderer.SetAllDirty();

            letterRemoved = false;
            return true;
        }
        // check if the last point are same and it is not first one
        if (_lineRenderer.points[_lineRenderer.points.Count - 2] == point && _lineRenderer.points.Count - 2 != 0)
        {
            RemovePoint(point);
            letterRemoved = true;
            return false;
        }

        if (_lineRenderer.points.Contains(point))
        {
            letterRemoved = false;
            return false;
        }
        else
        {
            //if we already drawing set last point to new position and add new for drawing
            _lineRenderer.points[_lineRenderer.points.Count - 1] = point;
            _lineRenderer.points.Add(point);
        }
        _lineRenderer.SetAllDirty();
        letterRemoved = false;
        return true;
    }
    public void RemovePoint(Vector2 point)
    {
        if (!_lineRenderer.points.Contains(point))
            return;
        _lineRenderer.points.Remove(point);
        _lineRenderer.SetAllDirty();
    }
    public void ClearLine()
    {
        _lineRenderer.points.Clear();
        _lineRenderer.SetAllDirty();
    }
}
