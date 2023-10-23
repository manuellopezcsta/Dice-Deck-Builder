using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadOutPanel : MonoBehaviour
{
    public List<ImageList> characterDecks; // 1 lista es 1 pj... cada lista tiene las 6 cartas iniciales de su mazo.
    public List<StringList> characterDices; // Todos los dados de cada 1.
    public List<StringList> characterStats; // Todos los stats de cada 1. Salud Armadura y Mr en ese orden.

    [Header("Datos que se llenan")]
    [SerializeField] List<Image> cards;
    [SerializeField] List<TextMeshProUGUI> dices;
    [SerializeField] List<TextMeshProUGUI> stats;

    void OnEnable()
    {
        LoadCharacterData();
    }

    void LoadCharacterData()
    {
        // Aca cargamos la data del pj seleccionado.
        int selected = PlayerPrefs.GetInt("selected"); // Va de 1 a 5
        // Cargamos Cartas
        int index = 0;
        foreach(Sprite card in characterDecks[selected - 1].innerList)
        {
            cards[index].sprite = card;
            index ++;
        }
        // Cargamos los dados.
        index = 0;
        foreach(String dice in characterDices[selected - 1].innerList)
        {
            dices[index].text = dice;
            index ++;
        }
        // Cargamos las stats.
        index = 0;
        foreach(String stat in characterStats[selected - 1].innerList)
        {
            stats[index].text = stat;
            index ++;
        }
    }
}
