using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GhostController[] ghosts;
    [SerializeField] private GameObject ghostScaredCountdown;
    [SerializeField] private TMP_Text ghostScaredCountdownText;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject[] lifeIndicators;
    [SerializeField] private TMP_Text gameTimer;
    [SerializeField] private GameObject startCountdown;
    [SerializeField] private TMP_Text startCountdownText;
    [SerializeField] private PacStudentController pacStudent;

    private int score;
    private int scaredCountdown;
    private int lives;
    private bool gameStarted;
    private float startTime;
    private int gameStartCountdown;

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        lives = 3;
        gameStarted = false;

        gameStartCountdown = 3;
        startCountdownText.text = gameStartCountdown.ToString();
        audioManager.IntroMusic();
        Invoke("GameStartCountdown", 1.0f);
    }

    private void Update()
    {
        UpdateGameTimer();
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void CollectPowerPellet()
    {
        audioManager.GhostScaredState();
        ghostScaredCountdown.SetActive(true);
        scaredCountdown = 10;
        ghostScaredCountdownText.text = scaredCountdown.ToString();
        foreach(GhostController ghost in ghosts)
        {
            ghost.ScaredState();
        }
        CancelInvoke("GhostCountdown");
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
            audioManager.GhostNormalState();
        }

        ghostScaredCountdownText.text = scaredCountdown.ToString();
    }

    public bool HasLives()
    {
        return lives > 0;
    }

    public void LoseLife()
    {
        lives--;
        Destroy(lifeIndicators[lives]);
    }

    private void UpdateGameTimer()
    {
        if(gameStarted)
        {
            gameTimer.text = TimeSpan.FromSeconds(Time.time - startTime).ToString(@"mm\:ss\:ff");
        }
    }

    private void StartGame()
    {
        startCountdown.SetActive(false);
        gameStarted = true;
        startTime = Time.time;
        pacStudent.enabled = true;
        foreach(GhostController ghost in ghosts)
        {
            ghost.enabled = true;
        }

        audioManager.GhostNormalState();
    }

    private void GameStartCountdown()
    {
        gameStartCountdown--;

        if (gameStartCountdown > 0)
        {
            startCountdownText.text = gameStartCountdown.ToString();
            Invoke("GameStartCountdown", 1.0f);
        }
        else
        {
            startCountdownText.text = "GO!";
            Invoke("StartGame", 1.0f);
        }
    }
}
