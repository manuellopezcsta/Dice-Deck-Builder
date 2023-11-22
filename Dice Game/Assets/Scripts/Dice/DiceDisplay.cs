using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DiceDisplay : MonoBehaviour
{
    TextMeshProUGUI diceValueDisplay;
    Animator animator;

    void Start()
    {
        diceValueDisplay = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        animator = gameObject.GetComponent<Animator>();
    }
    public void UpdateDisplay(int value)
    {
        gameObject.SetActive(true);
        diceValueDisplay.gameObject.SetActive(false);
        // Activamos el objeto antes de updatearle el texto por las dudas.
        animator.SetBool("GiroDado", true);
        diceValueDisplay.text = value.ToString();
    }
    public void PrenderNumero()
    {
        diceValueDisplay.gameObject.SetActive(true);
        animator.SetBool("GiroDado", false);
    }
} 
