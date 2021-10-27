using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour, ITweenableObject
{
    [SerializeField] private Tweener tweener;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private float speed;

    private Vector2 gridPos;

    private Vector2 lastInput;
    private Vector2 currentInput;

    private PlayerInput input;

    private bool isTweening = false;

    private void Start()
    {
        input = new PlayerInput();
        input.Enable();

        gridPos = GridManager.GlobalPositionToGrid(transform.position);
        lastInput = new Vector2();
        currentInput = new Vector2();
    }

    private void Update()
    {
        float horInput = input.Movement.MoveHorizontal.ReadValue<float>();
        float verInput = input.Movement.MoveVertical.ReadValue<float>();

        //prevents movement on two axis
        if(!(horInput != 0 && verInput != 0) && (horInput != 0 || verInput != 0))
        {
            lastInput.x = horInput;
            lastInput.y = verInput;
        }

        if(!isTweening)
        {
            if (gridManager.PositionIsMoveable(gridPos + lastInput))
            {
                currentInput = lastInput;
                isTweening = true;
                tweener.AddTween(transform, transform.position, GridManager.GridToGlobalPosition(gridPos + currentInput), 1.0f/speed, this);
            }
            else if (gridManager.PositionIsMoveable(gridPos + currentInput))
            {
                isTweening = true;
                tweener.AddTween(transform, transform.position, GridManager.GridToGlobalPosition(gridPos + currentInput), 1.0f / speed, this);
            }
        }
    }

    public void NextTween()
    {
        gridPos = GridManager.GlobalPositionToGrid(transform.position);
        isTweening = false;
    }
}
