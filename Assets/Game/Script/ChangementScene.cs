using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour
{
    public GameObject joueur;
    private GameObject entreeBatiment;
    public string nomScene;

    //Boolean collision entrées niveaux
    private bool collisionEntree;

    private Scene scene;
    private bool clickChangementScene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        joueur = GameObject.Find("Player");
        clickChangementScene = false;
        nomScene = "noScene";
    }

    // Update is called once per frame
    void Update()
    {
        switch (nomScene)
        {
            case "Niveau 1":
                entreeBatiment = GameObject.Find("EntreeBatiment1").transform.GetChild(0).gameObject;
                break;
            case "Niveau 2":
                entreeBatiment = GameObject.Find("EntreeBatiment2").transform.GetChild(0).gameObject;
                break;
            case "Niveau 3":
                entreeBatiment = GameObject.Find("EntreeBatiment3").transform.GetChild(0).gameObject;
                break;
        }

        if (entreeBatiment != null)
        {
            collisionEntree = joueur.GetComponent<BoxCollider>().bounds.Intersects(entreeBatiment.GetComponent<BoxCollider>().bounds);
            if (nomScene == "noScene")
            {
                clickChangementScene = false;
            } else if (scene.name == "Lobby") {
                clickChangementScene = collisionEntree && Input.GetKeyDown(KeyCode.E);
            }
        }

        if (clickChangementScene && scene.name == "Lobby")
        {
            changerScene(nomScene);
        }

    }

    public void changerScene(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }
}
