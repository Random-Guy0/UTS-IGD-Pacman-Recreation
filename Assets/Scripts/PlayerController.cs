using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private float speed;

    private Animator animator;

    private Vector3[] targetList;

    private int i = 0;

    private Tween activeTween;

    private void Start()
    {
        audioManager.PlayMoveSound();
        animator = GetComponent<Animator>();
        animator.SetTrigger("startMoving");
        targetList = new Vector3[] { new Vector3(-7.5f, 13.0f), new Vector3(-7.5f, 9.0f), new Vector3(-12.5f, 9.0f), new Vector3(-12.5f, 13.0f) };
        activeTween = new Tween(transform, transform.position, targetList[i], Time.time, CalculateDuration(transform.position, targetList[i]));
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, activeTween.EndPos) > 0.1f)
        {
            float t = CalculateTimeFraction(activeTween.StartTime, activeTween.Duration);
            transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, t);
        }
        else
        {
            transform.position = activeTween.EndPos;
            if (transform.eulerAngles.z != 270.0f)
            {
                if (transform.localScale.y < 0)
                {
                    Vector3 scale = transform.localScale;
                    scale.y *= -1;
                    transform.localScale = scale;
                }
                transform.Rotate(Vector3.forward, -90.0f);
                
            }
            else
            {
                transform.Rotate(Vector3.forward, -90.0f);
                Vector3 scale = transform.localScale;
                scale.y *= -1;
                transform.localScale = scale;
            }
            i++;
            if(i == 4)
            {
                i = 0;
            }
            activeTween = new Tween(transform, transform.position, targetList[i], Time.time, CalculateDuration(transform.position, targetList[i]));
        }
    }

    private float CalculateTimeFraction(float startTime, float duration)
    {
        float t = (Time.time - startTime) / duration;
        return t;
    }

    private float CalculateDuration(Vector3 startPos, Vector3 endPos)
    {
        float distance = Vector3.Distance(startPos, endPos);
        float duration = distance / speed;
        return duration;
    }
}
