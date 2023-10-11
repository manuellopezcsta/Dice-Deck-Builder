using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public static bool juegoPausado = false;

    public GameObject menuPausa;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(juegoPausado){
                Resumir();
            }else{
                Pausar();
            }
        }
    }
    public void Resumir(){
        menuPausa.SetActive(false);
        Time.timeScale=1f;
        juegoPausado=false;
    }

    void Pausar(){
        menuPausa.SetActive(true);
        Time.timeScale=0f;
        juegoPausado=true;
    }

    public void AbrirOpciones(){

    }

    public void IrAMenuPrincipal () {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
