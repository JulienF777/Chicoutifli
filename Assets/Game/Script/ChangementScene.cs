using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour
{
    public GameObject joueur;
    public GameObject batiment; //à redefinir à chaque changement de niveau
    public string nomScene;

    private Scene scene;
    private bool clickChangementScene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        clickChangementScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        clickChangementScene = joueur.GetComponent<BoxCollider>().bounds.Intersects(batiment.GetComponent<BoxCollider>().bounds) && Input.GetKeyDown(KeyCode.E);

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
