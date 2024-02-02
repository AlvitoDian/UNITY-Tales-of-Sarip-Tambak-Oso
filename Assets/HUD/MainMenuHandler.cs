using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{   
    public GameObject settingPanel;
    public GameObject creditsPanel;
    public GameObject mainMenuPanel;

    void Start()
    {
        settingPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
    
    public void BackToMainMenu(){
        settingPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
    public void Play(){
        SceneManager.LoadScene("ChapterSelect");
    }

    public void FreeRoam(){
        SceneManager.LoadScene("Scene");
    }
    
    public void Settings(){
        settingPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    
    public void Credits(){
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    
    public void QuitGame(){
        Application.Quit();
    }
}
