using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{   
    public PlayerController playerController;
    public ThirdPersonCamera thirdPersonCamera;

    public void Setup (){
        playerController.walkSpeed = 0;
        thirdPersonCamera.mouseSensitivity = 0;
        
        gameObject.SetActive(true);
        /* Time.timeScale = 0; */
        Cursor.lockState = CursorLockMode.None;
    }

    public void Retry1Action()
    {
        SceneManager.LoadScene("1(Action)");
    }
    
    public void Retry2Action()
    {
        SceneManager.LoadScene("2(Action)");
    }
    
    public void Retry4Action()
    {
        SceneManager.LoadScene("4(Action)");
    }
    
    public void RetryDialogue()
    {
        SceneManager.LoadScene("ChapterSelect");
    }
}
