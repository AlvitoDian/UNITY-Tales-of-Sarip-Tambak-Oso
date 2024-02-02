using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    public GameObject tutorialPanel;
    public Animator anim;

    void Start()
    {   
        tutorialPanel.SetActive(false);

        StartCoroutine(TutorialAppear(1f));
    }

    IEnumerator TutorialAppear(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Tutor");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        tutorialPanel.SetActive(true);
        
        StartCoroutine(Freeze(0.7f));
    }

    IEnumerator Freeze(float delay)
    {
        yield return new WaitForSeconds(delay);

        Time.timeScale = 0;
    }

    public void DoneTutorial(){
        tutorialPanel.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
