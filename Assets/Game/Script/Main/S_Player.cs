using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class S_Player : MonoBehaviour
{
    //Child
    [SerializeField] private GameObject _playerMesh;
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private Rigidbody _playerRigidbody;

    //Movement
    [SerializeField] private float _playerSpeed = 4f;

    //Rotation
    [SerializeField] private float _rotationSmoothness = 5f;

    //Fight
    [SerializeField] private GameObject _hitPrefab;
    [SerializeField] private float _timeBeforeDestroy = 1f;
    [SerializeField] private float _hitRange = 2f;
    [SerializeField] private float _hitCooldown = 1f;
    private bool _canAttack = true;
    [SerializeField] private float _hitDamage = 1f;

    //Health
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    void Start()
    {
        initPlayer();
    }


    void Update()
    {
        playerMovement();
        playerRotation();
        playerFight();
    }

    private void initPlayer()
    {
        _canAttack = true;
        _currentHealth = _maxHealth;
    }

    private void playerMovement()
    {
        // Obtenir la rotation actuelle de la cam�ra
        Quaternion cameraRotation = _playerCamera.transform.rotation;

        // R�initialiser la composante Y de la rotation de la cam�ra
        cameraRotation = Quaternion.Euler(0, cameraRotation.eulerAngles.y, 0);

        // D�finir les axes de mouvement en fonction de la rotation de la cam�ra
        Vector3 forward = cameraRotation * Vector3.forward;
        Vector3 right = cameraRotation * Vector3.right;

        // Calculer les mouvements horizontal et vertical
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 horizontal = right * horizontalInput * _playerSpeed * Time.deltaTime;
        Vector3 vertical = forward * verticalInput * _playerSpeed * Time.deltaTime;

        // Appliquer le mouvement relatif � la cam�ra
        transform.position += horizontal + vertical;
    }

    private void playerRotation()
    {
        Ray ray = _playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Obtenir la direction du joueur vers le point touch� par le rayon
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f; // Gardez la rotation uniquement sur l'axe horizontal

            // Calculer et appliquer la rotation vers la direction
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _playerMesh.transform.rotation = Quaternion.Slerp(_playerMesh.transform.rotation, targetRotation, Time.deltaTime * _rotationSmoothness);
            }
        }
    }

    private void playerFight()
    {
        if (Input.GetMouseButtonDown(0) && _canAttack)
        {
            // Instantie le coup

            Vector3 hitPosition = _playerMesh.transform.position + _playerMesh.transform.forward * _hitRange;
            GameObject hitEffect = Instantiate(_hitPrefab, hitPosition, _playerMesh.transform.rotation);
            //Check if he touch an enemy
            Collider[] hitColliders = Physics.OverlapSphere(hitPosition, _hitRange);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.tag == "Enemy")
                {
                    hitColliders[i].gameObject.GetComponent<AIController>().TakeDamage(_hitDamage);
                    //Repulse the enemy
                    Vector3 repulseDirection = hitColliders[i].transform.position - transform.position;
                    repulseDirection.y = 0;
                    hitColliders[i].gameObject.GetComponent<AIController>().RepulseEnemyBasic(repulseDirection);
                }
            }
            StartCoroutine(attackCouldown(_hitCooldown));
            Destroy(hitEffect, _timeBeforeDestroy);
        }
        //If right click, the player will attack in zone and inpulse the enemies
        if (Input.GetMouseButtonDown(1) && _canAttack)
        {
            _hitPrefab.transform.localScale = new Vector3(_hitRange, _hitRange, _hitRange);
            // Instantie le coup
            GameObject hitEffect = Instantiate(_hitPrefab, _playerMesh.transform.position, _playerMesh.transform.rotation);
            //Check if he touch an enemy
            Collider[] hitColliders = Physics.OverlapSphere(_playerMesh.transform.position, _hitRange);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.tag == "Enemy")
                {
                    hitColliders[i].gameObject.GetComponent<AIController>().TakeDamage(_hitDamage);
                    //Repulse the enemy
                    Vector3 repulseDirection = hitColliders[i].transform.position - transform.position;
                    repulseDirection.y = 0;
                    hitColliders[i].gameObject.GetComponent<AIController>().RepulseEnemy(repulseDirection);
                }
            }
            StartCoroutine(attackCouldown(_hitCooldown));
            Destroy(hitEffect, _timeBeforeDestroy);
        }
    }

    private IEnumerator attackCouldown(float cooldown)
    {
        _canAttack = false;
        yield return new WaitForSeconds(cooldown);
        _canAttack = true;
    }

    public void setHitDamage(float newHitDamage)
    {
        _hitDamage = newHitDamage;
    }

    public void setMaxHealth(float newMaxHealth)
    {
        _maxHealth = newMaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Debug.Log("Player is dead");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_playerMesh.transform.position, _hitRange);
    }

    public void RepulsePlayer(Vector3 repulseDirection)
    {
        //Repulse the player
        _playerRigidbody.AddForce(repulseDirection.normalized * 5, ForceMode.Impulse);
        //Stop the impulse after 0.5s
        StartCoroutine(stopImpulse(0.5f));
    }

    private IEnumerator stopImpulse(float time)
    {
        yield return new WaitForSeconds(time);
        _playerRigidbody.velocity = Vector3.zero;
    }
}
