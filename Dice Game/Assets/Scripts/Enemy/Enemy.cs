using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public string name;
    public int hp;
    public int armour;
    //public Deck deck;  x si le damos inteligencia y mazos luego.

    public Enemy(string name, int hp, int armour)
    {
        this.name = name;
        this.hp = hp;
        this.armour = armour;
    }
}
