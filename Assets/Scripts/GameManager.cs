using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private int score;

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
