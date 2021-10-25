using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
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
}
