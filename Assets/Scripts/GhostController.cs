using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour, ITweenableObject
{
    [SerializeField] private GhostEyesDirection eyes;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Tweener tweener;
    [SerializeField] private float speed;
    [SerializeField] private int ghostNumber;
    [SerializeField] private Transform pacStudent;

    public GhostState State { get; private set; }

    private Vector2 previousDirection;
    private Vector2 target;

    private bool isMoving;
    private Vector2 gridPos;

    private void Start()
    {
        isMoving = false;
        gridPos = GridManager.GlobalPositionToGrid(transform.position);
        State = GhostState.Walking;
        ChooseTarget();
    }

    private void Update()
    {
        if (!isMoving)
        {
            Vector2 distanceToTarget = new Vector2(target.x - transform.position.x, target.y - transform.position.y);


            bool canMoveX = false;
            bool canMoveY = false;

            Vector2 potentialXTarget = new Vector2(transform.position.x + Mathf.Sign(distanceToTarget.x), transform.position.y);
            Vector2 potentialYTarget = new Vector2(transform.position.x, transform.position.y + Mathf.Sign(distanceToTarget.y));


            if(distanceToTarget.x != 0)
            {
                if(inGhostHouse())
                {
                    canMoveX = gridManager.PositionIsMoveableGhostHouse(potentialXTarget);
                }
                else
                {
                    canMoveX = gridManager.PositionIsMoveableGhost(potentialXTarget);
                }
            }
            else if(distanceToTarget.y != 0)
            {
                if(inGhostHouse())
                {
                    canMoveY = gridManager.PositionIsMoveableGhostHouse(potentialYTarget);
                }
                else
                {
                    canMoveY = gridManager.PositionIsMoveableGhost(potentialYTarget);
                }
            }

            if(canMoveX)
            {
                tweener.AddTween(transform, transform.position, potentialXTarget, 1.0f/speed, this);
                isMoving = true;
                eyes.SetEyeDirection(Mathf.Sign(distanceToTarget.x) * Vector2.right);
                previousDirection = Mathf.Sign(distanceToTarget.x) * Vector2.right;
            }
            else if(canMoveY)
            {
                tweener.AddTween(transform, transform.position, potentialYTarget, 1.0f / speed, this);
                isMoving = true;
                eyes.SetEyeDirection(Mathf.Sign(distanceToTarget.y) * Vector2.up);
                previousDirection = Mathf.Sign(distanceToTarget.y) * Vector2.up;
            }
        }
    }

    public void NextTween()
    {
        isMoving = false;
        gridPos = GridManager.GlobalPositionToGrid(transform.position);
        ChooseTarget();
    }

    public void ScaredState()
    {
        animator.SetTrigger("isScared");
        eyes.gameObject.SetActive(false);
        State = GhostState.Scared;
    }

    public void RecoveringState()
    {
        animator.SetTrigger("isRecovering");
        State = GhostState.Recovering;
    }

    public void WalkingState()
    {
        if (State != GhostState.Dead)
        {
            animator.SetTrigger("returnToNormal");
            eyes.gameObject.SetActive(true);
            State = GhostState.Walking;
        }
    }

    public void DeadState()
    {
        State = GhostState.Dead;
        rend.enabled = false;
        eyes.gameObject.SetActive(true);
        Invoke("Respawn", 5.0f);
    }

    private void Respawn()
    {
        animator.SetTrigger("returnToNormal");
        rend.enabled = true;
        State = GhostState.Walking;
    }

    private void ChooseTarget()
    {
        List<Vector2> moveablePositions = new List<Vector2>();

        if(gridManager.PositionIsMoveableGhost(gridPos + Vector2.up))
        {
            moveablePositions.Add(gridPos + Vector2.up);
        }

        if (gridManager.PositionIsMoveableGhost(gridPos + Vector2.down))
        {
            moveablePositions.Add(gridPos + Vector2.down);
        }

        if (gridManager.PositionIsMoveableGhost(gridPos + Vector2.left))
        {
            moveablePositions.Add(gridPos + Vector2.left);
        }

        if (gridManager.PositionIsMoveableGhost(gridPos + Vector2.right))
        {
            moveablePositions.Add(gridPos + Vector2.right);
        }

        if (inGhostHouse())
        {
            target = gridManager.GetGlobalPosOfClosetObject(GridObjectType.GhostHouseExit, transform.position);
        }
        else if(moveablePositions.Count == 1)
        {
            target = GridManager.GridToGlobalPosition(moveablePositions[0]);
        }
        else if(ghostNumber == 1 || State == GhostState.Scared || State == GhostState.Recovering)
        {
            Vector2 furthestPos = moveablePositions[0];

            for(int i = 1; i < moveablePositions.Count; i++)
            {
                if(Vector2.Distance(moveablePositions[i], GridManager.GlobalPositionToGrid(pacStudent.position)) > Vector2.Distance(furthestPos, GridManager.GlobalPositionToGrid(pacStudent.position)))
                {
                    furthestPos = moveablePositions[i];
                }
            }

            /*List<Vector2> validPositions = new List<Vector2>();
            foreach(Vector2 pos in moveablePositions)
            {
                Debug.Log(pos);
                if(Vector2.Distance(pacStudent.position, GridManager.GridToGlobalPosition(pos)) >= Vector2.Distance(pacStudent.position, transform.position))
                {
                    validPositions.Add(pos);
                }
            }

            Vector2 furthestPos;

            if (validPositions.Count != 0)
            {
                furthestPos = validPositions[0];

                for (int i = 1; i < validPositions.Count; i++)
                {
                    if (Vector2.Distance(pacStudent.position, GridManager.GridToGlobalPosition(validPositions[i])) > Vector2.Distance(pacStudent.position, GridManager.GridToGlobalPosition(furthestPos)))
                    {
                        furthestPos = validPositions[i];
                    }
                }
            }
            else
            {
                furthestPos = moveablePositions[0];

                for (int i = 1; i < moveablePositions.Count; i++)
                {
                    if (Vector2.Distance(pacStudent.position, GridManager.GridToGlobalPosition(moveablePositions[i])) > Vector2.Distance(pacStudent.position, GridManager.GridToGlobalPosition(furthestPos)) && moveablePositions[i] - gridPos != previousDirection)
                    {
                        furthestPos = moveablePositions[i];
                    }
                }
            }*/

            furthestPos.x *= -1;
            target = GridManager.GridToGlobalPosition(furthestPos);
        }
    }

    private bool inGhostHouse()
    {
        return gridManager.GetGridObjectType(gridPos) == GridObjectType.GhostHouseInterior;
    }
}
