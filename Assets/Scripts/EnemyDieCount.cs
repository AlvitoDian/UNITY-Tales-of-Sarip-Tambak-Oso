using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyDieCount : MonoBehaviour
{
    private int enemyCount = 0;
    public string sceneName = "SceneDialogueVN";
    public float delayBeforeSceneLoad = 3.0f;
    private bool isChangingScene = false;

    private void Start()
    {
        // Temukan semua musuh dalam scene menggunakan tag "Enemy".
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;

        // Cetak jumlah musuh yang ditemukan ke konsol.
        Debug.Log("Jumlah musuh yang ditemukan: " + enemyCount);
    }

    private void Update()
    {
        if (enemyCount <= 0 && !isChangingScene)
        {
            // Mulai Coroutine untuk mengatur jeda sebelum mengganti scene.
            Debug.Log("Mush Mati Semua");
            StartCoroutine(LoadSceneWithDelay());
            isChangingScene = true;
            /* UnlockCursor(); */
        }
    }


    public void EnemyDied()
    {
        enemyCount--;
        Debug.Log("Jumlah musuh yang matot: " + enemyCount);


      /*   if (enemyCount <= 0)
        {

            LoadSceneWithDelay();
            UnlockCursor();
        } */
    }

    private IEnumerator LoadSceneWithDelay()
    {
   
        yield return new WaitForSeconds(delayBeforeSceneLoad);


        SceneManager.LoadScene(sceneName);
    }

    /* private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    } */
}
