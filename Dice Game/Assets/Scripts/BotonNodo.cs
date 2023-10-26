using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


public class BotonNodo : MonoBehaviour
{
    Transform posButton;
    private GameObject playerSprite;
    Dictionary<string, string[]> level1 = new Dictionary<string, string[]>();
    Dictionary<string, string[]> level2 = new Dictionary<string, string[]>();
    Dictionary<string, string[]> level3 = new Dictionary<string, string[]>();
    Dictionary<string, string[]> level4 = new Dictionary<string, string[]>();
    [SerializeField] string lastButtonName = "999";

    void Start()
    {
        // Para el nivel 1
        level1.Add("0", new string[] { "1" });
        level1.Add("1", new string[] { "2" });
        level1.Add("2", new string[] { "3" });
        level1.Add("3", new string[] { "4" });
        level1.Add("4", new string[] { "5" });
        // Para el nivel 2
        level2.Add("0", new string[] { "1" });
        level2.Add("1", new string[] { "2", "3" });
        level2.Add("2", new string[] { "4", "5" });
        level2.Add("3", new string[] { "6" });
        level2.Add("4", new string[] { "7" });
        level2.Add("5", new string[] { "7" });
        level2.Add("6", new string[] { "8" });
        level2.Add("7", new string[] { "8" });
        // Para el nivel 3
        level3.Add("0", new string[] { "1" });
        level3.Add("1", new string[] { "2" });
        level3.Add("2", new string[] { "3" });
        level3.Add("3", new string[] { "4" });
        level3.Add("4", new string[] { "5" });
        // Para el nivel 4
        level4.Add("0", new string[] { "1" });
        level4.Add("1", new string[] { "2" });
        level4.Add("2", new string[] { "3" });
        level4.Add("3", new string[] { "4" });
        level4.Add("4", new string[] { "5" });
        // Para mover el player al boton.
        playerSprite = GameObject.Find("PlayerPlaceHolder"); // ARREGLAR ESTO
        // Cargamos la imagen del nodo.
        LoadNodePicture();
    }
    public void OnButtonClick(Button boton)
    {
        int currentlevel = OverWorldManager.instance.currentLevel;
        string botonActual = OverWorldManager.instance.currentButton.ToString();
        Dictionary<string, string[]> holder = null;
        switch (currentlevel)
        {
            case 1:
                holder = level1;
                break;
            case 2:
                holder = level2;
                break;
            case 3:
                holder = level3;
                break;
            case 4:
                holder = level4;
                break;
        }
        // Si es el boton correcto
        if (holder.ContainsKey(botonActual))
        {
            string[] valores = holder[botonActual];

            if (valores.Contains(boton.name))
            {
                int index = int.Parse(boton.name) - 1;
                OverWorldManager.instance.currentButton = int.Parse(boton.name);
                // Checkeamos si es el ultimo boton
                CheckForLevelCompletition();

                posButton = boton.transform;
                playerSprite.transform.position = posButton.position;

                // Iniciamos el combate o la accion
                GameManager.instance.HandleNodeAction(index);
                boton.interactable = false;
            }
        }

    }

    void LoadNodePicture()
    {
        //Debug.Log("DEBUG " + transform.name);
        int index = int.Parse(transform.name) - 1;
        // Obtenemos el objeto y luego su sprite
        Sprite nodeImage = GameManager.instance.GetNodeData(index).GetSprite();
        transform.GetChild(0).GetComponent<Image>().sprite = nodeImage;
    }

    void CheckForLevelCompletition()
    {
        // Lo comparamos con el boton actual y si es igual ejecutamos el codigo sino no.
        string botonActual = OverWorldManager.instance.currentButton.ToString();
        if(botonActual == lastButtonName)
        { // ARREGLAR ESTO.
            /*OverWorldManager.instance.CompleteLevel();
            OverWorldManager.instance.currentLevel += 1;
            */
            SwitchToFinalBattle();
        }
    }
    void SwitchToFinalBattle(){
        string botonActual = OverWorldManager.instance.currentButton.ToString();
        if(botonActual == lastButtonName)
        {
            GuardaRopas.instance.FinalBattle();
            SceneManager.LoadScene("FinalBattle");
        }
    }
    
}
