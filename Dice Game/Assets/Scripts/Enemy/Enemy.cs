using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Si tira error el inspector es xq estas viendo el Enemy Manager, borrar esto. y listo, no se va a ver la lista pero no ta el error.
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
