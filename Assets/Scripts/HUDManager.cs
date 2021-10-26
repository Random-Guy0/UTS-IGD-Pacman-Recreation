using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public void ReturnToTitleScreen()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
