using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> phrases;
    public UIDocument dialogueUI;
    public UIDocument HUD;
    public Label nomPersonnage;
    public Label texteDialogue;
    public Button dialogueSuivant;

    // Start is called before the first frame update
    void Start()
    {
        phrases = new Queue<string>();

        nomPersonnage = dialogueUI.rootVisualElement.Q<Label>("NomPersonnage");
        texteDialogue = dialogueUI.rootVisualElement.Q<Label>("DialoguePersonnage");
        dialogueSuivant = dialogueUI.rootVisualElement.Q<Button>("BoutonSuivant");
        dialogueSuivant.clicked += OnDialogueSuivantClick;
        dialogueUI.rootVisualElement.visible = false;
    }

    void OnDialogueSuivantClick(){
        AffichePhraseSuivante();
    }

    public void StartDialogue(Dialogue dialogue){
        phrases.Clear();

        foreach (string phrase in dialogue.phrases){
            phrases.Enqueue(phrase);
        }

        Debug.Log(phrases);

        nomPersonnage.text = dialogue.nomPersonnage;

        AffichePhraseSuivante();
    }

    public void AffichePhraseSuivante(){
        if (phrases.Count == 0){
            FinDialogue();
            return;
        }

        string phrase = phrases.Dequeue();
        
        texteDialogue.text = phrase;

        Debug.Log(phrase);
    }

    void FinDialogue(){
        Debug.Log("Fin du dialogue");
        dialogueUI.rootVisualElement.visible = false;
        HUD.rootVisualElement.visible = true;
    }
}
