using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIBoss : MonoBehaviour
{
    public float shootRange = 10f; // Range for shooting
    public float attackRange = 2f; // Range for attacking
    public float desiredDistance = 5f; // Desired distance from the player
    public float randomMoveInterval = 3f; // Interval for random movement
    public float rotationSpeed = 5f; // Speed of rotation
    public float shootCooldown = 2f; // Cooldown for shooting
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public GameObject hitPrefab;
    public Rigidbody rb;
    public float hitDamage;
    public float attackCooldown = 3;
    public float health = 100;


    private bool m_canAttack;
    private bool m_canShoot;
    private GameObject _meshAnim;

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
        m_canAttack = true;
        m_canShoot = true;
        _meshAnim = transform.GetChild(0).gameObject;
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
        if (Time.time >= nextRandomMoveTime)
        {
            PerformRandomMoveAroundPlayer();
            nextRandomMoveTime = Time.time + randomMoveInterval;
        }
        if (currentState == BossState.Shoot && m_canShoot)
        {
            RotateTowardsPlayer();
            Shoot();
            StartCoroutine(shootCooldownTimer(shootCooldown));
        }
        else if (currentState == BossState.ZoneAttack)
        {
            Attack();
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

    private void Shoot()
    {
        // Create a bullet with size divided by 2
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.LookAt(playerTransform);
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

    private void RotateTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        // Rotate towards the player's direction
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveTo(Vector3 destination)
    {
        // Set the destination for NavMeshAgent
        navMeshAgent.SetDestination(destination);
        _meshAnim.GetComponent<Animator>().SetBool("isRunning", true);
    }

    private void Attack()
    {

        
        if (m_canAttack)
        {
            StartCoroutine(attackCouldown(attackCooldown));
            _meshAnim.GetComponent<Animator>().SetBool("isAttack", true);
            _meshAnim.GetComponent<Animator>().SetBool("isRunning", false);
        } else
        {
            _meshAnim.GetComponent<Animator>().SetBool("isAttack", false);
        }


    }

    private IEnumerator attackCouldown(float cooldown)
    {
        m_canAttack = false;
        yield return new WaitForSeconds(cooldown);
        Vector3 hitPosition = navMeshAgent.transform.position;
        GameObject hitEffect = Instantiate(hitPrefab, hitPosition, navMeshAgent.transform.rotation);
        Collider[] hitColliders = Physics.OverlapSphere(hitPosition, attackRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Player")
            {
                Debug.Log("Player prends des damages");
                hitColliders[i].gameObject.GetComponent<S_Player>().TakeDamage(hitDamage);
                //Repulse the player
                Vector3 repulseDirection = hitColliders[i].transform.position - transform.position;
                repulseDirection.y = 0;
                hitColliders[i].gameObject.GetComponent<S_Player>().RepulsePlayer(repulseDirection);
            }
        }
        Destroy(hitEffect, 0.1f);
        m_canAttack = true;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}


    private IEnumerator shootCooldownTimer(float cooldown)
    {
        m_canShoot = false;
        yield return new WaitForSeconds(cooldown);
        m_canShoot = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Destroy(GameObject.Find("Player"));
            Destroy(GameObject.Find("Quetes"));
            SceneManager.LoadScene("MenuFinJeu");
        }
    }

    public void RepulseEnemy(Vector3 repulseDirection)
    {
        rb.AddForce(repulseDirection.normalized * 5, ForceMode.Impulse);
        StartCoroutine(StopRepulsion(0.5f));
    }

    public void RepulseEnemyBasic(Vector3 repulseDirection)
    {
        rb.AddForce(repulseDirection.normalized * 2, ForceMode.Impulse);
        StartCoroutine(StopRepulsion(0.5f));
    }

    private IEnumerator StopRepulsion(float time)
    {
        yield return new WaitForSeconds(time);
        rb.velocity = Vector3.zero;
    }
}

