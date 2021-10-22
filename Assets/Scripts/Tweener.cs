using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    //private Tween activeTween;

    private List<Tween> activeTweens = new List<Tween>();

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        bool exists = TweenExists(targetObject);
        if(!exists)
        {
            activeTweens.Add(new Tween(targetObject, startPos, endPos, Time.time, duration));
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        for (int i = 0; i < activeTweens.Count; i++)
        {
            if (Vector3.Distance(activeTweens[i].Target.position, activeTweens[i].EndPos) > 0.1f)
            {
                float t = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                t = t * t * t;
                activeTweens[i].Target.position = Vector3.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, t);
            }
            else
            {
                activeTweens[i].Target.position = activeTweens[i].EndPos;
                activeTweens.RemoveAt(i);
                i--;
            }
        }
    }

    public bool TweenExists(Transform target)
    {
        bool result = false;
        foreach (Tween tween in activeTweens)
        {
            if(tween.Target == target)
            {
                result = true;
                break;
            }
        }

        return result;
    }
}
