using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventLoader : MonoBehaviour
{
    [SerializeField] Image displayArt;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI boton1text;
    [SerializeField] TextMeshProUGUI boton2text;
    [SerializeField] Button boton1;
    [SerializeField] Button boton2;
    [SerializeField] Button botonVolver;
    string output1;
    string output2;

    void OnEnable()
    {
        // Preparamos los botones
        botonVolver.interactable = false;
        boton1.interactable = true;
        boton2.interactable = true;
        // Cargamos el evento con datos.
        FillData();
    }

    void FillData()
    {
        int index = OverWorldManager.instance.currentButton -1;
        Event evento = (Event) GameManager.instance.GetNodeData(index);
        // We load the data
        displayArt.sprite = evento.displayArt;
        description.text = evento.textAndChoices[0];
        boton1text.text = evento.textAndChoices[1];
        boton2text.text = evento.textAndChoices[2];
    }

    public void ClickVolver()
    {
        botonVolver.interactable = false;
        GameManager.instance.SetGameState(GameManager.GAME_STATE.OVERWORLD);
    }

    public void ClickB1()
    {
        int index = OverWorldManager.instance.currentButton -1;
        Event evento = (Event) GameManager.instance.GetNodeData(index);
        // Cambiamos la descripcion del evento con lo que paso.
        description.text = evento.textAndChoices[3];
        // Prendemos el boton de volver y apagamos el resto
        botonVolver.interactable = true;
        boton1.interactable = false;
        boton2.interactable = false;
    }

    public void ClickB2()
    {
        int index = OverWorldManager.instance.currentButton -1;
        Event evento = (Event) GameManager.instance.GetNodeData(index);

        description.text = evento.textAndChoices[4];
        botonVolver.interactable = true;
        boton1.interactable = false;
        boton2.interactable = false;
    }
}
