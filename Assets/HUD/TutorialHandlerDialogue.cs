using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHandlerDialogue : MonoBehaviour
{
    public GameObject tutorialPanel;

    public void DoneTutorial(){
        tutorialPanel.SetActive(false);
    }
}
