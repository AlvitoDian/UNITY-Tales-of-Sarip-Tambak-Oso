using System.Collections;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour
{
    public GameObject imageObject; // Assign your image GameObject in the Unity Editor
    public Animator anim;

    void Start()
    {
        // Menampilkan GameObject pada awal permainan
        imageObject.SetActive(true);

        // Menjalankan fungsi untuk menyembunyikan GameObject setelah 5 detik
        StartCoroutine(HideImageAfterDelay(5f));
    }

    IEnumerator HideImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Menyembunyikan GameObject setelah 5 detik
        anim.SetBool("Hide", true);
    }
}
