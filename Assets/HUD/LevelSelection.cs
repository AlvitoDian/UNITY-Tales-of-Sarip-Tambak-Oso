using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons;

    public void PlayChapter1(){
        SceneManager.LoadScene("1(Cutscene)");
    }
    
    public void PlayChapter2(){
        SceneManager.LoadScene("2(Cutscene)");
    }

    public void PlayChapter3(){
        SceneManager.LoadScene("3(Cutscene)");
    }

    public void PlayChapter4(){
        SceneManager.LoadScene("4(Cutscene)");
    }

    void Update()
    {
        // Jika tombol 'd' ditekan
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Hapus semua PlayerPrefs
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs deleted!");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            // Hapus semua PlayerPrefs
            PlayerPrefs.GetInt("levelAt", 6);
            Debug.Log("PlayerPrefs max!");
        }
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("levelAt", 2);
    }

    void Start()
    {
        int levelAt = GetCurrentLevel();

        Debug.Log(levelAt);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            /* if (i + 2 > levelAt)
            levelButtons[i].interactable = false; */
            if (i + 2 > levelAt)
            {
                levelButtons[i].interactable = false;
                Debug.Log("Button " + i + " is not interactable.");
            }
            else
            {
                Debug.Log("Button " + i + " is interactable.");
            }
        }
    }

}
