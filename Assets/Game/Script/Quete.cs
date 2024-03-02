using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quete : MonoBehaviour
{
    public GameObject joueur;
    public GameObject pharmacien;
    public GameObject seringue;

    private int idQuete;
    private bool queteEnCours;

    // Start is called before the first frame update
    void Start()
    {
        idQuete = 1;
        queteEnCours = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Si le joueur clique sur E et qu'il est à côté du pharmacien, alors la quête est lancée
        if (joueur.GetComponent<BoxCollider>().bounds.Intersects(pharmacien.GetComponent<BoxCollider>().bounds) && Input.GetKeyDown(KeyCode.E) && !queteEnCours) {
            Debug.Log("ça touche");
            lancerQuete(idQuete);
        }

        //Si le joueur clique sur E et qu'il est à côté de la seringue, alors la quête est finie
        if (joueur.GetComponent<BoxCollider>().bounds.Intersects(seringue.GetComponent<BoxCollider>().bounds)) {
            if (Input.GetKeyDown(KeyCode.E) && queteEnCours){
                Debug.Log("ça touche");
                finirQuete(idQuete);
            } else if (Input.GetKeyDown(KeyCode.E) && !queteEnCours){
                Debug.Log("La quête n'est pas lancée.");
            }
        }
    }

    private void lancerQuete(int idQuete){
        switch (idQuete){
            case 1:
                Debug.Log("Quete 1");
                queteEnCours = true;
                break;
        }
    }

    private void finirQuete(int idQuete){
        switch (idQuete){
            case 1:
                Debug.Log("Quete 1 finie");
                queteEnCours = false;
                idQuete = 2;
                break;
        }
    }
}
