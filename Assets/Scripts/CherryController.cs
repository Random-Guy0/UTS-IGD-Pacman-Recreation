using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField] private GameObject cherry;
    [SerializeField] private Tweener tweener;
    [SerializeField] private float duration;

    private Camera cam;

    private GameObject currentCherry;

    private void Start()
    {
        Invoke("SpawnCherry", 10.0f);
        cam = Camera.main;
    }

    private void SpawnCherry()
    {
        Vector2 maxScreenPoint = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        float randomX = Random.Range(-maxScreenPoint.x - 0.5f, maxScreenPoint.x + 0.5f);
        float randomY = Random.Range(-maxScreenPoint.y - 0.5f, maxScreenPoint.y + 0.5f);

        int sideToSpawnOn = Random.Range(0, 4);
        Vector2 startPos = Vector2.zero;

        switch (sideToSpawnOn)
        {
            case 0:
                startPos = new Vector2(-maxScreenPoint.x - 0.5f, randomY);
                break;
            case 1:
                startPos = new Vector2(maxScreenPoint.x + 0.5f, randomY);
                break;
            case 2:
                startPos = new Vector2(randomX, -maxScreenPoint.y - 0.5f);
                break;
            case 3:
                startPos = new Vector2(randomX, maxScreenPoint.y + 0.5f);
                break;
        }

        currentCherry = Instantiate(cherry, startPos, Quaternion.identity);
        Vector2 endPos = -startPos;
        tweener.AddTween(currentCherry.transform, startPos, endPos, duration, null);

        Invoke("DestroyCherry", duration + 0.1f);
        Invoke("SpawnCherry", 10.0f);
    }

    private void DestroyCherry()
    {
        Destroy(currentCherry);
    }
}
