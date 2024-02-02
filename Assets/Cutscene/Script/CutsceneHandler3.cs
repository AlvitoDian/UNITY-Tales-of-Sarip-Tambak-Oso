using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneHandler3 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("3(Dialogue)");
    }
}
