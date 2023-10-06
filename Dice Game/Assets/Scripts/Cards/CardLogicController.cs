using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardLogicController : MonoBehaviour
{
    [SerializeField] int uniqueID;
    Image cardImage;
    private RectTransform itemTransform;
    private Card carta; // Referencia de la carta se define cuando se cargan las imagenes.

    void Start()
    {
        cardImage = GetComponent<Image>();
        itemTransform = GetComponent<RectTransform>();
    }
    public void SwitchCardImage()
    {
        carta = CombatManager.instance.playerHand[uniqueID - 1];
        //Debug.Log("PLAYER HAND SIZE: " + CombatManager.instance.playerHand.Count); // ESTO TA ROTO
        cardImage.sprite = carta.image;
    }

    public void MakeItBig()
    {
        // Makes the card big when its selected
        itemTransform.localScale = new Vector3(1.4f,1.4f,1.4f);
    }

    public void BackToNormal()
    {
        // Makes the card back to its normal size.
        itemTransform.localScale = new Vector3(1f,1f,1f);
    }

    public Card GetCard()
    {
        return carta;
    }
}
