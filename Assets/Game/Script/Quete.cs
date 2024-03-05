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

    public int idQuete;
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
        if (SceneManager.GetActiveScene().name == "Lobby"){
            pharmacien = GameObject.Find("Pharmacien");
            seringue = new GameObject();
            seringue.AddComponent<BoxCollider>();
        } else {
            Debug.Log("on est bien ici");
            pharmacien = new GameObject();
            seringue.AddComponent<BoxCollider>();
            seringue = GameObject.Find("PickUp");
        }

        changementScene = GameObject.Find("SceneManager");

        DontDestroyOnLoad(this.gameObject);

        if (!queteEnCours && !choisirSeringue){
            pharmacien.transform.Find("Point Exclamation").gameObject.SetActive(true);
        }
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
        tableauDialogue[1] = GameObject.Find("Dialogue Pharmacien Quete 2");
        tableauDialogue[2] = GameObject.Find("Dialogue Pharmacien Quete 3");
        tableauDialogue[3] = GameObject.Find("Dialogue Pharmacien Quete 4");

        if (SceneManager.GetActiveScene().name == "Lobby"){
            pharmacien = GameObject.Find("Pharmacien");
            switch (idQuete)
            {
                case 1:
                    pharmacien.transform.position = new Vector3(-6, 1, 1);
                    Debug.Log("Pharmacien pos 1");
                    break;
                case 2:
                    pharmacien.transform.position = new Vector3(-1, 1, 22);
                    Debug.Log("Pharmacien pos 2");
                    break;
                case 3:
                    pharmacien.transform.position = new Vector3(15, 1, 27);
                    Debug.Log("Pharmacien pos 3");
                    break;
                case 4:
                    pharmacien.transform.position = new Vector3(39, 1, 28);
                    Debug.Log("Pharmacien pos 4");
                    break;
            }

            if (!queteEnCours && idQuete > 2){
                GameObject.Find("EntreeBatiment"+(idQuete-1)).SetActive(true);
            }

            seringue = new GameObject();
            seringue.AddComponent<BoxCollider>();
        } else {
            pharmacien = new GameObject();
            seringue = GameObject.Find("PickUp");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby"){
            //S'il doit choisir une seringue, on active le point d'interrogation au dessus du pharmacien
            if (choisirSeringue){
                pharmacien.transform.GetChild(1).gameObject.SetActive(true);
                pharmacien.transform.GetChild(2).gameObject.SetActive(false);
            } else if (!queteEnCours && !choisirSeringue) {
                pharmacien.transform.GetChild(2).gameObject.SetActive(true);
                pharmacien.transform.GetChild(1).gameObject.SetActive(false);
            } else {
                pharmacien.transform.GetChild(1).gameObject.SetActive(false);
                pharmacien.transform.GetChild(2).gameObject.SetActive(false);
            }

            //Si le joueur clique sur E et qu'il est à côté du pharmacien, alors la quête est lancée ou alors on lui propose de choisir une seringue
            if (joueur.GetComponent<BoxCollider>().bounds.Intersects(pharmacien.GetComponent<BoxCollider>().bounds) && Input.GetKeyDown(KeyCode.E) && !queteEnCours) 
            {
                if (choisirSeringue){
                    GetComponent<ChoixSeringue>().affichageChoixSeringue(idQuete);
                } else {
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
                queteEnCours = true;
                tableauDialogue[idQuete - 1].GetComponent<LancementDialogue>().DebutDialogue();
                break;
            case 3:
                queteEnCours = true;
                tableauDialogue[idQuete - 1].GetComponent<LancementDialogue>().DebutDialogue();
                break;
            case 4:
                queteEnCours = true;
                tableauDialogue[idQuete - 1].GetComponent<LancementDialogue>().DebutDialogue();
                Debug.Log("Combat contre le boss");
                pharmacien.SetActive(false);
                break;
        }

        GameObject.Find("EntreeBatiment"+idQuete).transform.GetChild(0).transform.gameObject.SetActive(true);
        if (pharmacien.activeInHierarchy){
            pharmacien.transform.Find("Point Exclamation").gameObject.SetActive(false);
        }

        changementScene.GetComponent<ChangementScene>().nomScene = "Niveau "+idQuete.ToString();
    }

    private void finirQuete(){
        switch (idQuete){
            case 1:
                Debug.Log("Quete 1 finie");
                queteEnCours = false;
                choisirSeringue = true;
                idQuete++;
                break;
            case 2:
                Debug.Log("Quete 2 finie");
                queteEnCours = false;
                choisirSeringue = true;
                idQuete++;
                break;
            case 3:
                Debug.Log("Quete 3 finie");
                queteEnCours = false;
                choisirSeringue = false;
                idQuete++;
                break;
        }
        changementScene.GetComponent<ChangementScene>().nomScene = "noScene";
        changementScene.GetComponent<ChangementScene>().changerScene("Lobby");
    }
}
