using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ChoixSeringue : MonoBehaviour
{
    public UIDocument choixSeringueUI;
    public UIDocument HUD;

    private Label texteSeringueGauche;
    private Label texteSeringueDroite;
    private Label texteDialogueSeringue;
    private Button seringueGauche;
    private Button seringueDroite;

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
        if (scene.name == "Alpha")
        {
            choixSeringueUI = GameObject.Find("ChoixSeringue").GetComponent<UIDocument>();
            HUD = GameObject.Find("Statistiques").GetComponent<UIDocument>();
            choixSeringueUI.rootVisualElement.visible = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        texteSeringueGauche = choixSeringueUI.rootVisualElement.Q<Label>("TexteBoutonChoixSeringue1");
        texteSeringueDroite = choixSeringueUI.rootVisualElement.Q<Label>("TexteBoutonChoixSeringue2");
        seringueGauche = choixSeringueUI.rootVisualElement.Q<Button>("BoutonChoixSeringue1");
        seringueGauche.clicked += OnSeringueGaucheClick;
        seringueDroite = choixSeringueUI.rootVisualElement.Q<Button>("BoutonChoixSeringue2");
        seringueDroite.clicked += OnSeringueDroiteClick;
        texteDialogueSeringue = choixSeringueUI.rootVisualElement.Q<Label>("DialogueChoixSeringue");
        texteDialogueSeringue.text = "Merci d'avoir récupérer le composant, j'ai pu te créer deux nouvelles seringues. Laquelle veux-tu choisir ?";

        choixSeringueUI.rootVisualElement.visible = false;

        texteSeringueGauche.text = "PV : 0\nVitesse : 0\nAttaque : 0";
        texteSeringueDroite.text = "PV : 0\nVitesse : 0\nAttaque : 0";
    }

    public void affichageChoixSeringue(){
        Debug.Log("1");
        choixSeringueUI.rootVisualElement.visible = true;
        HUD.rootVisualElement.visible = false;
        Debug.Log("2");

        var pvGauche = Random.value;
        var vitGauche = Random.value;
        var atqGauche = Random.value;
        Debug.Log("3");

        var pvDroite = Random.value;
        var vitDroite = Random.value;
        var atqDroite = Random.value;
        Debug.Log("4");

        GetComponent<Quete>().choisirSeringue = false;
        Debug.Log("5");

        texteSeringueGauche.text = "PV : " + pvGauche + "\nVitesse : " + vitGauche + "\nAttaque : " + atqGauche;
        texteSeringueDroite.text = "PV : " + pvDroite + "\nVitesse : " + vitDroite + "\nAttaque : " + atqDroite;
        
        Debug.Log("texte gauche : "+texteSeringueGauche.text+" texte droite : "+texteSeringueDroite.text);
        Debug.Log("6");
    }

    void OnSeringueDroiteClick(){
        Debug.Log("ajouter fonction d'ajout de stats au personnage");
        choixSeringueUI.rootVisualElement.visible = false;
        HUD.rootVisualElement.visible = true;
    }

    void OnSeringueGaucheClick(){
        Debug.Log("ajouter fonction d'ajout de stats au personnage");
        choixSeringueUI.rootVisualElement.visible = false;
        HUD.rootVisualElement.visible = true;
    }
}
