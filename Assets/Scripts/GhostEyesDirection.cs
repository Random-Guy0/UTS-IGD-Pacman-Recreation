using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyesDirection : MonoBehaviour
{
    [SerializeField] private Sprite[] verticalEyeSprites;
    [SerializeField] private Sprite[] horizontalEyeSprites;

    [SerializeField] private SpriteRenderer eyes;

    [SerializeField] private Vector2 direction;

    private Sprite[,] eyeSprites = new Sprite[3, 3];

    private void Start()
    {
        eyeSprites[0, 1] = horizontalEyeSprites[0];
        eyeSprites[2, 1] = horizontalEyeSprites[1];

        eyeSprites[1, 0] = verticalEyeSprites[0];
        eyeSprites[1, 2] = verticalEyeSprites[1];
    }

    private void Update()
    {
        SetEyeDirection(direction);
    }

    public void SetEyeDirection(Vector2 direction)
    {
        direction.x += 1;
        direction.y += 1;

        eyes.sprite = eyeSprites[(int)direction.x, (int)direction.y];
    }
}
