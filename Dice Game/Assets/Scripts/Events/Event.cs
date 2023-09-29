using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : CombatesYEventos
{
    public string name;
    public int modifier;
    public Sprite img;
    //public Deck deck;  x si le damos inteligencia y mazos luego.

    public Event(Sprite img, string name, int modifier = 0)
    {   this.modifier = modifier;
        this.name = name;
        this.img = img;
    }

        public Sprite GetSprite()
        {
            return img;
        }
}