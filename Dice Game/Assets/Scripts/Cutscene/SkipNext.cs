using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Collections;

public class SkipNext : MonoBehaviour
{
    int i=11;
    float x, y;

   // Vector3 escala;
    public TextMeshProUGUI textoBoton;
    public void Button(){
        SceneManager.LoadScene("CharSelection");
    }

    private void Start() {
        x=transform.localScale.x;
        y=transform.localScale.y;
    }

    public void ReceiveIntroEnd(){
        textoBoton.text="NEXT";
        i=0;
        StartCoroutine(AnimateButton());
    }
    IEnumerator AnimateButton(){
        while(i<11){
            for(;i<5;i++){
                x+= 0.1f;
                y+= 0.1f;
                transform.localScale= new Vector3(x,y,1);
                yield return new WaitForSeconds(0.1f);
            }
            for(;i<10;i++){
                x-= 0.1f;
                y-= 0.1f;
                transform.localScale= new Vector3(x,y,1);
                yield return new WaitForSeconds(0.1f);
            }
            i=0;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
