using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LancementDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    private UIDocument dialogueUI;
    private UIDocument HUD;

    public void DebutDialogue(){
        dialogueUI = FindObjectOfType<DialogueManager>().dialogueUI;
        HUD = FindObjectOfType<DialogueManager>().HUD;
        dialogueUI.rootVisualElement.visible = true;
        HUD.rootVisualElement.visible = false;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
