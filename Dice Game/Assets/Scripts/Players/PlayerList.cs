using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    [SerializeField] Sprite[] playerImage;
    [SerializeField] Sprite[] playerImageFront;
    [SerializeField] DeckLists decks;
    [SerializeField] DiceList dices;
    [SerializeField] Sprite[] miniSprite;
    public List<Player> list = new List<Player>();
    public Player debugPlayer;


    void Start()
    {
        FillPlayers();
    }

    void FillPlayers()
    {
        list.Add(new Player("Raijin", decks.GetDeckN(1), 20, 5, 10, GetDiceN(1), playerImage[0], playerImageFront[0], PopUpManager.POPUPTARGET.PLAYER1, miniSprite[0])); // Kaiju
        list.Add(new Player("Quatrio", decks.GetDeckN(2), 20, 10, 5, GetDiceN(2), playerImage[1], playerImageFront[1], PopUpManager.POPUPTARGET.PLAYER1, miniSprite[1])); // Mecha
        list.Add(new Player("Lisbet", decks.GetDeckN(3), 15, 10, 10, GetDiceN(3), playerImage[2], playerImageFront[2], PopUpManager.POPUPTARGET.PLAYER1, miniSprite[2])); // Valkyria
        list.Add(new Player("Murdock", decks.GetDeckN(4), 25, 5, 5, GetDiceN(4), playerImage[3], playerImageFront[3], PopUpManager.POPUPTARGET.PLAYER1, miniSprite[3])); // Waldogro
        list.Add(new Player("Lana", decks.GetDeckN(5), 10, 5, 5, GetDiceN(5), playerImage[4], playerImageFront[4], PopUpManager.POPUPTARGET.PLAYER1, miniSprite[4])); // Conejo
    }

    List<Dice> GetDiceN(int number) // va de 1 a 5.. es el num de personaje
    {
        List<Dice> result = new List<Dice>();

        for(int i= 0; i < 4; i++)
        {
            // Clonamos el dado.
            Dice holder = dices.diceList[number - 1].Clone();
            // Agregamos el dado nuevo
            result.Add(holder);
        }
        return result;
    }

    // Returns the value for player 1 or player 2 based on whats stored in player Prefs.
    public Player GetPlayerFromListN(int number)
    {
        Player result = null;
        switch(number)
        {
            case 1:
                result = list[PlayerPrefs.GetInt("player1") - 1];
                result.identifier = PopUpManager.POPUPTARGET.PLAYER1;
                break;
            case 2:
                result = list[PlayerPrefs.GetInt("player2") - 1];
                result.identifier = PopUpManager.POPUPTARGET.PLAYER2;
                break;
            default:
                Debug.Log("SE ACTIVO DEFAULT EN GET PLAYERN");
                break;
        }
        debugPlayer = result;
        return result;
    }
}
