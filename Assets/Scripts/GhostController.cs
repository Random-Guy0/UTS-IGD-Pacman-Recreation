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

    public GhostState State { get; private set; }

    private Vector2 direction;
    private Vector2 target;

    private bool isMoving;
    private Vector2 gridPos;

    private void Start()
    {
        isMoving = false;
        gridPos = GridManager.GlobalPositionToGrid(transform.position);
        State = GhostState.Walking;
    }

    private void Update()
    {
        if (!isMoving)
        {
            ChooseTarget();

            Vector2 distanceToTarget = new Vector2(transform.position.x - target.x, transform.position.y - target.y);

            bool canMoveX = false;
            bool canMoveY = false;

            Vector2 potentialXTarget = new Vector2(transform.position.x - Mathf.Sign(target.x), transform.position.y);
            Vector2 potentialYTarget = new Vector2(transform.position.x, transform.position.y + Mathf.Sign(target.y));


            if(distanceToTarget.x != 0)
            {
                if(inGhostHouse())
                {
                    canMoveX = gridManager.PositionIsMoveableGhostHouse(potentialXTarget);
                }
                else
                {
                    canMoveX = gridManager.PositionIsMoveable(potentialXTarget);
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
                    canMoveY = gridManager.PositionIsMoveable(potentialYTarget);
                }
            }

            if(canMoveX)
            {
                tweener.AddTween(transform, transform.position, potentialXTarget, 1.0f/speed, this);
                isMoving = true;
            }
            else if(canMoveY)
            {
                tweener.AddTween(transform, transform.position, potentialYTarget, 1.0f / speed, this);
                isMoving = true;
            }
        }
    }

    public void NextTween()
    {
        isMoving = false;
        gridPos = GridManager.GlobalPositionToGrid(transform.position);
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
        if(inGhostHouse())
        {
            target = gridManager.GetGlobalPosOfClosetObject(GridObjectType.GhostHouseExit, transform.position);
        }
    }

    private bool inGhostHouse()
    {
        return gridManager.GetGridObjectType(gridPos) == GridObjectType.GhostHouseInterior;
    }
}
