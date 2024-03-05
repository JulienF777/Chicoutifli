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
                transform.position = new Vector3(-6, 0.2f, -36);
                break;
            case "Niveau 1":
                transform.position = new Vector3(0, 1f, 4);
                break;
            case "Niveau 2":
                transform.position = new Vector3(-15, 1f, 11);
                break;
            case "Niveau 3":
                transform.position = new Vector3(27, 9, 34);
                break;
            case "Niveau 4":
                transform.position = new Vector3(-4, 1, -1);
                break;
        }
    }
}
