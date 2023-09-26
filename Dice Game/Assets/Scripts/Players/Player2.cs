using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2
{
    public string name;
    public Deck currentDeck;
    public Player2(string name, Deck startingDeck)
    {
        this.name = name;
        this.currentDeck = startingDeck;
    }
}
