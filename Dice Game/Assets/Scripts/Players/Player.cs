using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    public string name;
    public Deck currentDeck;
    public int MaxHp;
    public int currentHp;
    public int armour;
    public int mArmour;
    public Sprite sprite;
    public Player(string name, Deck startingDeck, int MaxHp, int armour, int mArmour, Sprite img = null)
    {
        this.name = name;
        this.currentDeck = startingDeck;
        this.MaxHp = MaxHp;
        this.armour = armour;
        this.mArmour = mArmour;
        this.currentHp = MaxHp;
        this.sprite = img;
    }
}