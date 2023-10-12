using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Se llama desde el boton
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
