using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private GhostEyesDirection eyes;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer rend;

    public GhostState State { get; private set; }

    private Vector2 direction;

    private void Start()
    {
        State = GhostState.Walking;
    }

    private Vector2 CalculateDirection(Vector2 previousDirection)
    {
        return Vector2.zero;
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
        animator.SetTrigger("returnToNormal");
        eyes.gameObject.SetActive(true);
        State = GhostState.Walking;
    }
}
