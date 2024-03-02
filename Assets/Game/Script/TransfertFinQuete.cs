using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransfertFinQuete : MonoBehaviour
{
    public int idQuete;
    public bool choixSeringue;
    public bool queteEnCours;

    // Start is called before the first frame update
    void Start()
    {
        idQuete = 1;
        choixSeringue = false;
        queteEnCours = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Si le joueur clique sur E et qu'il est à côté de la seringue, alors la quête est finie
        if (GetComponent<BoxCollider>().bounds.Intersects(seringue.GetComponent<BoxCollider>().bounds)) {
            if (Input.GetKeyDown(KeyCode.E) && queteEnCours){
                finirQuete();
            } else if (Input.GetKeyDown(KeyCode.E) && !queteEnCours){
                Debug.Log("La quête n'est pas lancée.");
            }
        }
    }

    private void finirQuete(){
        switch (idQuete){
            case 1:
                Debug.Log("Quete 1 finie");
                queteEnCours = false;
                choisirSeringue = true;
                //idQuete = 2;
                break;
        }
    }
}
