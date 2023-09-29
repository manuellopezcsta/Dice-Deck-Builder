using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckLists : MonoBehaviour
{
    public List<Deck> deckList = new List<Deck>();
    [SerializeField] CardList cardList;
    Deck deck1;
    Deck deck2;
    Deck deck3;
    Deck deck4;
    Deck deck5;
    Deck deck6;

    public void Start()
    {
        deck1 = deckList[0];
        deck2 = deckList[1];
        deck3 = deckList[2];
        deck4 = deckList[3];
        deck5 = deckList[4];
        deck6 = deckList[5];

        // Los armamos
        ArmarMazo1();
        ArmarMazo2();
        ArmarMazo3();
        ArmarMazo4();
        ArmarMazo5();
        ArmarMazo6();
    }

    void ArmarMazo1()
    {
        for (int i = 0; i < 10; i++)
        {
            //Debug.Log(i + " " + cardList.cardList[i]);
            deck1.AddCard(cardList.cardList[i]);
        }
    }
    void ArmarMazo2()
    {
        for (int i = 0; i < 10; i++)
        {
            deck2.AddCard(cardList.cardList[i]);
        }
    }
    void ArmarMazo3()
    {
        for (int i = 0; i < 10; i++)
        {
            deck3.AddCard(cardList.cardList[i]);
        }
    }
    void ArmarMazo4()
    {
        for (int i = 0; i < 10; i++)
        {
            deck4.AddCard(cardList.cardList[i]);
        }
    }
    void ArmarMazo5()
    {
        for (int i = 0; i < 10; i++)
        {
            deck5.AddCard(cardList.cardList[i]);
        }
    }
    void ArmarMazo6()
    {
        for (int i = 0; i < 10; i++)
        {
            deck6.AddCard(cardList.cardList[i]);
        }
    }

}

