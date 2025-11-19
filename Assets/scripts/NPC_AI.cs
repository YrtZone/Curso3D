using UnityEngine;
using System.Collections.Generic;

public class NPC_IA : MonoBehaviour
{
    [Header("Components")]
    public List<Transform> wayPoints;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator anim;
    public LayerMask playerLayer;

    [Header("Variables")]
    public int currentWaypointIndex = 0;
    public float speed = 2f;
    private bool isPlayerDetected = false;
    private bool onRadius;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim= GetComponent<Animator>();
        navMeshAgent.speed = speed;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerDetected) Walking();
        else
        {
            StopWalking();
            anim.SetTrigger("Attack");
        }
    }

    private void Walking()
    {
        if (wayPoints.Count == 0) 
        {
            Debug.Log("travado aqui");
            return;
        }

        float distanceToWaypoint = Vector3.Distance(wayPoints[currentWaypointIndex].position, transform.position);
        if (distanceToWaypoint <= 2)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Count;
        }

        navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position);
        anim.SetBool("Move", true);
        onRadius = false;
    }

    private void StopWalking()
    {
        navMeshAgent.isStopped = true;
        anim.SetBool("Move", false);
        onRadius = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detectado!");
            isPlayerDetected = true;             
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player desdetectado!");
            isPlayerDetected = false;
            navMeshAgent.isStopped = false;
        }
    }


}