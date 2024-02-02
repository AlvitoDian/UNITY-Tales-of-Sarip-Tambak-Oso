using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


    public class PlayerStats : CharacterStats
    {
        
        public GameObject gameOverScreen;
        public HealthBar healthbar;

        Animator animator;

        public PlayerController playerController;
        public ThirdPersonCamera thirdPersonCamera;
        public string dieNextScene;
        public int levelUnlock;

        void Start()
        {   
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
            animator = GetComponent<Animator>();
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }


        //? Code Reference : Sebastian Graves
        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            /* animator.SetBool("hitted", true); */

            healthbar.SetCurrentHealth(currentHealth);

            if(currentHealth <= 0)
            {
                currentHealth = 0;

                animator.SetBool("knock", true);
                Debug.Log("Kamu Matti");
                playerController.walkSpeed = 0;
                thirdPersonCamera.mouseSensitivity = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if(levelUnlock != null)
                {
                    Debug.Log(levelUnlock);
                    PlayerPrefs.SetInt("levelAt", levelUnlock);
                    PlayerPrefs.Save();
                }
                if(dieNextScene != null)
                {
                    Invoke("LoadNextSceneWithDelay", 3f);
                }
                if(gameOverScreen != null)
                {
                    gameOverScreen.SetActive(true);
                }
                
                
            }
     
        }

        void LoadNextSceneWithDelay()
        {
            SceneManager.LoadScene(dieNextScene);
        }
    }