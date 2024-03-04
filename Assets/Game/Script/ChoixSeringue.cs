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
    private Label titreSeringueGauche;
    private Label titreSeringueDroite;
    private Button seringueGauche;
    private Button seringueDroite;
    private GameObject joueur;
    private GameObject pharmacien;
    private TypeSeringue typeSeringue;
    private float[] statsSeringueGauche;
    private float[] statsSeringueDroite;

    void Start()
    {
        typeSeringue = new TypeSeringue();
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
        joueur = GameObject.Find("Player");
        statsSeringueGauche = new float[4];
        statsSeringueDroite = new float[4];

        if (scene.name == "Lobby")
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
            titreSeringueGauche = choixSeringueUI.rootVisualElement.Q<Label>("TitreSeringueGauche");
            titreSeringueDroite = choixSeringueUI.rootVisualElement.Q<Label>("TitreSeringueDroite");

            choixSeringueUI.rootVisualElement.visible = false;

            texteSeringueGauche.text = "PV : 0\nVitesse : 0\nAttaque : 0";
            texteSeringueDroite.text = "PV : 0\nVitesse : 0\nAttaque : 0";

            pharmacien = GameObject.Find("Pharmacien").transform.GetChild(0).gameObject;
        }
    }

    public void affichageChoixSeringue(int idQuete){

        //0 : Vie, 1 : Vitesse, 2 : Attaque, 3 : Kiting
        var typeSeringueGauche = Random.Range(0, 3);
        //Les deux autres se font en fonction de typeSeringueGauche
        var typeSeringueDroite = Random.Range(0, 2);

        pharmacien.GetComponent<Animator>().SetBool("isGiving", true);

        switch (idQuete)
        {
            case 2:
                switch (typeSeringueGauche)
                {
                    case 0:
                        statsSeringueGauche = typeSeringue.statsSeringueVieNiveau1;
                        titreSeringueGauche.text = "Seringue de Vie";
                        switch (typeSeringueDroite)
                        {
                            case 0:
                                statsSeringueDroite = typeSeringue.statsSeringueVitesseNiveau1;
                                titreSeringueDroite.text = "Seringue de Vitesse";
                                break;
                            case 1:
                                statsSeringueDroite = typeSeringue.statsSeringueAttaqueNiveau1;
                                titreSeringueDroite.text = "Seringue d'Attaque";
                                break;
                        }
                        break;
                    case 1:
                        statsSeringueGauche = typeSeringue.statsSeringueVitesseNiveau1;
                        titreSeringueGauche.text = "Seringue de Vitesse";
                        switch (typeSeringueDroite)
                        {
                            case 0:
                                statsSeringueDroite = typeSeringue.statsSeringueVieNiveau1;
                                titreSeringueDroite.text = "Seringue de Vie";
                                break;
                            case 1:
                                statsSeringueDroite = typeSeringue.statsSeringueAttaqueNiveau1;
                                titreSeringueDroite.text = "Seringue d'Attaque";
                                break;
                        }
                        break;
                    case 2:
                        statsSeringueGauche = typeSeringue.statsSeringueAttaqueNiveau1;
                        titreSeringueGauche.text = "Seringue d'Attaque";
                        switch (typeSeringueDroite)
                        {
                            case 0:
                                statsSeringueDroite = typeSeringue.statsSeringueVitesseNiveau1;
                                titreSeringueDroite.text = "Seringue de Vitesse";
                                break;
                            case 1:
                                statsSeringueDroite = typeSeringue.statsSeringueVieNiveau1;
                                titreSeringueDroite.text = "Seringue de Vie";
                                break;
                        }
                        break;
                }
                break;
            case 3:
                switch (typeSeringueGauche)
                {
                    case 0:
                        statsSeringueGauche = typeSeringue.statsSeringueVieNiveau2;
                        titreSeringueGauche.text = "Seringue de Vie";
                        switch (typeSeringueDroite)
                        {
                            case 0:
                                statsSeringueDroite = typeSeringue.statsSeringueVitesseNiveau2;
                                titreSeringueDroite.text = "Seringue de Vitesse";
                                break;
                            case 1:
                                statsSeringueDroite = typeSeringue.statsSeringueAttaqueNiveau2;
                                titreSeringueDroite.text = "Seringue d'Attaque";
                                break;
                        }
                        break;
                    case 1:
                        statsSeringueGauche = typeSeringue.statsSeringueVitesseNiveau2;
                        titreSeringueGauche.text = "Seringue de Vitesse";
                        switch (typeSeringueDroite)
                        {
                            case 0:
                                statsSeringueDroite = typeSeringue.statsSeringueVieNiveau2;
                                titreSeringueDroite.text = "Seringue de Vie";
                                break;
                            case 1:
                                statsSeringueDroite = typeSeringue.statsSeringueAttaqueNiveau2;
                                titreSeringueDroite.text = "Seringue d'Attaque";
                                break;
                        }
                        break;
                    case 2:
                        statsSeringueGauche = typeSeringue.statsSeringueAttaqueNiveau2;
                        titreSeringueGauche.text = "Seringue d'Attaque";
                        switch (typeSeringueDroite)
                        {
                            case 0:
                                statsSeringueDroite = typeSeringue.statsSeringueVitesseNiveau2;
                                titreSeringueDroite.text = "Seringue de Vitesse";
                                break;
                            case 1:
                                statsSeringueDroite = typeSeringue.statsSeringueVieNiveau2;
                                titreSeringueDroite.text = "Seringue de Vie";
                                break;
                        }
                        break;
                }
                break;
        }

        choixSeringueUI.rootVisualElement.visible = true;
        HUD.rootVisualElement.visible = false;

        //statsSeringueGauche[0] = Mathf.Round(Random.value*10);

        GetComponent<Quete>().choisirSeringue = false;

        texteSeringueGauche.text = "PV : " + statsSeringueGauche[0] + "\nAttaque : " + statsSeringueGauche[1] + "\nVitesse : " + statsSeringueGauche[2]  + "\nCooldown : " + statsSeringueGauche[3];
        texteSeringueDroite.text = "PV : " + statsSeringueDroite[0] + "\nAttaque : " + statsSeringueDroite[1] + "\nVitesse : " + statsSeringueDroite[2]  + "\nCooldown : " + statsSeringueDroite[3];
        
    }

    void OnSeringueDroiteClick(){
        Debug.Log("Stats : "+statsSeringueDroite[1] + "; Joueur : "+joueur);
        joueur.GetComponent<S_PlayerStatistics>().setMaxHealth(statsSeringueDroite[0]);
        joueur.GetComponent<S_PlayerStatistics>().setHitStength(statsSeringueDroite[1]);
        joueur.GetComponent<S_PlayerStatistics>().setSpeed(statsSeringueDroite[2]);
        joueur.GetComponent<S_PlayerStatistics>().setHitCooldown(statsSeringueDroite[3]);
        Debug.Log("ajouter fonction d'ajout de stats au personnage");
        choixSeringueUI.rootVisualElement.visible = false;
        HUD.rootVisualElement.visible = true;
        
        pharmacien.transform.GetChild(1).gameObject.SetActive(true);
        pharmacien.GetComponent<Animator>().SetBool("isGiving", false);
    }

    void OnSeringueGaucheClick(){
        Debug.Log("Stats : "+statsSeringueGauche[1]);
        joueur.GetComponent<S_PlayerStatistics>().setMaxHealth(statsSeringueGauche[0]);
        joueur.GetComponent<S_PlayerStatistics>().setHitStength(statsSeringueGauche[1]);
        joueur.GetComponent<S_PlayerStatistics>().setSpeed(statsSeringueGauche[2]);
        joueur.GetComponent<S_PlayerStatistics>().setHitCooldown(statsSeringueGauche[3]);
        Debug.Log("ajouter fonction d'ajout de stats au personnage");
        choixSeringueUI.rootVisualElement.visible = false;
        HUD.rootVisualElement.visible = true;

        pharmacien.transform.GetChild(1).gameObject.SetActive(true);
        pharmacien.GetComponent<Animator>().SetBool("isGiving", false);
    }
}
