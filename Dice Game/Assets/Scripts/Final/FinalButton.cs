using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalButton : MonoBehaviour
{
    public void FinalB () 
    {
        SceneManager.LoadScene("credits");
    }

    public void BotonCreditos() {
        SceneManager.LoadScene("Title");
        
    }
}
