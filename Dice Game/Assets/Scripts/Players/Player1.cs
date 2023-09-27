using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1
{
    public string name;
    public Deck currentDeck;
    public int hp;
    public int armour;
    public int mArmour;
    public Player1(string name, Deck startingDeck, int hp, int armour, int mArmour)
    {
        this.name = name;
        this.currentDeck = startingDeck;
        this.hp = hp;
        this.armour = armour;
        this.mArmour = mArmour;
    }
}