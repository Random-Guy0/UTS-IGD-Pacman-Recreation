using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Vector2 GridPos { get; private set; }

    public GridObjectType ObjectType { get { return (GridObjectType)objectType; } }
    [SerializeField] private int objectType = 0;

    public void SetGridPos()
    {
        GridPos = GridManager.GlobalPositionToGrid(transform.position);
    }

    public void SetObjectType(GridObjectType type)
    {
        objectType = (int)type;
    }
}
