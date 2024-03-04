using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionNiveaux : MonoBehaviour
{
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
        switch (SceneManager.GetActiveScene().name)
        {
            case "Lobby":
                transform.GetChild(0).gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;
            case "Niveau 1":
                transform.position = new Vector3(1f, 1f, 10f);
                transform.GetChild(0).gameObject.transform.localScale = new Vector3(2, 2, 2);
                break;
            case "Niveau 2":
                transform.position = new Vector3(-30f, 1f, 20f);
                transform.GetChild(0).gameObject.transform.localScale = new Vector3(2, 2, 2);
                break;
            case "Niveau 3":
                transform.position = new Vector3(1f, 1f, 10f);
                transform.GetChild(0).gameObject.transform.localScale = new Vector3(2, 2, 2);
                break;
        
        }
    }
}
