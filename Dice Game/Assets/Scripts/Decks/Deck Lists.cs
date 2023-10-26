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
    int[] deckIndex1 = { 4, 3, 3, 0, 1, 13 };
    int[] deckIndex2 = { 3, 2, 0, 1, 13, 14 };
    int[] deckIndex3 = { 9, 2, 0, 6, 10, 12 };
    int[] deckIndex4 = { 2, 0, 0, 6, 13, 13 };
    int[] deckIndex5 = { 2, 8, 6, 7, 4, 3 };



    public void Start()
    {
        deck1 = deckList[0];
        deck2 = deckList[1];
        deck3 = deckList[2];
        deck4 = deckList[3];
        deck5 = deckList[4];

        // Los armamos
        ArmarMazo1();
        ArmarMazo2();
        ArmarMazo3();
        ArmarMazo4();
        ArmarMazo5();
    }

    public Deck GetDeckN(int n)
    {
        Deck result = null;
        switch (n)
        {
            case 1:
                result = deckList[0];
                break;
            case 2:
                result = deckList[1];
                break;
            case 3:
                result = deckList[2];
                break;
            case 4:
                result = deckList[3];
                break;
            case 5:
                result = deckList[4];
                break;
        }
        return result;
    }


    void ArmarMazo1()
    {
        for (int i = 0; i < 6; i++)
        {
            //Debug.Log(i + " " + cardList.cardList[i]);
            //deck1.AddCard(cardList.cardList[i]);
            deck1.AddCard(cardList.cardList[deckIndex1[i]]);
        }
    }
    void ArmarMazo2()
    {
        for (int i = 0; i < 6; i++)
        {
            //deck2.AddCard(cardList.cardList[i]);
            deck2.AddCard(cardList.cardList[deckIndex2[i]]);
        }
    }
    void ArmarMazo3()
    {
        for (int i = 0; i < 6; i++)
        {
            //deck3.AddCard(cardList.cardList[i]);
            deck3.AddCard(cardList.cardList[deckIndex3[i]]);
        }
    }
    void ArmarMazo4()
    {
        for (int i = 0; i < 6; i++)
        {
            //deck4.AddCard(cardList.cardList[i]);
            deck4.AddCard(cardList.cardList[deckIndex4[i]]);
        }
    }
    void ArmarMazo5()
    {
        for (int i = 0; i < 6; i++)
        {
            //deck5.AddCard(cardList.cardList[i]);
            deck5.AddCard(cardList.cardList[deckIndex5[i]]);
        }
    }
}