using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoPause : MonoBehaviour
{
    [SerializeField] KeyCode pauseButton = KeyCode.Escape;
    [SerializeField] GameObject panel, resume, mainMenu, quit;
    private bool pause = false;
    [SerializeField] Button resumeButton;

    void Start()
    {
        resumeButton.onClick.AddListener(doResumeAndPause);
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            doResumeAndPause();
        }
        
    }

    public void doResumeAndPause()
    {
        if (!pause)
        {
            pause = true;
            Time.timeScale = 0;
            panel.SetActive(true);
            resume.SetActive(true);
            mainMenu.SetActive(true);
            quit.SetActive(true);
        } 
        else
        {
            pause = false;
            Time.timeScale = 1;
            panel.SetActive(false);
            resume.SetActive(false);
            mainMenu.SetActive(false);
            quit.SetActive(false);
        }
    }
}
