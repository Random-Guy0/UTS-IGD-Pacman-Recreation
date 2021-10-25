using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedBorderPellet : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image sprite;
    private RectTransform canvas;

    private void Start()
    {
        //not the most efficient I know, but the best way I could think of to do this
        canvas = GameObject.FindGameObjectWithTag("Animated Border").GetComponent<RectTransform>();

        //ensures that the border can fit in both 4:3 and 16:9 aspect ratios by disabling pellets that extend beyond the screens edge
        gameObject.SetActive(canvas.rect.Contains(new Vector2(Mathf.Abs(rectTransform.anchoredPosition.x) + 10.0f, Mathf.Abs(rectTransform.anchoredPosition.y) + 10.0f)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(TempDisappear());
    }

    private IEnumerator TempDisappear()
    {
        sprite.enabled = false;
        yield return new WaitForSeconds(0.85f);
        sprite.enabled = true;
    }
}
