using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

[Serializable]
public class Event : CombatesYEventos
{
    public string name;
    public int modifier;
    public Sprite mini;
    public Sprite displayArt;
    public string[] textAndChoices; // Texto explicatorio, y las 2 opciones.

    public Event(Sprite mini, string name, Sprite displayArt, string[] textAndChoices)
    {   this.modifier = Random.Range(-5,6);
        this.name = name;
        this.mini = mini;
        this.displayArt = displayArt;
        this.textAndChoices = textAndChoices;
    }

        public Sprite GetSprite()
        {
            return mini;
        }
}