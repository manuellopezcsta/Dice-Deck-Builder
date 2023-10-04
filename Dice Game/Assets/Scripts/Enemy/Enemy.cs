using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Si tira error el inspector es xq estas viendo el Enemy Manager, borrar esto. y listo, no se va a ver la lista pero no ta el error.
public class Enemy : CombatesYEventos
{
    public string name;
    public int hp;
    public int currentHp;
    public int armour;
    public int mArmour;
    public Sprite img;
    //public Deck deck;  x si le damos inteligencia y mazos luego.

    public Enemy(Sprite img, string name, int hp, int armour = 0,int mArmour = 0)
    {
        this.name = name;
        this.hp = hp;
        this.currentHp = hp;
        this.armour = armour;
        this.mArmour = mArmour;
        this.img = img;
    }

    public Sprite GetSprite()
    {
        return img;
    }

    public void DebugInfo()
    {
        Debug.Log("NAME: " +this.name +" HP: " +this.hp + " CURRENTHP: " + this.currentHp + " ARMOUR:" + this.armour + " MR: " + this.mArmour);
    }

    public void TomarDaño(int valor)
    {
        int danioPasa = 999;
        // Si tiene armadura
        if(armour > 0)
        {
            // Si tengo mas  armadura que el ataque
            if(armour > valor)
            {
                danioPasa = 0;
                armour -= valor;
            }
            else{
                danioPasa = valor - armour;
                armour = 0;
            }
        } else {
            // SI no tengo armadura
            danioPasa = valor;
        }
        // Igualamos
        currentHp -= danioPasa;
    }

    public void TomarDañoMagico(int valor)
    {
        int danioPasa = 999;
        // Si tiene armadura
        if(mArmour > 0)
        {
            // Si tengo mas  armadura que el ataque
            if(mArmour > valor)
            {
                danioPasa = 0;
                mArmour -= valor;
            }
            else{
                danioPasa = valor - mArmour;
                mArmour = 0;
            }
        } else {
            // SI no tengo armadura
            danioPasa = valor;
        }
        // Igualamos
        currentHp -= danioPasa;
    }    
}
