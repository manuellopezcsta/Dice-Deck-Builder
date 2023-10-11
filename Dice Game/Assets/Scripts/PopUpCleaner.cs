using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopUpCleaner : MonoBehaviour
{
    // Limpia los popUps al activarse.
    void OnEnable()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
