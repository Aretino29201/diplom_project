using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform playerT;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool isWalkPointSet;
    public float walkPointRange;

    //Atacking
    public float timeBetweenAtack;
    public bool isAtacking;

    //States
    public float sightRange, attackRange;
    public bool playerInSight, playerInAtkRange, isPlayerInvis;

    public GameObject projectile;
    public float projectileFwdSpeed = 32, projectileUpSpeed =  8; 


    private void Awake()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        agent= GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAtkRange = Physics.Raycast(transform.position, transform.forward, attackRange, whatIsPlayer);// Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSight && !playerInAtkRange || isPlayerInvis) Patroling(2f);
        else if(playerInSight && !playerInAtkRange && !isPlayerInvis) ChasePlayer();
        else if(playerInAtkRange && !isPlayerInvis)AttackPlayer();
    }

    //State func

    private void Patroling(float radius)
    {
        if( !isWalkPointSet) { SearchWalkPoint(); }
        else agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint= transform.position - walkPoint;
        //елси дистанция меньше метра, то ищем новую точку
        if(distanceToWalkPoint.magnitude <= radius)
        {
            isWalkPointSet = false;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerT.position);
        transform.LookAt(playerT);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerT);
        Patroling(0f);
        
        if (!isAtacking)
        {
            
            //atack code
            Rigidbody rb = Instantiate(projectile,new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * projectileFwdSpeed, ForceMode.Impulse);
            rb.AddForce(transform.up * projectileUpSpeed, ForceMode.Impulse);
            //
            isAtacking = true;
            Invoke("ResetAtack", timeBetweenAtack);
        }
    }

    private void SearchWalkPoint()
    {// generate random point on navmesh
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
        {
            isWalkPointSet= true;
        }
    }

    void ResetAtack()
    {
        isAtacking= false;
    }
}
