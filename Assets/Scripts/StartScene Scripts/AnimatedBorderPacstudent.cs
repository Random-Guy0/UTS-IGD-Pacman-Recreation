using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBorderPacstudent : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform canvas;

    private Vector2[] anchorList;

    private Tween activeTween;

    private int nextTarget = 0;

    private void Start()
    {
        anchorList = new Vector2[4];
        anchorList[0] = new Vector2(0.5f, 1.0f);
        anchorList[1] = new Vector2(0.0f, 0.5f);
        anchorList[2] = new Vector2(0.5f, 0.0f);
        anchorList[3] = new Vector2(1.0f, 0.5f);

        rectTransform.anchorMax = anchorList[nextTarget];
        rectTransform.anchorMin = anchorList[nextTarget];
        Vector2 target = FindNextTarget(nextTarget);
        activeTween = new Tween(transform, rectTransform.anchoredPosition, target, Time.time, CalculateDuration(rectTransform.anchoredPosition, target));
    }

    private void Update()
    {
        if(Vector2.Distance(rectTransform.anchoredPosition, activeTween.EndPos) > 0.1f)
        {
            float t = CalculateTimeFraction(activeTween.StartTime, activeTween.Duration);
            rectTransform.anchoredPosition = Vector2.Lerp(activeTween.StartPos, activeTween.EndPos, t);
        }
        else
        {
            rectTransform.anchoredPosition = activeTween.EndPos;

            if (rectTransform.eulerAngles.z != 90.0f)
            {
                if (rectTransform.localScale.y < 0)
                {
                    Vector3 scale = rectTransform.localScale;
                    scale.y *= -1;
                    rectTransform.localScale = scale;
                }
                rectTransform.Rotate(Vector3.forward, 90.0f);

            }
            else
            {
                rectTransform.Rotate(Vector3.forward, 90.0f);
                Vector3 scale = rectTransform.localScale;
                scale.y *= -1;
                rectTransform.localScale = scale;
            }

            nextTarget++;
            if(nextTarget == 4)
            {
                nextTarget = 0;
            }

            Vector3 pos = rectTransform.position;
            rectTransform.anchorMax = anchorList[nextTarget];
            rectTransform.anchorMin = anchorList[nextTarget];
            rectTransform.position = pos;
            Vector2 target = FindNextTarget(nextTarget);
            activeTween = new Tween(transform, rectTransform.anchoredPosition, target, Time.time, CalculateDuration(rectTransform.anchoredPosition, target));
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

    private Vector2 FindNextTarget(int nextTarget)
    {
        switch(nextTarget)
        {
            case 0:
                return new Vector2(canvas.rect.xMin + (rectTransform.rect.width / 2.0f), rectTransform.anchoredPosition.y);
            case 1:
                return new Vector2(rectTransform.anchoredPosition.x, canvas.rect.yMin + (rectTransform.rect.height / 2.0f));
            case 2:
                return new Vector2(canvas.rect.xMax - (rectTransform.rect.width / 2.0f), rectTransform.anchoredPosition.y);
            case 3:
                return new Vector2(rectTransform.anchoredPosition.x, canvas.rect.yMax - (rectTransform.rect.height / 2.0f));
            default:
                return Vector2.zero;
        }
    }
}
