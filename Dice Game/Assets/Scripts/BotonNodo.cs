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
    [SerializeField] string lastButtonName = "999";
    [SerializeField] string lastLevel = "1"; // CAMBIAR A 4 CUANDO ESTE LISTO EL JUEGO. ARREGLAR.

    void Start()
    {
        // Para el nivel 1
        level1.Add("0", new string[] { "1" });
        level1.Add("1", new string[] { "2" });
        level1.Add("2", new string[] { "3" });
        level1.Add("3", new string[] { "4" });
        // Para el nivel 2
        level2.Add("0", new string[] { "1" });
        level2.Add("1", new string[] { "2", "3", "4" });
        level2.Add("2", new string[] { "5", "6" });
        level2.Add("3", new string[] { "5", "6", "7" });
        level2.Add("4", new string[] { "6", "7" });
        level2.Add("5", new string[] { "8", "9" });
        level2.Add("6", new string[] { "8", "9", "10" });
        level2.Add("7", new string[] { "9", "10" });
        level2.Add("8", new string[] { "11" });
        level2.Add("9", new string[] { "11" });
        level2.Add("10", new string[] { "11" });
        // Para el nivel 2
        level3.Add("0", new string[] { "1" });
        level3.Add("1", new string[] { "2", "3", "4" });
        level3.Add("2", new string[] { "5", "6" });
        level3.Add("3", new string[] { "5", "6", "7" });
        level3.Add("4", new string[] { "6", "7" });
        level3.Add("5", new string[] { "8", "9", "10" });
        level3.Add("6", new string[] { "9", "10", "11" });
        level3.Add("7", new string[] { "10", "11", "12" });
        level3.Add("8", new string[] { "13" });
        level3.Add("9", new string[] { "13", "14" });
        level3.Add("10", new string[] { "14", "15" });
        level3.Add("11", new string[] { "15", "16" });
        level3.Add("12", new string[] { "16" });
        level3.Add("13", new string[] { "17" });
        level3.Add("14", new string[] { "17", "18" });
        level3.Add("15", new string[] { "18", "19" });
        level3.Add("16", new string[] { "19" });
        level3.Add("17", new string[] { "20" });
        level3.Add("18", new string[] { "21", "22" });
        level3.Add("19", new string[] { "21" });
        level3.Add("20", new string[] { "22" });
        level3.Add("21", new string[] { "22" });
        level3.Add("22", new string[] { });
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
        string levelActual = OverWorldManager.instance.currentLevel.ToString();

        // Si es el ultimo boton
        if (botonActual == lastButtonName)
        {
            // Te pregunta si es el ultimo nivel del juego
            if (levelActual == lastLevel)
            {
                SwitchToFinalBattle();
            } // Sino te manda al siguiente nivel del juego.
            else
            { // ARREGLAR ESTO.
                OverWorldManager.instance.CompleteLevel();
                OverWorldManager.instance.currentLevel += 1;
            }
        }
    }
    void SwitchToFinalBattle()
    {
        string botonActual = OverWorldManager.instance.currentButton.ToString();
        if (botonActual == lastButtonName)
        {
            GuardaRopas.instance.SaveData();
            SceneManager.LoadScene("FinalBattle");
        }
    }

}
