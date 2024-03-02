using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 3.5f;
    public float speedRun = 5.5f;

    public float viewRadius = 10;
    public float viewAngle = 120;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public float meshResolution = 2;
    public int edgeResolveIterations = 4;
    public float edgeDistance = 0.5f;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_waitTime;
    float m_timeToRotate;
    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_isPatrolling;
    bool m_caughtPlayer;


    // Start is called before the first frame update
    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_isPatrolling = true;
        m_caughtPlayer = false;
        m_PlayerInRange = false;
        m_waitTime = startWaitTime;
        m_timeToRotate = timeToRotate;

        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(RandomPoint());
    }

    // Update is called once per frame
    void Update()
    {
        EnvironmentView();

        if (m_isPatrolling)
        {
            Patroling();
        }
        else
        {
            Chasing();
        }
    }

    private void Patroling()
    {
        if (m_PlayerNear)
        {
            if (timeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                timeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_waitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_waitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_waitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;
        //Chase the player and if the player is out of range, wait for a few seconds and then go back to patrolling
        if (m_PlayerInRange)
        {
            if (m_PlayerPosition != Vector3.zero)
            {
                Move(speedRun);
                LookingPlayer(m_PlayerPosition);
                m_PlayerNear = true;
                playerLastPosition = m_PlayerPosition;
            }
        }
        else
        {
            m_isPatrolling = true;
            m_PlayerNear = false;
            m_PlayerPosition = Vector3.zero;
            m_waitTime = startWaitTime;
            m_timeToRotate = timeToRotate;
        }
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    public void NextPoint()
    {
        navMeshAgent.SetDestination(RandomPoint());
    }

    void CaughtPlayer()
    {
        m_caughtPlayer = true;
    }

    void LookingPlayer(Vector3 playerPosition)
    {
        navMeshAgent.SetDestination(playerPosition);
        if (Vector3.Distance(transform.position, playerPosition) < 2)
        {
            if (m_waitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(RandomPoint());
                m_waitTime = startWaitTime;
                m_timeToRotate = timeToRotate;


            }
            else
            {
                Stop();
                m_waitTime -= Time.deltaTime;
            }
        }
    }

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_isPatrolling = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
            }
            if (m_PlayerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }
    }

    //Get a random point in the navmesh
    public Vector3 RandomPoint()
    {
        //Get a random point in the navmesh at least 10 units away from the AI
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
        return hit.position;

    }
}