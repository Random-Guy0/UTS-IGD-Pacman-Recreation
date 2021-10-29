using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private List<Tween> activeTweens = new List<Tween>();

    public bool AddTween(Transform target, Vector3 startPos, Vector3 endPos, float duration, ITweenableObject targetObject)
    {
        bool exists = TweenExists(target);
        if(!exists)
        {
            activeTweens.Add(new Tween(target, startPos, endPos, Time.time, duration, targetObject));
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
            if (Vector3.Distance(activeTweens[i].Target.position, activeTweens[i].EndPos) > 0.01f)
            {
                float t = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                activeTweens[i].Target.position = Vector3.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, t);
            }
            else
            {
                activeTweens[i].Target.position = activeTweens[i].EndPos;
                if (activeTweens[i].TargetObject != null)
                {
                    activeTweens[i].TargetObject.NextTween();
                }
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

    private Tween FindTween(Transform target)
    {
        Tween result = null;
        foreach (Tween tween in activeTweens)
        {
            if (tween.Target == target)
            {
                result = tween;
                break;
            }
        }

        return result;
    }

    public bool CancelTween(Tween tween)
    {
        if (tween != null)
        {
            activeTweens.Remove(tween);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CancelTween(Transform target)
    {
        Tween tween = FindTween(target);
        return CancelTween(tween);
    }
}
