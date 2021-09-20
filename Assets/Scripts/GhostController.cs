using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostController : MonoBehaviour //write subclasses for differing ghost behaviour
{
    [SerializeField] private GhostEyesDirection eyes;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer rend;

    [SerializeField] private float startingScaredTime;
    [SerializeField] private float startingFlashingTime;

    private bool isDead = false;

    private Vector2 direction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction = CalculateDirection(direction);
            eyes.SetEyeDirection(direction);
        }

        if (collision.gameObject.CompareTag("GhostHouse") && isDead) 
        {
            rend.enabled = true;
            isDead = false;
        }
    }

    protected abstract Vector2 CalculateDirection(Vector2 previousDirection);

    public void PowerPelletCollected()
    {
        animator.SetTrigger("isScared");
        CancelInvoke();
        Invoke("FlashingState", startingScaredTime);
    }

    public void FlashingState()
    {
        animator.SetTrigger("isFlashing");
        Invoke("ReturnToNormal", startingFlashingTime);
    }

    public void ReturnToNormal()
    {
        animator.SetTrigger("returnToNormal");
    }

    public void Dead()
    {
        rend.enabled = false;
        isDead = true;
    }
}
