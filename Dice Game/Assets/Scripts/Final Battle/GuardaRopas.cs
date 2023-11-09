using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardaRopas : MonoBehaviour
{
    // Encargado de transferir los datos entre la escena comun y el combate final
    public Player player1;
    public Player player2;
    public static GuardaRopas instance = null;
    public int secretEndingScore = 0;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }

    public void SaveData()
    {
        player1 = CombatManager.instance.GetPlayerN(1);
        player2 = CombatManager.instance.GetPlayerN(2);
        secretEndingScore = GameManager.instance.secretEndingCounter;
    }
}
