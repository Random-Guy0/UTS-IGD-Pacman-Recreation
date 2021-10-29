using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private GhostEyesDirection eyes;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer rend;

    private Vector2 direction;

    private Vector2 CalculateDirection(Vector2 previousDirection)
    {
        return Vector2.zero;
    }

    public void ScaredState()
    {
        animator.SetTrigger("isScared");
    }

    public void RecoveringState()
    {
        animator.SetTrigger("isRecovering");
    }

    public void WalkingState()
    {
        animator.SetTrigger("returnToNormal");
    }
}
