using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardMenu : MonoBehaviour
{
    [SerializeField]CardList completeList;
    List<Card> rompeJuramentos = new List<Card>();
    List<Card> aliadas = new List<Card>();
    List<Card> common = new List<Card>();

    // Stuff for the choices
    Card option1;
    Card option2;
    Card option3;

    // To change the pictures
    [SerializeField] Image card1;
    [SerializeField] Image card2;
    [SerializeField] Image card3;
    // To swap between player selections.
    bool playerOneTurn = true; // 
    [SerializeField] TextMeshProUGUI texto;
    // So we dont repeat code
    bool listsAreDone = false;

    void BuildLists()
    {
        BuildRompeJuramentos();
        BuildAliadas();
        BuildCommon();
        listsAreDone = true;
    }

    void OnEnable()
    {
        if(!listsAreDone)
        {
            BuildLists();
        }
        playerOneTurn = true;
        RunPanelLogic();
    }

    public void OnCardSelection(Button boton)
    {
        Player currentPlayer;
        if(playerOneTurn)
        {
            currentPlayer = CombatManager.instance.GetPlayerN(1);
        } else {
            currentPlayer = CombatManager.instance.GetPlayerN(2);
        }
        // Checkeamos el nombre del boton y agregamos la carta opcion 1 , 2 o 3.
        switch(boton.name)
        {
            case "Card 1":
                currentPlayer.currentDeck.AddCard(option1);
                Debug.Log("Se agrego " + option1.cardName + " al mazo de " + currentPlayer.name);
                break;
            case "Card 2":
                currentPlayer.currentDeck.AddCard(option2);
                Debug.Log("Se agrego " + option2.cardName + " al mazo de " + currentPlayer.name);
                break;
            case "Card 3":
                currentPlayer.currentDeck.AddCard(option3);
                Debug.Log("Se agrego " + option3.cardName + " al mazo de " + currentPlayer.name);
                break;
        }
        // Before we switch to player 2 , we check if its player 2 turn to disable the panel and avoid a loop.
        if(!playerOneTurn)
        {
            gameObject.SetActive(false);
        }

        playerOneTurn = false;
        // We run the panel again if the player 2 has not chose yet.
        if(!playerOneTurn)
        {
            RunPanelLogic();
        }
    }

    Card ChooseRandomFromList(List<Card> cardList)
    {
        // Sacamos un index random de la lista
        int index = Random.Range(0, cardList.Count);
        return cardList[index];
    }

    void ShowCards()
    {
        card1.sprite = option1.image;
        card2.sprite = option2.image;
        card3.sprite = option3.image;
    }

    void RunPanelLogic()
    {
        // We create the choices
        option1 = ChooseRandomFromList(common);
        option2 = ChooseRandomFromList(aliadas);
        option3 = ChooseRandomFromList(rompeJuramentos);
        // We show them
        ShowCards();
        // We update the text
        UpdateText();
    }

    void UpdateText()
    {
        if(playerOneTurn)
        {
            texto.text = "Elije una carta para agregar a tu mazo J1";
        } else {
            texto.text = "Elije una carta para agregar a tu mazo J2";
        }
    }

    void BuildRompeJuramentos()
    {
        List<Card> cartas = completeList.cardList;
        rompeJuramentos.Add(cartas[2]); // Curar
        rompeJuramentos.Add(cartas[3]); // Armadura
        rompeJuramentos.Add(cartas[4]); // MR
        rompeJuramentos.Add(cartas[6]); // Parry Fisico
        rompeJuramentos.Add(cartas[7]); // Parry Magico
        rompeJuramentos.Add(cartas[13]); // Ataque RJ
        rompeJuramentos.Add(cartas[14]); // Ataque Magico RJ
        rompeJuramentos.Add(cartas[15]); // Break RJ
        rompeJuramentos.Add(cartas[16]); // Veneno RJ

    }

    void BuildAliadas()
    {
        List<Card> cartas = completeList.cardList;
        aliadas.Add(cartas[9]); // Cura x2
        aliadas.Add(cartas[10]); // Potenciador
        aliadas.Add(cartas[11]); // Armadura x2
        aliadas.Add(cartas[12]); // Bloqueador
    }

    void BuildCommon()
    {
        List<Card> cartas = completeList.cardList;
        common.Add(cartas[0]); // Ataque
        common.Add(cartas[1]); // Ataque Magico
        common.Add(cartas[2]); // Curar
        common.Add(cartas[3]); // Armadura
        common.Add(cartas[4]); // MR
        common.Add(cartas[5]); // Break
        common.Add(cartas[6]); // Parry Fisico
        common.Add(cartas[7]); // Parry Magico
        common.Add(cartas[8]); // Veneno

    }
}
