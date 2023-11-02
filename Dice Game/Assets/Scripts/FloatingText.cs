using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // Para popUps y efectos.
    [SerializeField] float destroyTime;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
