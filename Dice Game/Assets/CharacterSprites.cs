using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSprites : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private Player p1, p2;

    [SerializeField] PlayerList playerList;
    
    void Start()
    {
        
        p1=playerList.GetPlayerFromListN(1);
        player1.gameObject.GetComponent<Image>().sprite = p1.spriteFront;
        p2=playerList.GetPlayerFromListN(2);
        player2.gameObject.GetComponent<Image>().sprite = p2.spriteFront;//Carga los sprites correspondientes elegidos
        Debug.Log("Loading panel 1 sprites");
    }

    
}
