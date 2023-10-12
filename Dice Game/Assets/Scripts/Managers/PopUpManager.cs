using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager instance = null;
    [SerializeField] Transform popUpHolder; // Se instancian aca.
    [SerializeField] GameObject popUpP1; // Player 1
    [SerializeField] GameObject popUpP2; // Player 2
    [SerializeField] GameObject popUpP3; // Enemigo
    [SerializeField] List<GameObject> queuedEvents = new List<GameObject>();
    public float timeBetweenPopUps;

    [Header("Turn Indicator")]
    [SerializeField] Transform turnIndicatorHolder; // Se instancian aca.
    [SerializeField] GameObject turnPrefab; // Player 1
    public float waitTimeTurnIndicator;

    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        if (instantiateObjects == null && queuedEvents.Count != 0)
        {
            instantiateObjects = StartCoroutine(InstantiateObjects());
        }
    }

    void StopPopUps()
    {
        instantiateObjects = null;
        queuedEvents = new List<GameObject>();
    }

    public enum POPUPTARGET
    {
        PLAYER1,
        PLAYER2,
        ENEMY
    }
    public void GeneratePopUp(string popUpText, POPUPTARGET target)
    {
        GameObject popUp = null;
        // Switch de ver cual genero. 
        switch (target)
        {
            case POPUPTARGET.PLAYER1:
                popUp = Instantiate(popUpP1, popUpHolder);
                break;
            case POPUPTARGET.PLAYER2:
                popUp = Instantiate(popUpP2, popUpHolder);
                break;
            case POPUPTARGET.ENEMY:
                popUp = Instantiate(popUpP3, popUpHolder);
                break;
        }
        // Le doy el valor del texto.
        TextMeshProUGUI text = popUp.GetComponent<TextMeshProUGUI>();
        text.text = popUpText;
        popUp.SetActive(false);
        queuedEvents.Add(popUp);
    }

    Coroutine instantiateObjects = null;
    private IEnumerator InstantiateObjects()
    {
        List<GameObject> clone = queuedEvents.ToList(); ;
        foreach (GameObject obj in clone)
        {
            // Si se termino el combate
            if (!CombatManager.instance.FightingAnEnemy())
            {
                StopPopUps();
                yield break;
            }

            // Comportamiento comun de la rutina.
            obj.SetActive(true);
            queuedEvents.RemoveAt(0);
            yield return new WaitForSeconds(timeBetweenPopUps);
        }
        instantiateObjects = null;
    }

    // Returns the value to check if the coroutine is running, to lock dice .
    public bool ArePopUpsActive()
    {
        return instantiateObjects != null;
    }

    // Para el indicador de turnos
    //public Coroutine showCurrentTurnPopUp = null;
    public IEnumerator ShowCurrentTurnPopUp(string popUpText)
    {
        // Generamos el prefab
        GameObject popUp = Instantiate(turnPrefab, turnIndicatorHolder);
        // Le doy el valor del texto. (turno)
        TextMeshProUGUI text = popUp.GetComponent<TextMeshProUGUI>();
        text.text = popUpText;
        //showCurrentTurnPopUp = null;
        yield return new WaitForSeconds(waitTimeTurnIndicator);
    }
}