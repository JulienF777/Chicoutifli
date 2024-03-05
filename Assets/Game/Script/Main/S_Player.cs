using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class S_Player : MonoBehaviour
{
    //Child
    [SerializeField] public GameObject _playerMesh;
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
    [SerializeField] private float _hitCooldown = 2f;
    private bool _canAttack = true;
    [SerializeField] private float _hitDamage = 1f;

    //Health
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    //UI
    public UIDocument HUD;

    //VFX
    public GameObject VFXContainer;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HUD = GameObject.Find("Statistiques").GetComponent<UIDocument>();
        _currentHealth = _maxHealth;
        HUD.rootVisualElement.Q<ProgressBar>("HP").value = _maxHealth;
        HUD.rootVisualElement.Q<Label>("DMGvalue").text = _hitDamage.ToString();
        HUD.rootVisualElement.Q<Label>("ASvalue").text = _hitCooldown.ToString();
        HUD.rootVisualElement.Q<Label>("MSvalue").text = _playerSpeed.ToString();
        if (scene.name == "Alpha")
        {
            transform.position = new Vector3(-6, 1, -36);
        }
    }

    void Start()
    {
        initPlayer();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        playerMovement();
        playerRotate();
        playerFight();
        playerAnimation();
    }

    private void initPlayer()
    {
        _canAttack = true;
        _currentHealth = _maxHealth;
    }

    private void playerMovement()
    {
        // Obtenir la rotation actuelle de la caméra
        Quaternion cameraRotation = _playerCamera.transform.rotation;

        // Réinitialiser la composante Y de la rotation de la caméra
        cameraRotation = Quaternion.Euler(0, cameraRotation.eulerAngles.y, 0);

        // Définir les axes de mouvement en fonction de la rotation de la caméra
        Vector3 forward = cameraRotation * Vector3.forward;
        Vector3 right = cameraRotation * Vector3.right;

        // Calculer les mouvements horizontal et vertical
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Combine horizontal and vertical movement
        Vector3 movement = right * horizontalInput + forward * verticalInput;

        // Normalize the movement vector to ensure consistent speed
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // Set the velocity of the Rigidbody to the normalized movement vector multiplied by speed
        _playerRigidbody.velocity = movement * _playerSpeed;
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
                _playerMesh.transform.rotation = targetRotation;
            }
        }
    }

    private void playerFight()
    {
        if (Input.GetMouseButtonDown(0) && _canAttack)
        {
            _playerMesh.GetComponent<Animator>().SetBool("isHitting", true);
            playerRotation();
            // Instantie le coup
            VFXContainer.GetComponent<VisualEffect>().Play();
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
                else if (hitColliders[i].gameObject.tag == "Boss")
                {
                    hitColliders[i].gameObject.GetComponent<AIBoss>().TakeDamage(_hitDamage);
                    //Repulse the enemy
                    Vector3 repulseDirection = hitColliders[i].transform.position - transform.position;
                    repulseDirection.y = 0;
                    hitColliders[i].gameObject.GetComponent<AIBoss>().RepulseEnemyBasic(repulseDirection);
                }
            }
            StartCoroutine(attackCouldown(_hitCooldown));
            Destroy(hitEffect, _timeBeforeDestroy);
        } else if (Input.GetMouseButtonDown(1) && _canAttack) //If right click, the player will attack in zone and inpulse the enemies
        {
            _playerMesh.GetComponent<Animator>().SetBool("isHitting", true);
            playerRotation();
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

                else if (hitColliders[i].gameObject.tag == "Boss")
                {
                    hitColliders[i].gameObject.GetComponent<AIBoss>().TakeDamage(_hitDamage);
                    //Repulse the enemy
                    Vector3 repulseDirection = hitColliders[i].transform.position - transform.position;
                    repulseDirection.y = 0;
                    hitColliders[i].gameObject.GetComponent<AIBoss>().RepulseEnemy(repulseDirection);
                }
            }
            StartCoroutine(attackCouldown(_hitCooldown));
            Destroy(hitEffect, _timeBeforeDestroy);
        } else {
            _playerMesh.GetComponent<Animator>().SetBool("isHitting", false);
        }
    }

    private IEnumerator attackCouldown(float cooldown)
    {
        _canAttack = false;
        yield return new WaitForSeconds(cooldown);
        _canAttack = true;
    }

    //Setters
    public void setSpeed(float newSpeed)
    {
        _playerSpeed = newSpeed;
    }

    public void setHitCooldown(float newCooldown)
    {
        _hitCooldown = newCooldown;
    }
    public void setHitDamage(float newHitDamage)
    {
        _hitDamage = newHitDamage;
    }

    public void setMaxHealth(float newMaxHealth)
    {
        _maxHealth = newMaxHealth;
    }

    //Getters
    public float getMaxHealth()
    {
        return _maxHealth;
    }
    public float getSpeed()
    {
        return _playerSpeed;
    }
    public float getHitDamage()
    {
        return _hitDamage;
    }
    public float getHitCooldown()
    {
        return _hitCooldown;
    }


    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        HUD.rootVisualElement.Q<ProgressBar>("HP").value = _currentHealth / _maxHealth * 100;
        Debug.Log(HUD.rootVisualElement.Q<ProgressBar>("HP").value);
        Debug.Log("Current Health : " + _currentHealth + " / " + _maxHealth + "\nDamage taken : " + damage);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Debug.Log("Player is dead");
            SceneManager.LoadScene("MenuMort");
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
        //_playerCamera.transform.position = _playerCamera.transform.position - repulseDirection.normalized;
        //Stop the impulse after 0.5s
        StartCoroutine(stopImpulse(0.5f));
    }

    private IEnumerator stopImpulse(float time)
    {
        yield return new WaitForSeconds(time);
        _playerRigidbody.velocity = Vector3.zero;
    }

    private void playerAnimation()
    {
        Animator animator = _playerMesh.GetComponent<Animator>();
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

    }

    private void playerRotate()
    {
        //Debug.Log(_playerRigidbody.velocity.magnitude);
        // Vérifier si le joueur se déplace (vérifier si la vitesse est supérieure à un seuil)
        if (_playerRigidbody.velocity.magnitude >= 0.1f)
        {
            // Calculer la direction de déplacement du joueur en supprimant la composante y (pour ne pas incliner le joueur)
            Vector3 direction = new Vector3(_playerRigidbody.velocity.x, 0f, _playerRigidbody.velocity.z);

            // Tourner le joueur pour faire face à la direction de déplacement
            Quaternion targetLoc = Quaternion.LookRotation(direction);
            _playerMesh.transform.rotation = Quaternion.Slerp(_playerMesh.transform.rotation, targetLoc, Time.deltaTime * _rotationSmoothness);
        }
    }

}
