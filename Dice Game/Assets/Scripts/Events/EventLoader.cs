using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EventLoader : MonoBehaviour
{
    [SerializeField] Image displayArt;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI boton1text;
    [SerializeField] TextMeshProUGUI boton2text;
    [SerializeField] Button boton1;
    [SerializeField] Button boton2;
    [SerializeField] Button botonVolver;
    int modifierRange = 7; // Se usa para el rango de efecto.
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
        int index = OverWorldManager.instance.currentButton - 1;
        Event evento = (Event)GameManager.instance.GetNodeData(index);
        // We load the data
        displayArt.sprite = evento.displayArt;
        description.text = evento.textAndChoices[0];
        boton1text.text = evento.textAndChoices[1];
        boton2text.text = evento.textAndChoices[2];
        // Si el evento es de historia o forzado, apagamos el segundo boton de nuevo.
        if (evento.name.StartsWith("historia") || evento.name.StartsWith("forced"))
        {
            Debug.Log("Evento de historia o forzado detectado, apagando segundo boton.");
            boton2.interactable = false;
        }
    }

    public void ClickVolver()
    {
        botonVolver.interactable = false;
        GameManager.instance.SetGameState(GameManager.GAME_STATE.OVERWORLD);
    }

    public void ClickB1()
    {
        int index = OverWorldManager.instance.currentButton - 1;
        Event evento = (Event)GameManager.instance.GetNodeData(index);

        // Prendemos el boton de volver y apagamos el resto
        botonVolver.interactable = true;
        boton1.interactable = false;
        boton2.interactable = false;
        // Actualizamos el texto segun el resultado y ejecuta la ganancia o perdida de recursos.
        ResolveButton1Clicked(evento);
    }

    public void ClickB2()
    {
        int index = OverWorldManager.instance.currentButton - 1;
        Event evento = (Event)GameManager.instance.GetNodeData(index);

        description.text = evento.textAndChoices[5];
        botonVolver.interactable = true;
        boton1.interactable = false;
        boton2.interactable = false;
    }

    void ResolveButton1Clicked(Event evento)
    {
        Player player1 = CombatManager.instance.GetPlayerN(1);
        Player player2 = CombatManager.instance.GetPlayerN(2);
        evento.modifier = Random.Range(1, modifierRange + 1); // El valor va a ser positivo pero despues lo puedo cambiar a negativo cuando lo necesito.

        int holder = Random.Range(0, 2); // 0 bad ending , 1 good ending.
        bool goodEnding = holder == 1;

        Debug.Log("EL MODIFIER ES DE : " + evento.modifier);
        Debug.Log("GOOD ENDING ES: " + goodEnding);

        switch (evento.name)
        {
            case "heal":  // Te curas o tomas dano perforante.
                if (goodEnding)
                {
                    // Cambiamos la descripcion del evento con lo que paso.
                    description.text = evento.textAndChoices[3];
                    player1.currentHp = Mathf.Clamp(player1.currentHp + evento.modifier, 0, player1.MaxHp);
                    player2.currentHp = Mathf.Clamp(player2.currentHp + evento.modifier, 0, player2.MaxHp);
                }
                else
                {
                    description.text = evento.textAndChoices[4];
                    player1.currentHp = Mathf.Clamp(player1.currentHp - evento.modifier, 1, player1.MaxHp);
                    player2.currentHp = Mathf.Clamp(player2.currentHp - evento.modifier, 1, player2.MaxHp);
                }
                break;
            case "take dmg":
                if (goodEnding) // Logras esquivar el dano
                {
                    description.text = evento.textAndChoices[3];
                }
                else
                { // Te comes el da;o
                    description.text = evento.textAndChoices[4];
                    player1.currentHp = Mathf.Clamp(player1.currentHp - evento.modifier, 1, player1.MaxHp);
                    player2.currentHp = Mathf.Clamp(player2.currentHp - evento.modifier, 1, player2.MaxHp);
                }
                break;
            case "gain armour": // Ganas o perdes armadura
                if (goodEnding)
                {
                    description.text = evento.textAndChoices[3];
                    player1.armour = Mathf.Clamp(player1.armour + evento.modifier, 0, player1.maxArmour);
                    player2.armour = Mathf.Clamp(player2.armour + evento.modifier, 0, player2.maxArmour);
                }
                else
                {
                    description.text = evento.textAndChoices[4];
                    player1.armour = Mathf.Clamp(player1.armour - evento.modifier, 1, player1.maxArmour);
                    player2.armour = Mathf.Clamp(player2.armour - evento.modifier, 1, player2.maxArmour);
                }
                break;
            case "gain mr":
                if (goodEnding) // Ganas o perdes mr
                {
                    description.text = evento.textAndChoices[3];
                    player1.mArmour = Mathf.Clamp(player1.mArmour + evento.modifier, 0, player1.maxMArmour);
                    player2.mArmour = Mathf.Clamp(player2.mArmour + evento.modifier, 0, player2.maxMArmour);
                }
                else
                {
                    description.text = evento.textAndChoices[4];
                    player1.mArmour = Mathf.Clamp(player1.mArmour - evento.modifier, 1, player1.maxMArmour);
                    player2.mArmour = Mathf.Clamp(player2.mArmour - evento.modifier, 1, player2.maxMArmour);
                }
                break;
            case "historia1": // Un evento de historia comun
                description.text = evento.textAndChoices[3]; // Siempre sale el primer texto..
                GameManager.instance.secretEndingCounter += 1;
                break;
            case "focedTrampa": // Un evento de trampa forzado.
                if (goodEnding) // Safas o no de la trampra.
                {
                    description.text = evento.textAndChoices[3]; // Safaste
                }
                else
                {
                    description.text = evento.textAndChoices[4];
                }
                break;
        }
    }
}
