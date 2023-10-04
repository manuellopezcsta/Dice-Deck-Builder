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