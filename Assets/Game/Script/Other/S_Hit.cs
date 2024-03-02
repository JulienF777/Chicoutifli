using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Hit : MonoBehaviour
{
    [SerializeField] string _tagHit = string.Empty;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == _tagHit)
        {
            Debug.Log("Attaque");
        }
    }
}
