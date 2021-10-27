using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GridObject[,] grid;

    public static Vector2 GlobalPositionToGrid(Vector2 globalPos)
    {
        return new Vector2(globalPos.x + 0.5f, globalPos.y);
    }

    public static Vector2 GridToGlobalPosition(Vector2 gridPos)
    {
        return new Vector2(gridPos.x - 0.5f, gridPos.y);
    }

    private void Start()
    {
        grid = new GridObject[28, 29];

        GridObject[] unsortedGrid = FindObjectsOfType<GridObject>();

        foreach(GridObject gridObject in unsortedGrid)
        {
            gridObject.SetGridPos();
            grid[(int)gridObject.GridPos.x + 13, (int)gridObject.GridPos.y + 14] = gridObject;
        }

        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                if(grid[i, j] == null)
                {
                    GameObject emptySpace = new GameObject("emptySpace");
                    emptySpace.transform.position = GridToGlobalPosition(new Vector2(i - 13, j - 14));
                    GridObject emptyGrid = emptySpace.AddComponent<GridObject>();
                    emptyGrid.SetGridPos();
                    grid[i, j] = emptyGrid;
                }
            }
        }
    }

    public GridObject GetGridAtPosition(Vector2 gridPos)
    {
        return  grid[(int)gridPos.x + 13, (int)gridPos.y + 14];
    }

    public bool PositionIsMoveable(Vector2 gridPos)
    {
        GridObject selectedGrid = GetGridAtPosition(gridPos);

        return selectedGrid.ObjectType != GridObjectType.InsideCorner && selectedGrid.ObjectType != GridObjectType.InsideWall
            && selectedGrid.ObjectType != GridObjectType.OutsideCorner && selectedGrid.ObjectType != GridObjectType.OutsideWall 
            && selectedGrid.ObjectType != GridObjectType.TJunction;
    }
}
