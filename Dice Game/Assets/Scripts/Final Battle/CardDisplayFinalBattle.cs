
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class CardDisplayFinalBattle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // CUANDO SE UTILIZA EL CANVAS NO SE PUEDE USAR ON MOUSE DOWN ETC, X ESO TENGO Q USAR LOS ONPOINTER ... CON la herencia de los eventos y el event system arriba.
    [SerializeField] GameObject cardDisplay;
    Image imageToShow;
    [SerializeField] private Vector3 ubicacionCartaExplicacion1;
    [SerializeField] private Vector3 ubicacionCartaExplicacion2;
    [SerializeField] private RectTransform cartaExplicacion;

    public void OnPointerEnter(PointerEventData eventData)
    {
     
        //Debug.Log("Se entro a "+ gameObject.name);
        // obtenemos la imagen de este objeto
        imageToShow = gameObject.transform.GetComponent<Image>();
        // La seteamos en el card Display
        cardDisplay.GetComponent<Image>().sprite = imageToShow.sprite;
        cardDisplay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Se salio de "+ gameObject.name);
        cardDisplay.SetActive(false);
    }

}
