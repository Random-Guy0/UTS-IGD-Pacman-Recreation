using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreTxt;
    [SerializeField] private TMP_Text bestTimeTxt;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0.0f);

        highScoreTxt.text = "High Score: " + highScore;
        bestTimeTxt.text = "Best Time: " + TimeSpan.FromSeconds(bestTime).ToString(@"mm\:ss\:ff");
    }

    public void LoadLevelOne()
    {
        LoadScene(1);
    }

    public void LoadLevelTwo()
    {
        LoadScene(2);
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void Exit()
    {
        Application.Quit();
    }    
}
