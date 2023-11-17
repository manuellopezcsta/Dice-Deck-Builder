using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class ClearEffect : MonoBehaviour
{
    public SFXManager.VFXName effect;
    public void SelfDestroyAfterAnimEnded()
    {
        Destroy(gameObject);
    }

    void OnEnable()
    {
        if(SFXManager.instance != null) // Para testear arrancando el juego en la game scene.
        {
            SFXManager.instance.PlayVFX(effect);
        }
    }


}
