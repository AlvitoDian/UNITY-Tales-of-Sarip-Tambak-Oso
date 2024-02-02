using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStats : CharacterStats
{
        public EnemyDieCount enemyDieCount;
        Animator animator;
        public float destroyDelay = 2f;  // Delay in seconds before destroying the enemy object


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Start()
        {   
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            animator.SetBool("hitted", true);

            if(currentHealth <= 0)
            {
                currentHealth = 0;

                
                animator.SetBool("knock", true);
                Invoke("DestroyEnemy", destroyDelay);
                 // Invoke the DestroyEnemy method with a delay
                //Destroy(gameObject); //Coba Destroy GameObject
                /* SceneManager.LoadScene("SceneDialogueVN"); */
                
            }
     
        }
      

        private void DestroyEnemy()
        {
            Destroy(gameObject); // Destroy the enemy object
            enemyDieCount.EnemyDied();
        }

        /* private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Weapon"))
            {
                animator.SetBool("hitted", true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Weapon"))
            {
                animator.SetBool("hitted", false);
            }
        } */
}
