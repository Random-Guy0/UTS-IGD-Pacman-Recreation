using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GhostController[] ghosts;
    [SerializeField] private GameObject ghostScaredCountdown;
    [SerializeField] private TMP_Text ghostScaredCountdownText;

    private int score;
    private int scaredCountdown;

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

    public void CollectPowerPellet()
    {
        ghostScaredCountdown.SetActive(true);
        scaredCountdown = 10;
        ghostScaredCountdownText.text = scaredCountdown.ToString();
        foreach(GhostController ghost in ghosts)
        {
            ghost.ScaredState();
        }
        Invoke("GhostCountdown", 1.0f);
    }

    private void GhostCountdown()
    {
        scaredCountdown--;

        if(scaredCountdown == 3)
        {
            foreach (GhostController ghost in ghosts)
            {
                ghost.RecoveringState();
            }
        }

        if(scaredCountdown != 0)
        {
            Invoke("GhostCountdown", 1.0f);
        }
        else
        {
            foreach (GhostController ghost in ghosts)
            {
                ghost.WalkingState();
            }
            ghostScaredCountdown.SetActive(false);
        }

        ghostScaredCountdownText.text = scaredCountdown.ToString();
    }
}
