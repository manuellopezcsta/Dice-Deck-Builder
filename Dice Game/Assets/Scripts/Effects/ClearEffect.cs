using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    public void SelfDestroyAfterAnimEnded()
    {
        Destroy(gameObject);
    }
}
