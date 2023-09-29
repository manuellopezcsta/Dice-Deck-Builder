using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class BotonNodo : MonoBehaviour
{
    Transform posButton;
    private GameObject playerSprite;
    private Image targetPicture;

    void Start()
    {
        playerSprite = GameObject.Find("PlayerPlaceHolder"); // ARREGLAR ESTO
        // Cargamos la imagen del nodo.
        LoadNodePicture();
    }

    public void OnButtonClick(Button boton)
    {
        // Si es el boton correcto
        if(OverWorldManager.instance.currentButton == int.Parse(boton.name) )
        {
            // Lo usamos para despues.
            int index = int.Parse(boton.name) - 1;

            posButton = boton.transform;
            playerSprite.transform.position = posButton.position;
            //Debug.Log("Se movio a la siguiente zona");
            // Aumentamos en 1 el boton.
            OverWorldManager.instance.currentButton += 1;

            // Iniciamos el combate o la accion
            GameManager.instance.HandleNodeAction(index);
            boton.interactable = false;
        }
    }

    void LoadNodePicture()
    {
        //Debug.Log("DEBUG " + transform.name);
        int index = int.Parse(transform.name) -1 ;
        // Obtenemos el objeto y luego su sprite
        Sprite nodeImage =  GameManager.instance.GetNodeData(index).GetSprite();
        transform.GetChild(0).GetComponent<Image>().sprite = nodeImage;
    }
}
