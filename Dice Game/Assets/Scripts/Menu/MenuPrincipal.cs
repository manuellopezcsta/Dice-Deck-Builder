using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Play() { 
        SceneManager.LoadScene("Intro");
        
    }

    public void Quit () {
        Application.Quit();
    }
}
