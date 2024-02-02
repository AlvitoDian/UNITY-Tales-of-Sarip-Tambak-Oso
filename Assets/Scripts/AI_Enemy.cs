using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    public float Deg;


    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        if(Mathf.Abs(Vector3.Angle(transform.forward,dir))<Deg)
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
