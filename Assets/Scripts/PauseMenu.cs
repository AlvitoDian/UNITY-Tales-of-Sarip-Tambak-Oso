using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{   
    public GameObject pauseScreen;
    public GameObject settingPanel;
    public bool cursorShowAfterPaused;

    void Start()
    {
        settingPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Settings(){
        settingPanel.SetActive(true);
    }
    
    public void Back(){
        settingPanel.SetActive(false);
    }

    public void Pause()
    {   
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Paused");
    }

    public void Continue()/*  */
    {   
        Cursor.visible = cursorShowAfterPaused;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        /* Cursor.lockState = CursorLockMode.Locked; */
    }
    
    public void ExitMainMenu()
    {   
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
