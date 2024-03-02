using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIBoss : MonoBehaviour
{
    public float shootRange = 10f; // Range for shooting
    public float attackRange = 2f; // Range for attacking
    public float desiredDistance = 5f; // Desired distance from the player
    public float randomMoveInterval = 3f; // Interval for random movement

    private enum BossState
    {
        Shoot,
        ZoneAttack
    }

    private BossState currentState; // Current state of the boss
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component
    private Transform playerTransform; // Reference to the player's transform
    private float nextRandomMoveTime; // Time for next random movement

    private void Start()
    {
        // Initialize the boss in the "shoot" state
        currentState = BossState.Shoot;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nextRandomMoveTime = Time.time + randomMoveInterval;
    }

    private void Update()
    {
        // Check the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Switch state based on player's distance
        if (distanceToPlayer > shootRange)
        {
            // If the player is out of shoot range, switch to shoot mode
            SwitchToShoot();
        }
        else if (distanceToPlayer <= attackRange)
        {
            // If the player is within attack range, switch to zone attack mode
            SwitchToZoneAttack();
        }

        // Perform random movement if in shooting mode
        if (currentState == BossState.Shoot && Time.time >= nextRandomMoveTime)
        {
            PerformRandomMoveAroundPlayer();
            nextRandomMoveTime = Time.time + randomMoveInterval;
        }
        else if (currentState == BossState.ZoneAttack)
        {
            MoveAwayFromPlayer();
        }
    }

    private void SwitchToShoot()
    {
        if (currentState != BossState.Shoot)
        {
            // Transition to shoot mode
            Debug.Log("Switching to Shoot mode.");
            currentState = BossState.Shoot;
            // Update the next random move time
            nextRandomMoveTime = Time.time + randomMoveInterval;
        }
    }

    private void SwitchToZoneAttack()
    {
        if (currentState != BossState.ZoneAttack)
        {
            // Transition to zone attack mode
            Debug.Log("Switching to Zone Attack mode.");
            currentState = BossState.ZoneAttack;
            MoveAwayFromPlayer();
        }
    }

    private void MoveAwayFromPlayer()
    {
        // Calculate the direction away from the player
        Vector3 directionFromPlayer = transform.position - playerTransform.position;
        // Normalize the direction and multiply it by the desired distance
        Vector3 destination = transform.position + directionFromPlayer.normalized * desiredDistance;
        // Move the boss to the destination using NavMeshAgent
        MoveTo(destination);
    }

    private void PerformRandomMoveAroundPlayer()
    {
        // Calculate a random direction around the player within the shoot range
        Vector3 randomDirection = Random.insideUnitSphere * shootRange;
        randomDirection += playerTransform.position;
        // Move the boss to the random destination using NavMeshAgent
        MoveTo(randomDirection);
    }

    private void MoveTo(Vector3 destination)
    {
        // Set the destination for NavMeshAgent
        navMeshAgent.SetDestination(destination);
    }
}
