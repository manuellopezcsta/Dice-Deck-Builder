using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceDisplay : MonoBehaviour
{
    TextMeshProUGUI diceValueDisplay;

    void Start()
    {
        diceValueDisplay = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDisplay(int value)
    {
        // Activamos el objeto antes de updatearle el texto por las dudas.
        gameObject.SetActive(true);
        diceValueDisplay.text = value.ToString();
    }
}
