using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class InteractOnTrigger : MonoBehaviour
{
    private UIDocument HUD;

    // Start is called before the first frame update
    void Start()
    {
        HUD = GameObject.Find("Statistiques").GetComponent<UIDocument>();
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
        HUD = GameObject.Find("Statistiques").GetComponent<UIDocument>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            HUD.rootVisualElement.Q<Label>("Interaction").style.opacity = 100;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            HUD.rootVisualElement.Q<Label>("Interaction").style.opacity = 0;
        }
    }
}
