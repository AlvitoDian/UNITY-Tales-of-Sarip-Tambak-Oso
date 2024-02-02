using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    
    /* public string sceneName; */
    public Image black;
    public Animator anim;


    public IEnumerator Fading(string scene)
    {
        /* Debug.Log("Fading method called with scene: " + scene); */
        anim.SetBool("fadeOut", true);
        yield return new WaitUntil(()=>black.color.a==1);
        SceneManager.LoadScene(scene);
    }
    
    
}
