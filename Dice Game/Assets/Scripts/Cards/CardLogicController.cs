using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardLogicController : MonoBehaviour
{
    [SerializeField] int uniqueID;
    Image cardImage;

    void Start()
    {
        cardImage = GetComponent<Image>();
    }
    public void SwitchCardImage()
    {
        cardImage.sprite = CombatManager.instance.playerHand[uniqueID - 1].image;
    }
}
