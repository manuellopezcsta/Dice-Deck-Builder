using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1
{
    public string name;
    public Deck currentDeck;
    public Player1(string name, Deck startingDeck)
    {
        this.name = name;
        this.currentDeck = startingDeck;
    }
}