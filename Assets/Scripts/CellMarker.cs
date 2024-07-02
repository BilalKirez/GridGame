using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CellMarker : MonoBehaviour
{
    public bool isMarked = false;
    private SpriteRenderer spriteRenderer;
    public Sprite xSprite;
    public Sprite defaultSprite;
    public Vector2Int index;
    public GridCreator gridCreator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
    }

    private void OnMouseDown()
    {
        if (isMarked)
            return;

        gridCreator.connectedMarkedCells.Clear();
        isMarked = true;
        spriteRenderer.sprite = xSprite;
        CheckExplosion();
    }

    public void ResetCell()
    {
        isMarked = false;
        spriteRenderer.sprite = defaultSprite;
    }
    /// <summary>
    /// 
    /// </summary>
    public void CheckExplosion()
    {
        FindMarkedConnectedCells();
        if (gridCreator.connectedMarkedCells.Count >= 3)
        {
            ClearMarkedCells(gridCreator.connectedMarkedCells);
        }
    }

    private void ClearMarkedCells(List<CellMarker> markedCells)
    {
        foreach (CellMarker cell in markedCells)
        {
            cell.ResetCell();
        }
    }
    public void FindMarkedConnectedCells()
    {
        var validXs = new List<int> { this.index.x - 1, this.index.x + 1 }.Where(x => x > -1 && x < gridCreator.gridSize).ToList();
        var validYs = new List<int> { this.index.y - 1, this.index.y + 1 }.Where(y => y > -1 && y < gridCreator.gridSize).ToList();
        var validNeighborIndexs = new List<Vector2Int>();

        foreach (var x in validXs)
            validNeighborIndexs.Add(new Vector2Int(x, this.index.y));

        foreach (var y in validYs)
            validNeighborIndexs.Add(new Vector2Int(this.index.x, y));
        var markedNeighbours = new List<CellMarker>();
        foreach (var idx in validNeighborIndexs)
        {
            var cellMarker = gridCreator.cellMarkers[idx.x, idx.y];
            if (cellMarker != null && cellMarker.isMarked)
            {
                markedNeighbours.Add(cellMarker);
            }
        }
        gridCreator.connectedMarkedCells.Add(this);
        foreach (var markedNeighbour in markedNeighbours)
        {
            if (gridCreator.connectedMarkedCells.Find(x => x.index == markedNeighbour.index) != null)
            {
                continue;
            }
            markedNeighbour.FindMarkedConnectedCells();
        }
    }
}
