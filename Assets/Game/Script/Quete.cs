using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Quete : MonoBehaviour
{
    public GameObject joueur;
    public GameObject pharmacien;
    public GameObject seringue;
    public GameObject [] tableauDialogue;

    private int idQuete;
    private bool queteEnCours;
    public bool choisirSeringue;

    // Start is called before the first frame update
    void Start()
    {
        idQuete = 1;
        queteEnCours = false;
        choisirSeringue = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Si le joueur clique sur E et qu'il est à côté du pharmacien, alors la quête est lancée
        if (joueur.GetComponent<BoxCollider>().bounds.Intersects(pharmacien.GetComponent<BoxCollider>().bounds) && Input.GetKeyDown(KeyCode.E) && !queteEnCours) 
        {
            if (choisirSeringue){
                Debug.Log(idQuete);
                GetComponent<ChoixSeringue>().affichageChoixSeringue();
                Debug.Log(idQuete);
            } else {
                Debug.Log("ça touche");
                lancerQuete();
            }
        }

        //Si le joueur clique sur E et qu'il est à côté de la seringue, alors la quête est finie
        if (joueur.GetComponent<BoxCollider>().bounds.Intersects(seringue.GetComponent<BoxCollider>().bounds)) {
            if (Input.GetKeyDown(KeyCode.E) && queteEnCours){
                finirQuete();
            } else if (Input.GetKeyDown(KeyCode.E) && !queteEnCours){
                Debug.Log("La quête n'est pas lancée.");
            }
        }
    }

    private void lancerQuete(){
        switch (idQuete){
            case 1:
                queteEnCours = true;
                tableauDialogue[idQuete-1].GetComponent<LancementDialogue>().DebutDialogue();
                break;
            case 2:
                Debug.Log("Quete 2 lancée");
                break;
        }
    }

    private void finirQuete(){
        switch (idQuete){
            case 1:
                Debug.Log("Quete 1 finie");
                queteEnCours = false;
                choisirSeringue = true;
                idQuete = 2;
                break;
        }
    }
}
