using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoScene : MonoBehaviour
{
    public void SceneLoader(int index)
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
