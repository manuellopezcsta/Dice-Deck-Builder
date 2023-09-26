using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public string cardName;
    public Sprite image;
    public CardEffects effect; 

    public Card(string cardName, Sprite image, CardEffects effect)
    {
        this.cardName = cardName;
        this.image = image;
        this.effect = effect;
    }

    public enum CardEffects {
        ATAQUE,
        CURAR,
        ARMADURA,
        ETC
    }
}
