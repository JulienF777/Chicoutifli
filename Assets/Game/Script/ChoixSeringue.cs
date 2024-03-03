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
    private GameObject joueur;

    private float [] statsSeringueGauche;
    private float [] statsSeringueDroite;

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
        joueur = GameObject.Find("Player");
        statsSeringueGauche = new float[4];
        statsSeringueDroite = new float[4];

        if (scene.name == "Alpha")
        {
            choixSeringueUI = GameObject.Find("ChoixSeringue").GetComponent<UIDocument>();
            HUD = GameObject.Find("Statistiques").GetComponent<UIDocument>();
            choixSeringueUI.rootVisualElement.visible = false;

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
    }

    public void affichageChoixSeringue(){
        choixSeringueUI.rootVisualElement.visible = true;
        HUD.rootVisualElement.visible = false;

        statsSeringueGauche[0] = Mathf.Round(Random.value*10);
        statsSeringueGauche[1] = Mathf.Round(Random.value*10);
        statsSeringueGauche[2] = Mathf.Round(Random.value*10);
        statsSeringueGauche[3] = (Random.value);

        statsSeringueDroite[0] = Mathf.Round(Random.value*10);
        statsSeringueDroite[1] = Mathf.Round(Random.value*10);
        statsSeringueDroite[2] = Mathf.Round(Random.value*10);
        statsSeringueDroite[3] = (Random.value);

        GetComponent<Quete>().choisirSeringue = false;

        texteSeringueGauche.text = "PV : " + statsSeringueGauche[0] + "\nVitesse : " + statsSeringueGauche[1] + "\nAttaque : " + statsSeringueGauche[2] + "\nCooldown : " + statsSeringueGauche[3];
        texteSeringueDroite.text = "PV : " + statsSeringueDroite[0] + "\nVitesse : " + statsSeringueDroite[1] + "\nAttaque : " + statsSeringueDroite[2] + "\nCooldown : " + statsSeringueDroite[3];
        
        Debug.Log("texte gauche : "+texteSeringueGauche.text+" texte droite : "+texteSeringueDroite.text);
    }

    void OnSeringueDroiteClick(){
        joueur.GetComponent<S_PlayerStatistics>().setMaxHealth(statsSeringueDroite[0]);
        joueur.GetComponent<S_PlayerStatistics>().setSpeed(statsSeringueDroite[1]);
        joueur.GetComponent<S_PlayerStatistics>().setHitStength(statsSeringueDroite[2]);
        joueur.GetComponent<S_PlayerStatistics>().setHitCooldown(statsSeringueDroite[3]);
        Debug.Log("ajouter fonction d'ajout de stats au personnage");
        choixSeringueUI.rootVisualElement.visible = false;
        HUD.rootVisualElement.visible = true;
    }

    void OnSeringueGaucheClick(){
        joueur.GetComponent<S_PlayerStatistics>().setMaxHealth(statsSeringueGauche[0]);
        joueur.GetComponent<S_PlayerStatistics>().setSpeed(statsSeringueGauche[1]);
        joueur.GetComponent<S_PlayerStatistics>().setHitStength(statsSeringueGauche[2]);
        joueur.GetComponent<S_PlayerStatistics>().setHitCooldown(statsSeringueGauche[3]);
        Debug.Log("ajouter fonction d'ajout de stats au personnage");
        choixSeringueUI.rootVisualElement.visible = false;
        HUD.rootVisualElement.visible = true;
    }
}
