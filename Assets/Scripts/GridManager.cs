using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public List<CellMarker> connectedMarkedCells = new List<CellMarker>();
    public CellMarker[,] cellMarkers;
    public GridCreator gridCreator;

    public void CheckExplosion(CellMarker cellMarker)
    {
        FindMarkedConnectedCells(cellMarker);
        if (connectedMarkedCells.Count >= 3)
        {
            ClearMarkedCells(connectedMarkedCells);
        }
    }

    private void ClearMarkedCells(List<CellMarker> markedCells)
    {
        foreach (CellMarker cell in markedCells)
        {
            cell.ResetCell();
        }
    }

    public void FindMarkedConnectedCells(CellMarker cellMarker)
    {
        var validXs = new List<int> { cellMarker.index.x - 1, cellMarker.index.x + 1 }.Where(x => x > -1 && x < gridCreator.gridSize).ToList();
        var validYs = new List<int> { cellMarker.index.y - 1, cellMarker.index.y + 1 }.Where(y => y > -1 && y < gridCreator.gridSize).ToList();
        var validNeighborIndexs = new List<Vector2Int>();

        foreach (var x in validXs)
            validNeighborIndexs.Add(new Vector2Int(x, cellMarker.index.y));

        foreach (var y in validYs)
            validNeighborIndexs.Add(new Vector2Int(cellMarker.index.x, y));

        var markedNeighbours = new List<CellMarker>();
        foreach (var idx in validNeighborIndexs)
        {
            var neighbourCellMarker = cellMarkers[idx.x, idx.y];
            if (neighbourCellMarker != null && neighbourCellMarker.isMarked)
            {
                markedNeighbours.Add(neighbourCellMarker);
            }
        }

        connectedMarkedCells.Add(cellMarker);
        foreach (var markedNeighbour in markedNeighbours)
        {
            if (connectedMarkedCells.Find(x => x.index == markedNeighbour.index) != null)
            {
                continue;
            }
            FindMarkedConnectedCells(markedNeighbour);
        }
    }
}

