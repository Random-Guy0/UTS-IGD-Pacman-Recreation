using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject gameOverTxt;

    private int score;
    private int scaredCountdown;
    private int lives;
    private bool gameInProgress;
    private float startTime;
    private int gameStartCountdown;
    private int pelletCount;

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        lives = 3;
        gameInProgress = false;
        pelletCount = 218;

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

        if(lives == 0)
        {
            GameOver();
        }
    }

    private void UpdateGameTimer()
    {
        if(gameInProgress)
        {
            gameTimer.text = TimeSpan.FromSeconds(Time.time - startTime).ToString(@"mm\:ss\:ff");
        }
    }

    private void StartGame()
    {
        startCountdown.SetActive(false);
        gameInProgress = true;
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

    public void EatPellet()
    {
        pelletCount--;
        AddScore(10);

        if(pelletCount == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverTxt.SetActive(true);
        pacStudent.enabled = false;
        foreach (GhostController ghost in ghosts)
        {
            ghost.enabled = false;
        }
        gameInProgress = false;

        float finalTime = Time.time - startTime;

        int previousHighScore = PlayerPrefs.GetInt("HighScore", 0);
        float previousBestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (score > previousHighScore || (score == previousHighScore && finalTime < previousBestTime))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.SetFloat("BestTime", finalTime);
            PlayerPrefs.Save();
        }

        Invoke("ReturnToStartScene", 3.0f);
    }

    private void ReturnToStartScene()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
