using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour
{
    public GameObject joueur;
    public GameObject entreeBatiment1; //à redefinir à chaque changement de niveau
    public GameObject entreeBatiment2; //à redefinir à chaque changement de niveau
    public GameObject entreeBatiment3; //à redefinir à chaque changement de niveau
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
        if (nomScene == "Niveau 1")
        {
            collisionEntree = joueur.GetComponent<BoxCollider>().bounds.Intersects(entreeBatiment1.GetComponent<BoxCollider>().bounds);
        } else if (nomScene == "Niveau 2")
        {
            collisionEntree = joueur.GetComponent<BoxCollider>().bounds.Intersects(entreeBatiment2.GetComponent<BoxCollider>().bounds);
        } else if (nomScene == "Niveau 3")
        {
            collisionEntree = joueur.GetComponent<BoxCollider>().bounds.Intersects(entreeBatiment3.GetComponent<BoxCollider>().bounds);
        }

        if (nomScene == "noScene")
        {
            clickChangementScene = false;
        } else if (scene.name == "Alpha") {
            if (collisionEntree)
            {
                Debug.Log("E pour changer de niveau");
            }
            clickChangementScene = collisionEntree&& Input.GetKeyDown(KeyCode.E);
        }

        if (clickChangementScene && scene.name == "Alpha")
        {
            changerScene(nomScene);
        }

    }

    public void changerScene(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }
}
