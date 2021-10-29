using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PacStudentController : MonoBehaviour, ITweenableObject
{
    [SerializeField] private Tweener tweener;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dustEffect;
    [SerializeField] private ParticleSystem wallHitEffect;
    [SerializeField] private float speed;

    private Vector2 gridPos;

    private Vector2 lastInput;
    private Vector2 currentInput;

    private PlayerInput input;

    private bool isTweening;

    private void Start()
    {
        input = new PlayerInput();
        input.Enable();

        gridPos = GridManager.GlobalPositionToGrid(transform.position);
        lastInput = new Vector2();
        currentInput = new Vector2();

        isTweening = false;
    }

    private void Update()
    {
        float horInput = input.Movement.MoveHorizontal.ReadValue<float>();
        float verInput = input.Movement.MoveVertical.ReadValue<float>();

        //prevents movement on two axis
        if (!(horInput != 0 && verInput != 0) && (horInput != 0 || verInput != 0))
        {
            lastInput.x = horInput;
            lastInput.y = verInput;
        }

        if (currentInput != Vector2.zero)
        {
            PlaySoundEffect(gridPos + currentInput);

            if (!gridManager.PositionIsMoveable(gridPos + currentInput) && !gridManager.PositionIsMoveable(gridPos + lastInput))
            {
                if (dustEffect.isPlaying)
                {
                    dustEffect.Stop();
                }

                if(!wallHitEffect.isPlaying)
                {
                    wallHitEffect.Play();
                }
            }
            else
            {
                if (!dustEffect.isPlaying)
                {
                    dustEffect.Play();
                }

                if(wallHitEffect.isPlaying)
                {
                    wallHitEffect.Stop();
                }
            }
        }

        if (!isTweening)
        {
            if (gridManager.PositionIsMoveable(gridPos + lastInput))
            {
                currentInput = lastInput;

                isTweening = true;
                animator.SetFloat("Moving", 1.0f);
                tweener.AddTween(transform, transform.position, GridManager.GridToGlobalPosition(gridPos + currentInput), 1.0f / speed, this);
            }
            else if (gridManager.PositionIsMoveable(gridPos + currentInput))
            {

                isTweening = true;
                animator.SetFloat("Moving", 1.0f);
                tweener.AddTween(transform, transform.position, GridManager.GridToGlobalPosition(gridPos + currentInput), 1.0f / speed, this);
            }
            RotatePacStudent(currentInput);
        }
    }

    public void NextTween()
    {
        gridPos = GridManager.GlobalPositionToGrid(transform.position);
        isTweening = false;
        animator.SetFloat("Moving", 0.0f);
    }

    private void PlaySoundEffect(Vector2 gridPos)
    {
        GridObjectType nextGridTile = gridManager.GetGridObjectType(gridPos);

        switch (nextGridTile)
        {
            case GridObjectType.Empty:
                audioManager.PlayMoveSound();
                break;
            case GridObjectType.Pellet:
            case GridObjectType.PowerPellet:
                audioManager.EatPellet();
                break;
            case GridObjectType.InsideCorner:
            case GridObjectType.InsideWall:
            case GridObjectType.OutsideCorner:
            case GridObjectType.OutsideWall:
                if (!gridManager.PositionIsMoveable(this.gridPos + lastInput) && !gridManager.PositionIsMoveable(gridPos))
                {
                    audioManager.StopSoundEffects();
                }
                break;
        }
    }

    private void RotatePacStudent(Vector2 direction)
    {
        switch(direction)
        {
            case Vector2 dir when dir.Equals(Vector2.right):
                transform.rotation = Quaternion.Euler(Vector3.zero);
                transform.localScale = Vector3.one;
                dustEffect.transform.localScale = Vector3.one;
                wallHitEffect.transform.localScale = Vector3.one;
                break;
            case Vector2 dir when dir.Equals(Vector2.left):
                transform.rotation = Quaternion.Euler(Vector3.zero);
                Vector3 scale = Vector3.one;
                scale.x = -1.0f;
                transform.localScale = scale;
                Vector3 flipY = Vector3.one;
                flipY.y = -1.0f;
                dustEffect.transform.localScale = flipY;
                wallHitEffect.transform.localScale = flipY;
                break;
            case Vector2 dir when dir.Equals(Vector2.up):
                Vector3 newScaleUp = transform.localScale;
                if(transform.localScale.x == -1.0f)
                {
                    newScaleUp = Vector3.one;
                    newScaleUp.y = -1.0f;
                }
                transform.localScale = newScaleUp;
                transform.rotation = Quaternion.Euler(Vector3.forward * 90.0f);
                break;
            case Vector2 dir when dir.Equals(Vector2.down):
                Vector3 newScaleDown = transform.localScale;
                if (transform.localScale.x == -1.0f)
                {
                    newScaleDown = Vector3.one;
                    newScaleDown.y = -1.0f;
                }
                transform.localScale = newScaleDown;
                transform.rotation = Quaternion.Euler(Vector3.forward * -90.0f);
                break;
        }
    }
}
