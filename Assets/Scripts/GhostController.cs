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
    [SerializeField] private GameManager gameManager;

    public GhostState State { get; private set; }

    private Vector2 previousDirection;
    private Vector2 target;

    private bool isMoving;
    private Vector2 gridPos;

    private Vector2[] corners;
    private int currentGhost4Corner;


    List<Vector2> moveablePositions = new List<Vector2>();

    private void Start()
    {
        currentGhost4Corner = 0;
        corners = new Vector2[] { new Vector2(12.5f, 13.0f), new Vector2(12.5f, -13.0f), new Vector2(-12.5f, -13.0f), new Vector2(-12.5f, 13.0f) };
        isMoving = false;
        gridPos = GridManager.GlobalPositionToGrid(transform.position);
        State = GhostState.Walking;
        ChooseTarget();
    }

    private void Update()
    {
        if (!isMoving && State != GhostState.Dead)
        {
            Vector2 distanceToTarget = new Vector2(target.x - transform.position.x, target.y - transform.position.y);


            bool canMoveX = false;
            bool canMoveY = false;

            Vector2 potentialXTarget = new Vector2(transform.position.x + Mathf.Sign(distanceToTarget.x), transform.position.y);
            Vector2 potentialYTarget = new Vector2(transform.position.x, transform.position.y + Mathf.Sign(distanceToTarget.y));

            if (distanceToTarget.x != 0)
            {
                if (InGhostHouse())
                {
                    canMoveX = gridManager.PositionIsMoveableGhostHouse(GridManager.GlobalPositionToGrid(potentialXTarget));
                }
                else
                {
                    canMoveX = gridManager.PositionIsMoveableGhost(GridManager.GlobalPositionToGrid(potentialXTarget));
                }
            }
            else if (distanceToTarget.y != 0)
            {
                if (InGhostHouse())
                {
                    canMoveY = gridManager.PositionIsMoveableGhostHouse(GridManager.GlobalPositionToGrid(potentialYTarget));
                }
                else
                {
                    canMoveY = gridManager.PositionIsMoveableGhost(GridManager.GlobalPositionToGrid(potentialYTarget));
                }
            }
            if (canMoveX)
            {
                tweener.AddTween(transform, transform.position, potentialXTarget, 1.0f / speed, this);
                isMoving = true;
                eyes.SetEyeDirection(Mathf.Sign(distanceToTarget.x) * Vector2.right);
                previousDirection = Mathf.Sign(distanceToTarget.x) * Vector2.right;
            }
            else if (canMoveY)
            {
                tweener.AddTween(transform, transform.position, potentialYTarget, 1.0f / speed, this);
                isMoving = true;
                eyes.SetEyeDirection(Mathf.Sign(distanceToTarget.y) * Vector2.up);
                previousDirection = Mathf.Sign(distanceToTarget.y) * Vector2.up;
            }
        }
        else
        {
            tweener.AddTween(transform, transform.position, target, 1.0f / 0.8f, this);
        }

        if (InGhostHouse() && State == GhostState.Dead)
        {
            Respawn();
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
        if (State != GhostState.Dead)
        {
            eyes.gameObject.SetActive(false);
            State = GhostState.Scared;
            speed *= 0.5f;
            animator.ResetTrigger("returnToNormal");
        }

        animator.SetTrigger("isScared");
    }

    public void RecoveringState()
    {
        if (State != GhostState.Dead)
        {
            State = GhostState.Recovering;
            eyes.gameObject.SetActive(false);
        }
        animator.SetTrigger("isRecovering");
    }

    public void WalkingState()
    {
        if (State != GhostState.Dead)
        {
            eyes.gameObject.SetActive(true);
            State = GhostState.Walking;
            speed *= 2.0f;
        }

        animator.SetTrigger("returnToNormal");
    }

    public void DeadState()
    {
        State = GhostState.Dead;
        rend.enabled = false;
        eyes.gameObject.SetActive(true);
        gameManager.GhostDeadMusic();
    }

    public void Respawn()
    {
        if (State == GhostState.Dead)
        {
            rend.enabled = true;
            State = gameManager.GetStateOfOtherGhosts();
            gameManager.RespawnMusic();

            if (State == GhostState.Scared || State == GhostState.Recovering)
            {
                eyes.gameObject.SetActive(false);
            }

            if (State == GhostState.Walking)
            {
                WalkingState();
            }
        }
    }

    private void ChooseTarget()
    {
        moveablePositions.Clear();

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

        if(State == GhostState.Dead && !InGhostHouse())
        {
            //tweener.CancelTween(transform); this just cancels the wrong tween sometimes idk why
            target = gridManager.GetGlobalPosOfClosetObject(GridObjectType.GhostHouseInterior, transform.position);
        }
        else if (InGhostHouse())
        {
            target = gridManager.GetGlobalPosOfClosetObject(GridObjectType.GhostHouseExit, transform.position);
        }
        else if(moveablePositions.Count == 1)
        {
            target = GridManager.GridToGlobalPosition(moveablePositions[0]);
        }
        else if(ghostNumber == 1 || State == GhostState.Scared || State == GhostState.Recovering)
        {
            List<Vector2> validPositions = new List<Vector2>();
            foreach(Vector2 pos in moveablePositions)
            {
                if(Vector2.Distance(pacStudent.position, GridManager.GridToGlobalPosition(pos)) >= Vector2.Distance(pacStudent.position, transform.position) && pos - gridPos != -previousDirection)
                {
                    validPositions.Add(pos);
                }
            }

            Vector2 furthestPos;

            if (validPositions.Count != 0)
            {
                furthestPos = validPositions[Random.Range(0, validPositions.Count)];
            }
            else
            {
                List<Vector2> notValidButMoveable = new List<Vector2>();

                foreach(Vector2 pos in moveablePositions)
                {
                    if(pos - gridPos != -previousDirection)
                    {
                        notValidButMoveable.Add(pos);
                    }
                }

                furthestPos = notValidButMoveable[Random.Range(0, notValidButMoveable.Count)];
            }

            target = GridManager.GridToGlobalPosition(furthestPos);
        }
        else if(ghostNumber == 2)
        {
            List<Vector2> validPositions = new List<Vector2>();
            foreach (Vector2 pos in moveablePositions)
            {
                if (Vector2.Distance(pacStudent.position, GridManager.GridToGlobalPosition(pos)) <= Vector2.Distance(pacStudent.position, transform.position) && pos - gridPos != -previousDirection)
                {
                    validPositions.Add(pos);
                }
            }

            Vector2 closestPos;

            if (validPositions.Count != 0)
            {
                closestPos = validPositions[Random.Range(0, validPositions.Count)];
            }
            else
            {
                List<Vector2> notValidButMoveable = new List<Vector2>();

                foreach (Vector2 pos in moveablePositions)
                {
                    if (pos - gridPos != -previousDirection)
                    {
                        notValidButMoveable.Add(pos);
                    }
                }

                closestPos = notValidButMoveable[Random.Range(0, notValidButMoveable.Count)];
            }

            target = GridManager.GridToGlobalPosition(closestPos);
        }
        else if(ghostNumber == 3)
        {
            List<Vector2> validPositions = new List<Vector2>();

            foreach (Vector2 pos in moveablePositions)
            {
                if (pos - gridPos != -previousDirection)
                {
                    validPositions.Add(pos);
                }
            }

            target = GridManager.GridToGlobalPosition(validPositions[Random.Range(0, validPositions.Count)]);
        }
        else if(ghostNumber == 4)
        {
            if(transform.position.x == corners[currentGhost4Corner].x && transform.position.y == corners[currentGhost4Corner].y)
            {
                currentGhost4Corner++;

                if(currentGhost4Corner == 4)
                {
                    currentGhost4Corner = 0;
                }
            }

            List<Vector2> validPositions = new List<Vector2>();
            foreach (Vector2 pos in moveablePositions)
            {
                if (Vector2.Distance(corners[currentGhost4Corner], GridManager.GridToGlobalPosition(pos)) <= Vector2.Distance(corners[currentGhost4Corner], transform.position) && pos - gridPos != -previousDirection)
                {
                    validPositions.Add(pos);
                }
            }

            Vector2 closestPos;

            if (validPositions.Count != 0)
            {
                closestPos = validPositions[Random.Range(0, validPositions.Count)];
            }
            else
            {
                List<Vector2> notValidButMoveable = new List<Vector2>();

                foreach (Vector2 pos in moveablePositions)
                {
                    if (pos - gridPos != -previousDirection)
                    {
                        notValidButMoveable.Add(pos);
                    }
                }

                closestPos = notValidButMoveable[Random.Range(0, notValidButMoveable.Count)];
            }

            target = GridManager.GridToGlobalPosition(closestPos);
        }
    }

    private bool InGhostHouse()
    {
        return gridManager.GetGridObjectType(gridPos) == GridObjectType.GhostHouseInterior;
    }
}
