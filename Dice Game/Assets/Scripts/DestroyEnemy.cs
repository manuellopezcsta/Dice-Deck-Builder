using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    [SerializeField] private bool hack;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (hack)
            {
                hack = false;
            }
            else {
                hack = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && hack)
        {
            GameManager.instance.DestroyEnemy();
        }
    }
}
