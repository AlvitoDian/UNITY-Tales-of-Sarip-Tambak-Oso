using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{   
    EnemyManager enemyManager;
    EnemyAnimatorManager enemyAnimatorManager;
    EnemyStats enemyStats;

    public CapsuleCollider characterCollider;
    public CapsuleCollider characterCollisionBlockerCollider;
    
    NavMeshAgent navmeshAgent;
    public Rigidbody enemyRigidBody;

    public CharacterStats currentTarget;
    public LayerMask detectionLayer;

    public float distanceFromTarget;
    public float stoppingDistance = 1f;

    public float rotationSpeed = 15;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        navmeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidBody = GetComponent<Rigidbody>();
        enemyStats = GetComponentInChildren<EnemyStats>();
    }

    private void Start()
    {
        navmeshAgent.enabled = false;
        enemyRigidBody.isKinematic = false;
        Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true); //new
    }

    //? Code Reference : Sebastian Graves
    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats characterStats =  colliders[i].transform.GetComponent<CharacterStats>();

            if (characterStats != null)
            {
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    currentTarget = characterStats;
                }
            }
        }
    }
    //? Code Reference : Sebastian Graves
    public void HandleMoveToTarget()
    {   
        if (enemyManager.isPreformingAction)
            return;
            
        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        // Preforming an action, stop our movement
        if (enemyManager.isPreformingAction)
        {
            //Animator Launch Here...>>>     ....
            enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            navmeshAgent.enabled =  false;
        }
        else
        {
            if (distanceFromTarget > stoppingDistance)
            {
                //Animator Launch Here...>>>     ....
                enemyAnimatorManager.anim.SetFloat("Vertical", 0.5f, 0.1f, Time.deltaTime);
                //navmeshAgent.SetDestination(currentTarget.transform.position);
            }
            else if (distanceFromTarget <= stoppingDistance)
            {
                //navmeshAgent.SetDestination(currentTarget.transform.position);
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
        }
        if (enemyStats.currentHealth > 0)
        {
            HandleRotateTowardsTarget();
        }
        navmeshAgent.transform.localPosition = Vector3.zero;
        navmeshAgent.transform.localRotation = Quaternion.identity;
    }

    //? Code Reference : Sebastian Graves
    private void HandleRotateTowardsTarget()
    {
        //Rotate Manually
        if (enemyManager.isPreformingAction)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
        }
        // Rotate with pathfinding (navmesh)
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(navmeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyRigidBody.velocity;

            navmeshAgent.enabled =  true;
            navmeshAgent.SetDestination(currentTarget.transform.position);
            enemyRigidBody.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navmeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }        
    }
}
