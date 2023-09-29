using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    /// <summary>
    /// Se va a encargar de manejar los combates.
    /// </summary>
    
    private Enemy enemy;
    private Player1 p1;
    private Player2 p2;

    public static CombatManager instance = null;

    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }

    public void EnterCombat()
    {
        GameManager.instance.SetGameState(GameManager.GAME_STATE.ON_COMBAT);
        Debug.Log("Entrando en Combate.");
    }

    public void ExitCombat()
    {
        GameManager.instance.SetGameState(GameManager.GAME_STATE.OVERWORLD);
        Debug.Log("Saliendo de combate o accion.");
    }
}
