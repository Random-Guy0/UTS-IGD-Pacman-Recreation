using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour //write subclasses for differing ghost behaviour
{
    [SerializeField] private GhostEyesDirection eyes;

    private void OnCollisionEnter2D()
    {
        Vector2 direction = move();
        eyes.SetEyeDirection(direction);
    }

    private Vector2 move()
    {
        return Vector2.zero;
    }


}
