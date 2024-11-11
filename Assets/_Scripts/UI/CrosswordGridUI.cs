using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswordGridUI : MonoBehaviour
{
    public CrosswordCellUI CrosswordCellUIPrefab;
    public Transform GridContainerTransform;

    private Dictionary<System.Numerics.Vector2, CrosswordCellUI> _crosswordCellsGrid = new();
    private ObjectPool<CrosswordCellUI> _crosswordCellsPool;
    private void Start()
    {
        _crosswordCellsPool = new ObjectPool<CrosswordCellUI>(CrosswordCellUIPrefab,1, GridContainerTransform);
    }
    public void LoadCrossword(string[,] crosswordGrid)
    {
        _crosswordCellsPool.DeactivatePool();
        _crosswordCellsGrid.Clear();

        for (int i = 0; i < crosswordGrid.GetLength(0); i++)
        {
            for (int j = 0; j < crosswordGrid.GetLength(1); j++)
            {
                CrosswordCellUI cellUI = _crosswordCellsPool.GetFreeElement();
                cellUI.Setup(crosswordGrid[i, j]);
                _crosswordCellsGrid.Add(new System.Numerics.Vector2(i,j),cellUI);
            }
        }
    }
    public void OpenCellList(List<System.Numerics.Vector2> positions)
    {
        foreach (System.Numerics.Vector2 position in positions)
        {
            OpenCell(position);
        }
    }
    public void OpenCell(System.Numerics.Vector2 position)
    {
        _crosswordCellsGrid[position].OpenCell();
    }
}
