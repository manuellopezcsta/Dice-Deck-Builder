using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public TextMeshProUGUI date;
    //private string strBase;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateText("3294 D.C."));
    }

    IEnumerator AnimateText(string text){
        for(int i = 0; i < text.Length; i++) {
            date.text+=text[i];
            yield return new WaitForSeconds(0.5f);
        }
    }
}
