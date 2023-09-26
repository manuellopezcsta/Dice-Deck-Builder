using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonNodo : MonoBehaviour
{
    Transform posButton;
    private GameObject playerSprite;

    void Start()
    {
        playerSprite = GameObject.Find("PlayerPlaceHolder"); // ARREGLAR ESTO
    }

    public void OnButtonClick(Button boton)
    {
        // Si es el boton correcto
        if(OverWorldManager.instance.currentButton == int.Parse(boton.name) )
        {
            posButton = boton.transform;
            playerSprite.transform.position = posButton.position;
            //Debug.Log("Se movio a la siguiente zona");
            // Aumentamos en 1 el boton.
            OverWorldManager.instance.currentButton += 1;

            // Iniciamos el combate o la accion
            CombatManager.instance.EnterCombat();
            boton.interactable = false;
        }
    }
}
