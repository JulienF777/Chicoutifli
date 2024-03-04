using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public UIDocument mainMenu;

    private Button lancerJeu;
    private Button quitterJeu;

    // Start is called before the first frame update
    void Start()
    {
        lancerJeu = mainMenu.rootVisualElement.Q<Button>("LancerJeu");
        lancerJeu.clicked += LancerJeu;
        quitterJeu = mainMenu.rootVisualElement.Q<Button>("QuitterJeu");
        quitterJeu.clicked += QuitterJeu;
    }

    void LancerJeu(){
        SceneManager.LoadScene("Lobby");
    }

    void QuitterJeu(){
        Application.Quit();
    }
}
