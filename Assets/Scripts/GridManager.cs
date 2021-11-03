using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject emptySpace;

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
        grid = new GridObject[30, 29];

        GridObject[] unsortedGrid = FindObjectsOfType<GridObject>();

        foreach(GridObject gridObject in unsortedGrid)
        {
            gridObject.SetGridPos();
            grid[(int)gridObject.GridPos.x + 14, (int)gridObject.GridPos.y + 14] = gridObject;
        }

        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                if(grid[i, j] == null)
                {
                    AddEmptySpace(GridToGlobalPosition(new Vector2(i - 14, j - 14)));
                }
            }
        }
    }

    public GridObject GetGridAtPosition(Vector2 gridPos)
    {
        return  grid[(int)gridPos.x + 14, (int)gridPos.y + 14];
    }

    public bool PacStudentPositionIsMoveable(Vector2 gridPos)
    {
        GridObject selectedGrid = GetGridAtPosition(gridPos);

        return selectedGrid.ObjectType != GridObjectType.InsideCorner && selectedGrid.ObjectType != GridObjectType.InsideWall
            && selectedGrid.ObjectType != GridObjectType.OutsideCorner && selectedGrid.ObjectType != GridObjectType.OutsideWall
            && selectedGrid.ObjectType != GridObjectType.TJunction && selectedGrid.ObjectType != GridObjectType.GhostHouseExit
            && selectedGrid.ObjectType != GridObjectType.GhostHouseInterior;
    }

    public GridObjectType GetGridObjectType(Vector2 gridPos)
    {
        return grid[(int)gridPos.x + 14, (int)gridPos.y + 14].ObjectType;
    }

    public GameObject AddEmptySpace(Vector2 pos)
    {
        GameObject emptySpace = Instantiate(this.emptySpace, pos, Quaternion.identity);
        GridObject emptyGrid = emptySpace.GetComponent<GridObject>();
        emptyGrid.SetGridPos();
        grid[(int)emptyGrid.GridPos.x + 14, (int)emptyGrid.GridPos.y + 14] = emptyGrid;
        return emptySpace;
    }
}
