using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Si tira error el inspector es xq estas viendo el Enemy Manager, borrar esto. y listo, no se va a ver la lista pero no ta el error.
public class Enemy : CombatesYEventos
{
    public string name;
    public int hp;
    public int armour;
    public int mArmour;
    public Sprite img;
    //public Deck deck;  x si le damos inteligencia y mazos luego.

    public Enemy(Sprite img, string name, int hp, int armour = 0,int mArmour = 0)
    {
        this.name = name;
        this.hp = hp;
        this.armour = armour;
        this.mArmour = mArmour;
        this.img = img;
    }

    public Sprite GetSprite()
    {
        return img;
    }
}
