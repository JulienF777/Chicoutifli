using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class S_Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 4f;
    [SerializeField] private GameObject _playerMesh;
    [SerializeField] private GameObject _playerCamera;

    //Rotation
    [SerializeField] private float _rotationSmoothness = 5f;

    void Start()
    {

    }


    void Update()
    {
        playerMovement();
        playerRotation();
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
}
