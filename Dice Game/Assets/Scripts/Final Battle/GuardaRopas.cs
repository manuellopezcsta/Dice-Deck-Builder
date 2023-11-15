using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardaRopas : MonoBehaviour
{
    // Encargado de transferir los datos entre la escena comun y el combate final
    public Player player1;
    public Player player2;
    public static GuardaRopas instance = null;
    public int secretEndingScore = 0;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }

    public void SaveData()
    {
        player1 = CombatManager.instance.GetPlayerN(1);
        player2 = CombatManager.instance.GetPlayerN(2);
        secretEndingScore = GameManager.instance.secretEndingCounter;
    }

    public void UpdateDeckArt()
    {
        CardList holder = GameObject.Find("Card List").GetComponent<CardList>();
        List<Card> artList = holder.cardList;
        // For player 1
        Deck p1Deck = player1.currentDeck;
        foreach(Card card in p1Deck.cards)
        {
            foreach(Card art in artList)
            {
                if(card.cardName == art.cardName)
                {
                    card.image = art.image;
                    break;
                }
            }
        }

        // For player 1
        Deck p2Deck = player2.currentDeck;
        foreach(Card card in p2Deck.cards)
        {
            foreach(Card art in artList)
            {
                if(card.cardName == art.cardName)
                {
                    card.image = art.image;
                    break;
                }
            }
        }
    }
}
