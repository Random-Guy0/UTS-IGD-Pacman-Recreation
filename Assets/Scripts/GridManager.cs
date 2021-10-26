using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int[,] Grid { get { return grid; } }
    private int[,] grid;

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
        grid = new int[28, 29];
    }
}
