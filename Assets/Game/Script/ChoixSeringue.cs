using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoixSeringue : MonoBehaviour
{
    public UIDocument choixSeringueUI;
    public UIDocument HUD;

    private Label texteSeringueGauche;
    private Label texteSeringueDroite;
    private Label texteDialogueSeringue;
    private Button seringueGauche;
    private Button seringueDroite;

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
    }

    public void affichageChoixSeringue(){
        choixSeringueUI.rootVisualElement.visible = true;
        HUD.rootVisualElement.visible = false;

        var pvGauche = Random.value;
        var vitGauche = Random.value;
        var atqGauche = Random.value;

        var pvDroite = Random.value;
        var vitDroite = Random.value;
        var atqDroite = Random.value;

        GetComponent<Quete>().choisirSeringue = false;

        texteSeringueGauche.text = "PV : " + pvGauche + "\nVitesse : " + vitGauche + "\nAttaque : " + atqGauche;
        texteSeringueDroite.text = "PV : " + pvDroite + "\nVitesse : " + vitDroite + "\nAttaque : " + atqDroite;
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
