using UnityEngine;
using UnityEngine.SceneManagement;

public class AI_Enemy_FOV : MonoBehaviour
{   
    public GameOverScreen GameOverScreen;

    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;

    // AI Movement
    Animator animator;
   
    public float moveSpeed = 0.2f;
 
    Vector3 stopPosition;
 
    float walkTime;
    public float walkCounter;
    float waitTime;
    public float waitCounter;
 
    int WalkDirection;
 
    public bool isWalking;

    private bool isMovingForward = true;

    public void GameOver(){
        GameOverScreen.Setup();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
 
        //So that all the prefabs don't move/stop at the same time
        walkTime = Random.Range(3,6);
        waitTime = Random.Range(5,7);
 
 
        waitCounter = waitTime;
        walkCounter = walkTime;
 
        ChooseDirection();
    }


    private void Update()
    {
        LookForPlayer();

        if (isWalking)
        {
 
            // animator.SetBool("isRunning", true);
 
            walkCounter -= Time.deltaTime;
 
            switch (WalkDirection)
            {
                case  0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    /* animator.SetFloat("Vertical", 1.0f); */
                    break;
                case  1:
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    /* animator.SetFloat("Vertical", 1.0f); */
                    break;
                /* case  2:
                    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case  3:
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break; */
            }
 
            if (walkCounter <= 0)
            {
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;
                //stop movement

                animator.SetBool("isWalking", false);
                transform.position = stopPosition;
                // animator.SetBool("isRunning", false);
                //reset the waitCounter
                waitCounter = waitTime;
            }
 
 
        }
        else
        {
 
            waitCounter -= Time.deltaTime;
 
            if (waitCounter <= 0)
            {
                ChooseDirection();
            }
        }
    }

    //? Code Reference : Sebastian Graves
    private PlayerController LookForPlayer()
    {
        if (PlayerController.Instance == null)
        {
            return null;
        }

        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = PlayerController.Instance.transform.position - enemyPosition;
        toPlayer.y = 0;

        if (toPlayer.magnitude <= detectionRadius)
        {
            if (Vector3.Dot(toPlayer.normalized, transform.forward) >
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad)) {

                // Debug.Log("Player has been detected!");
                GameOver();
                Cursor.visible = true;
                
                return PlayerController.Instance;
            }
        }


        return null;
    }

    public void ChooseDirection()
    {
        /* WalkDirection = Random.Range(0, 4); */
        if (isMovingForward)
        {
            WalkDirection = 0; // Move forward
        }
        else
        {
            WalkDirection = 1; // Move to the right
        }

        isMovingForward = !isMovingForward;
 
        isWalking = true;
        walkCounter = walkTime;

         animator.SetBool("isWalking", true);
    }

    


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Color c = new Color(0.8f, 0, 0, 0.4f);
        UnityEditor.Handles.color = c;

        Vector3 rotatedForward = Quaternion.Euler(
            0,
            -detectionAngle * 0.5f,
            0) * transform.forward;

        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            rotatedForward,
            detectionAngle,
            detectionRadius);

    }
#endif
}