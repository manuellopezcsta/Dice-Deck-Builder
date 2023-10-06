using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    [SerializeField] 
    private Canvas canvas;

    public void dragHandler(BaseEventData data){//drag dado
        PointerEventData pointerData =(PointerEventData) data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle( 
            (RectTransform)canvas.transform,
            pointerData.position, //posicion del puntero
            canvas.worldCamera,
            out position);
        transform.position = canvas.transform.TransformPoint(position);
    }
}
