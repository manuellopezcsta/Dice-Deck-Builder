using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalManager : MonoBehaviour
{
    private Player winner;
    public GameObject playerFront;
    public GameObject playerBack;

    // Start is called before the first frame update
    void Start()
    {
        winner=FinalBattleManager.finalBattleManager.winner;
        playerFront.gameObject.GetComponent<Image>().sprite=winner.spriteFront;
        playerBack.gameObject.GetComponent<Image>().sprite=winner.sprite;

    }

    public void Next() 
    {
        if(GuardaRopas.instance.secretEndingScore>=4){
            SceneManager.LoadScene("SecretEnding");
        }
        else{
            SceneManager.LoadScene("Credits");
        }
    }
}
