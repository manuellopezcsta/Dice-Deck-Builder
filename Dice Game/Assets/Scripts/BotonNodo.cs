using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BotonNodo : MonoBehaviour
{
    Transform posButton;
    private GameObject playerSprite;
    Dictionary<string, string[]> nodosArbol = new Dictionary<string, string[]>();

    void Start()
    {
        nodosArbol.Add("0", new string[] { "1" });
        nodosArbol.Add("1", new string[] { "2", "3" });
        nodosArbol.Add("2", new string[] { "4", "5" });
        nodosArbol.Add("3", new string[] { "6" });
        nodosArbol.Add("4", new string[] { "7" });
        nodosArbol.Add("5", new string[] { "7" });
        nodosArbol.Add("6", new string[] { "8" });
        nodosArbol.Add("7", new string[] { "8" });
        playerSprite = GameObject.Find("PlayerPlaceHolder"); // ARREGLAR ESTO
        // Cargamos la imagen del nodo.
        LoadNodePicture();
    }
    public void OnButtonClick(Button boton)
    {
        string botonActual = OverWorldManager.instance.currentButton.ToString();
        // Si es el boton correcto
        if (nodosArbol.ContainsKey(botonActual))
        {
            string[] valores = nodosArbol[botonActual];

            if (valores.Contains(boton.name))
            {
                int index = int.Parse(boton.name) - 1;

                posButton = boton.transform;
                playerSprite.transform.position = posButton.position;

                // Iniciamos el combate o la accion
                GameManager.instance.HandleNodeAction(index);
                boton.interactable = false;
                OverWorldManager.instance.currentButton = int.Parse(boton.name);
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

    public void CompleteLevel()
    {
        OverWorldManager.instance.CompleteLevel();
        OverWorldManager.instance.currentLevel += 1;
    }
}
