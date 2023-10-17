using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

//Manager para dialogo 
public class DialogueManager : MonoBehaviour
{
    private Queue<String> sentences; //guarda el dialogo en una fila para acceder luego
    // Start is called before the first frame update
    private bool inDialogue = false;
    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        if(inDialogue && UnityEngine.Input.GetMouseButtonDown(0)){
            DisplayNextSentence(); 
        }
    }

    public void StartDialogue (Dialogue dialogue) {
        inDialogue=true;
        Debug.Log("Starting conversation with "+dialogue.name);
        sentences.Clear();
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence () {
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
    }

    void EndDialogue(){
        inDialogue=false;
        Debug.Log("End conversation");
    }
    
}
