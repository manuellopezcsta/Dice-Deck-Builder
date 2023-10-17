using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Activador del dialogo asociado a un elemnto
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue () {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
    }
}
