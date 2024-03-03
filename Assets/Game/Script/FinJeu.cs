using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class FinJeu : MonoBehaviour
{
    public UIDocument finJeu;

    private Button retourMenu;

    // Start is called before the first frame update
    void Start()
    {
        retourMenu = finJeu.rootVisualElement.Q<Button>("RetourMenu");
        retourMenu.clicked += RetourMenu;
    }

    void RetourMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
