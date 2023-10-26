using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceLogicBattle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private BoxCollider2D boxCollider;
    private RectTransform imageTransform;
    private Canvas canvas;
    private Vector3 originalPos;
    private int originalSiblingPosition;
    [SerializeField] bool isCollidingWithCard; // Lo usamos para ver si estaba chocando con la carta al momento de soltar el mouse.
    [SerializeField] GameObject collidedCard; // Referencia a la carta con la que choco.

    [SerializeField] int uniqueID; // Para identificar que dado es.

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        imageTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPos = imageTransform.position;
        originalSiblingPosition = imageTransform.GetSiblingIndex();
        //Debug.Log("START POS: X "+ originalPos.x + " Y " + originalPos.y + " Z " + originalPos.z);
    }

    // Al hacer click
    public void OnPointerDown(PointerEventData eventData)
    {
        //if (eventData.button == PointerEventData.InputButton.Left && !PopUpManager.instance.ArePopUpsActive())
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            imageTransform.SetAsLastSibling(); // Mueve la imagen al frente para que esté por encima de otras imágenes
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (eventData.button == PointerEventData.InputButton.Left && !PopUpManager.instance.ArePopUpsActive())
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out pos);
            imageTransform.position = canvas.transform.TransformPoint(pos);
        }
    }

    // Al soltar el mouse
    public void OnPointerUp(PointerEventData eventData) // Al soltar el boton del mouse
    {
        //if (eventData.button == PointerEventData.InputButton.Left && !PopUpManager.instance.ArePopUpsActive())
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(isCollidingWithCard) // Si esta chocando con carta
            {
                //Debug.Log("Boop! activando codigo de " + collidedCard.GetComponent<CardLogicFinalBattle>().GetCard().cardName);
                RunCardCode(collidedCard.GetComponent<CardLogicFinalBattle>().GetCard()); // Le pasamos el codigo de esa carta.

                // Ocultamos el dado que usamos. Y LA CARTA
                collidedCard.SetActive(false); // Carta. 1ero sino apagaria el codigo del dado y no encontraria la carta.
                gameObject.SetActive(false); // Dado

            }
            imageTransform.SetAsFirstSibling(); // Para que se debuge. si no lo hago el ultimo dado queda en una posicion rara.
            imageTransform.SetSiblingIndex(originalSiblingPosition); // Para que vuelva a su pos original.
            imageTransform.position = originalPos; // Hacemos que vuelva al soltarlo.
            //Debug.Log("X "+ imageTransform.position.x + " Y " + imageTransform.position.y + " Z " + imageTransform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CARD"))
        {
            //Debug.Log("Colisión con " + collision.gameObject.name.ToString() + " detectada.");
            collidedCard = collision.gameObject;
            collidedCard.GetComponent<CardLogicFinalBattle>().MakeItBig(); // La agrandamos para que sea mas facil con los colliders.
            isCollidingWithCard = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CARD"))
        {
            //Debug.Log("Colisión con " + collision.gameObject.name.ToString() + " termino.");
            collidedCard.GetComponent<CardLogicFinalBattle>().BackToNormal();
            isCollidingWithCard = false;
            collidedCard = null; 
        }
    }

    private void RunCardCode(Card card)
    {
        Player p1 = FinalBattleManager.instance.GetPlayerN(1);
        Player p2 = FinalBattleManager.instance.GetPlayerN(2);

        if(FinalBattleManager.instance.currentTurn == FinalBattleManager.Turno.P1)
        {
            card.RunLogic(card, p1.dices[uniqueID - 1]); // Ejecutamos el switch que ejecuta las funciones.
        }
        if(FinalBattleManager.instance.currentTurn == FinalBattleManager.Turno.P2)
        {
            card.RunLogic(card, p2.dices[uniqueID - 1]);
        }
        // Actualizamos la interfaz para ver que hizo la carta.
        FinalBattleManager.instance.UpdateUI();

        // Avanzamos el turno actual despues de jugar una carta
        FinalBattleManager.instance.SwitchToNextPhase();
    }
}