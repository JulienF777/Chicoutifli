using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBeforeDestroy;

    private void Start()
    {
        Destroy(gameObject, _timeBeforeDestroy);
    }
    void Update()
    {
        moveProjectile();
    }

    private void moveProjectile()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<S_Player>();
            Destroy(gameObject);
        }
    }
}
