using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TutorialManager : MonoBehaviour
{
   

    [Header("Imagenes")]
    [SerializeField] private Image SpriteEnemy;
    [SerializeField] private Image BarraVidaEnemy;
    [SerializeField] private Image BarraPlayer1;
    [SerializeField] private TMP_Text MRPlayer1;
    [SerializeField] private Image BarraPlayer2;
    [SerializeField] private Image BarraEspejada;

    [Header("Estado")]
    [SerializeField] private GameObject cartaExplicacion;
    [SerializeField] public int Evento = 0;
    [SerializeField] private DialogueTrigger dialogoDado;
    [SerializeField] private DialogueTrigger dialogoSegundoDado;
    [SerializeField] private DialogueTrigger dialogoTercerDado;
    [SerializeField] private DialogueTrigger dialogoEnemy;
    [SerializeField] private DialogueTrigger dialogoCuartoDado;
    [SerializeField] private DialogueTrigger dialogoFinal;
    [Header("SegundaCartaPlayer1")]
    [SerializeField] private GameObject SegundoDado;
    [SerializeField] private GameObject SegundaCarta;
    [Header("SegundaCartaPlayer2")]
    [SerializeField] private GameObject CuartoDado;
    [SerializeField] private GameObject CuartaCarta;
    [Header("SegundaCartaRemate")]
    [SerializeField] private GameObject QuintoDado;
    [SerializeField] private GameObject QuintaCarta;
    public List<GameObject> listaDados1;
    public List<GameObject> listaCartas1;
    public List<GameObject> listaDados2;
    public List<GameObject> listaCartas2;
    public List<GameObject> listaDados3;
    public List<GameObject> listaCartas3;

    /*
     aca van las cartas que se juegan por si se necesita
     */
  /*
   Aclaracion: las cartas cuando cambian de cartas/dice(turno player 1) a cartasplayer2/diceplayer2 aparecen con un collider
  prendido por eso solo referenciamos la segunda carta o dado por jugar*/

    private void Update()
    {
        if (DialogueManager.instance.inDialogue)
        {
            cartaExplicacion.SetActive(false);
        }
        if (DialogueManager.instance.inDialogue == false && Evento == 4)
        {
            Evento++;
            DialogoTutorial();
        }
        if (DialogueManager.instance.inDialogue == false && Evento == 7)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    public void DialogoTutorial()
    {
        if (Evento == 1)
        {
            Debug.Log("Entro");
            BarraVidaEnemy.fillAmount = 0.7f;
            BarraEspejada.fillAmount = BarraVidaEnemy.fillAmount;
            dialogoDado.TriggerDialogue();
            SegundoDado.GetComponent<DadoTutorial>().enabled = true;
            SegundaCarta.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (Evento == 2)
        {
            Debug.Log("Entro2");
            MRPlayer1.text = "3";
            dialogoSegundoDado.TriggerDialogue();
            PrenderTodo2();
            ApagarTodo1();
        }
        if (Evento == 3)
        {
            BarraPlayer1.fillAmount = 0.4f;
            CuartaCarta.GetComponent<Collider2D>().enabled = true;
            CuartoDado.GetComponent<DadoTutorial>().enabled = true;
            dialogoTercerDado.TriggerDialogue();
        }
        if (Evento == 4)
        {
            ApagarTodo2();
            BarraPlayer1.fillAmount = 0.8f;
            dialogoEnemy.TriggerDialogue();
            //CuartaCarta.GetComponent<Collider2D>().enabled = true;
        }
        if (Evento == 5)
        {
            dialogoCuartoDado.TriggerDialogue();
            PrenderRemate();

        }
        if (Evento == 6)
        {
            BarraVidaEnemy.fillAmount = 0.1f;
            BarraEspejada.fillAmount = BarraVidaEnemy.fillAmount;
            QuintaCarta.GetComponent<Collider2D>().enabled = true;
            QuintoDado.GetComponent<DadoTutorial>().enabled = true;
        }
        if (Evento == 7)
        {
            BarraVidaEnemy.fillAmount = 0f;
            BarraEspejada.fillAmount = BarraVidaEnemy.fillAmount;
            dialogoFinal.TriggerDialogue();
            ApagarFinal();
        }
    }
    private void ApagarTodo1()
    {
        foreach (GameObject obj in listaCartas1)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in listaDados1)
        {
            obj.SetActive(false);
        }
    }
    private void PrenderTodo2()
    {
        foreach (GameObject obj in listaCartas2)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in listaDados2)
        {
            obj.SetActive(true);
        }
    }
   
    private void ApagarTodo2()
    {
        foreach (GameObject obj in listaCartas2)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in listaDados2)
        {
            obj.SetActive(false);
        }
    }
    private void PrenderRemate()
    {
        foreach (GameObject obj in listaCartas3)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in listaDados3)
        {
            obj.SetActive(true);
        }
    }
    private void ApagarFinal()
    {
        foreach (GameObject obj in listaCartas3)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in listaDados3)
        {
            obj.SetActive(false);
        }
    }

    public void BotonSkipSi()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void BotonSkipNo()
    {
        DialogueManager.instance.IniciarPrimerDialogo();
    }
}
