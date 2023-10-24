using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    [SerializeField] Image bigDisplay;
    [SerializeField] TextMeshProUGUI description;
    public List<string> descriptionList;
    public List<Sprite> charactersBigSprites;
    public List<Button> characterButtons;
    int selected = 1; // Del 1 al 5 para saber que pj esta seleccionado.
    public bool selectingPlayer1;
    [SerializeField] GameObject panelP2;
    
    // Llamada x los portraits.
    public void SelectedOne(Button boton)
    {
        switch(boton.name)
        {
            case "PJ1":
                selected = 1;
                description.text = descriptionList[0];
                bigDisplay.sprite = charactersBigSprites[0];
                break;
            case "PJ2":
                selected = 2;
                description.text = descriptionList[1];
                bigDisplay.sprite = charactersBigSprites[1];
                break;
            case "PJ3":
                selected = 3;
                description.text = descriptionList[2];
                bigDisplay.sprite = charactersBigSprites[2];
                break;
            case "PJ4":
                selected = 4;
                description.text = descriptionList[3];
                bigDisplay.sprite = charactersBigSprites[3];
                break;
            case "PJ5":
                selected = 5;
                description.text = descriptionList[4];
                bigDisplay.sprite = charactersBigSprites[4];
                break;
        }
        // Guardamos el valor para leerlo en LoadOut Panel.
        PlayerPrefs.SetInt("selected",selected);
    }

    public void ElegirPersonaje()
    {
        if(selectingPlayer1)
        {
            PlayerPrefs.SetInt("player1", selected);
            // Esconder panel y mostrar el del jugador 2.
            gameObject.SetActive(false);
            panelP2.SetActive(true);
        } else {
            PlayerPrefs.SetInt("player2", selected);
            // Mandar a la escena tutorial
            SceneManager.LoadScene("Tutorial"); // CAMBIAR A ESCENA TUTORIAL
        }
    }

    void OnEnable()
    {
        PlayerPrefs.SetInt("selected",1);
        // Cuando se prenda que borre los prefs anteriores.
        if(selectingPlayer1)
        {
            if(PlayerPrefs.HasKey("player1"))
            {
                PlayerPrefs.DeleteKey("player1");
            }
            if(PlayerPrefs.HasKey("player2"))
            {
                PlayerPrefs.DeleteKey("player2");
            }
        } else{
            // Si es el panel del player 2 
            int p2 = PlayerPrefs.GetInt("player1");
            if(p2==1){
                selected=2;
                description.text = descriptionList[1];
                bigDisplay.sprite = charactersBigSprites[1];
            }
            // Apagamos el boton que ya esta elegido.
            switch(p2)
            {
                case 1:
                    characterButtons[0].interactable = false;
                    break;
                case 2:
                    characterButtons[1].interactable = false;
                    break;
                case 3:
                    characterButtons[2].interactable = false;
                    break;
                case 4:
                    characterButtons[3].interactable = false;
                    break;
                case 5:
                    characterButtons[4].interactable = false;
                    break;
            }
            
        }
    }
}
