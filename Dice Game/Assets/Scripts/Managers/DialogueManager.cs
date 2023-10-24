using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.UI;
using TMPro;

//Manager para dialogo 
public class DialogueManager : MonoBehaviour
{
    private Queue<String> sentences; //guarda el dialogo en una fila para acceder luego
    // Start is called before the first frame update
    public bool inDialogue = false;
    public static DialogueManager instance = null;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;

    public int dialogueSpeed;
    public int numDialogo = 0;
    [SerializeField] private DialogueTrigger dialogoInicial;
    [SerializeField] private TutorialManager tutorialManager;
    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void IniciarPrimerDialogo()
    {
        //Comenzamos el dialogo tutorial;
        dialogoInicial.TriggerDialogue();
    }

    void Awake()
    {
        // Para que no se destuya.
        //DontDestroyOnLoad(this.gameObject);   // Lo comento para que no se use en la otra escena, ya que es para el tutorial.
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
        
    }

    void Update()
    {
        if(inDialogue && UnityEngine.Input.GetMouseButtonDown(0)){
            DisplayNextSentence();
        }
    }

    public void StartDialogue (Dialogue dialogue) {
        inDialogue=true;
        animator.SetBool("IsOpen", true);
        //Debug.Log("Starting conversation with "+dialogue.name);
        nameText.text=dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence () { //Texto instantaneo
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        if(dialogueSpeed == 0) {
            dialogueText.text = sentence; //texto instantaneo
        } else {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        } 
        
    }

    IEnumerator TypeSentence(string sentence){ //Texto letra por letra
        dialogueText.text="";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text+=letter;

            for(int i = 0; i < dialogueSpeed; i++) {
                yield return null;    //espera dialogueSpeed frames entre cada letra
            }
        }
    }

    void EndDialogue(){
        inDialogue=false;
        animator.SetBool("IsOpen", false);
        Debug.Log("End conversation");
    }
}
