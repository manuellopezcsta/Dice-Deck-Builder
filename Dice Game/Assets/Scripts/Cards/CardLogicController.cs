using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardLogicController : MonoBehaviour
{
    [SerializeField] private Sprite faceSprite;
    [SerializeField] private Sprite backSprite;
   
    [SerializeField] int uniqueID;
    Image cardImage;
    private RectTransform itemTransform;
    private Card carta; // Referencia de la carta se define cuando se cargan las imagenes.

    //prueba carta

    private bool coroutineAllowed;
    private bool faceUp;


    void Start()
    {

        cardImage = GetComponent<Image>();
        itemTransform = GetComponent<RectTransform>();
        coroutineAllowed = true;
        faceUp = false;
 
        FlipCard();
    }
    public void SwitchCardImage()
    {
        coroutineAllowed = true;
        carta = CombatManager.instance.playerHand[uniqueID - 1];
        //Debug.Log("PLAYER HAND SIZE: " + CombatManager.instance.playerHand.Count); // ESTO TA ROTO
        
        faceSprite = carta.image;
        cardImage.sprite = backSprite;
        FlipCard();
    }

    public void MakeItBig()
    {
        // Makes the card big when its selected
        itemTransform.position = itemTransform.position + new Vector3(0f, 25f, 0f);
    }

    public void BackToNormal()
    {
        itemTransform.position = itemTransform.position - new Vector3(0f, 25f, 0f);
        // Makes the card back to its normal size.
        itemTransform.localScale = new Vector3(1.4f,1.8f,1f);
    }

    public Card GetCard()
    {
        return carta;
    }
    private IEnumerator RotateCard()
    {
        coroutineAllowed = false;
        if (!faceUp)
        {
            for (float i = 0f; i >= -360; i -= 5f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == -270f)
                {
                    cardImage.sprite = faceSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        coroutineAllowed = true;
        faceUp = !faceUp;
        
    }
    public void FlipCard()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(RotateCard());
        }
    }

}
