using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Quete : MonoBehaviour
{
    public GameObject joueur;
    public GameObject pharmacien;
    public GameObject seringue;
    public GameObject [] tableauDialogue;
    public GameObject changementScene;

    private int idQuete;
    private bool queteEnCours;
    public bool choisirSeringue;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Quete");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        idQuete = 1;
        queteEnCours = false;
        choisirSeringue = false;

        joueur = GameObject.Find("Player");
        if (SceneManager.GetActiveScene().name == "Alpha"){
            pharmacien = GameObject.Find("Pharmacien");
            seringue = new GameObject();
        } else {
            Debug.Log("on est bien ici");
            pharmacien = new GameObject();
            seringue = GameObject.Find("PickUp");
        }

        changementScene = GameObject.Find("SceneManager");

        DontDestroyOnLoad(this.gameObject);
    }

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
        changementScene = GameObject.Find("SceneManager");
        joueur = GameObject.Find("Player");
        tableauDialogue[0] = GameObject.Find("Dialogue Pharmacien Quete 1");
        if (SceneManager.GetActiveScene().name == "Alpha"){
            pharmacien = GameObject.Find("Pharmacien");
            seringue = new GameObject();
        } else {
            pharmacien = new GameObject();
            seringue = GameObject.Find("PickUp");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Alpha"){
            //Si le joueur clique sur E et qu'il est à côté du pharmacien, alors la quête est lancée
            if (joueur.GetComponent<BoxCollider>().bounds.Intersects(pharmacien.GetComponent<BoxCollider>().bounds) && Input.GetKeyDown(KeyCode.E) && !queteEnCours) 
            {
                Debug.Log("");
                if (choisirSeringue){
                    GetComponent<ChoixSeringue>().affichageChoixSeringue();
                } else {
                    Debug.Log("ça touche");
                    lancerQuete();
                }
            }
        } else {
            //Si le joueur clique sur E et qu'il est à côté de la seringue, alors la quête est finie
            if (joueur.GetComponent<BoxCollider>().bounds.Intersects(seringue.GetComponent<BoxCollider>().bounds)) {
                if (Input.GetKeyDown(KeyCode.E) && queteEnCours){
                    finirQuete();
                } else if (Input.GetKeyDown(KeyCode.E) && !queteEnCours){
                    Debug.Log("La quête n'est pas lancée.");
                }
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

        changementScene.GetComponent<ChangementScene>().nomScene = "Niveau "+idQuete.ToString();
    }

    private void finirQuete(){
        switch (idQuete){
            case 1:
                Debug.Log("Quete 1 finie");
                queteEnCours = false;
                choisirSeringue = true;
                break;
        }
        changementScene.GetComponent<ChangementScene>().nomScene = "noScene";
        changementScene.GetComponent<ChangementScene>().changerScene("Alpha");
    }
}
