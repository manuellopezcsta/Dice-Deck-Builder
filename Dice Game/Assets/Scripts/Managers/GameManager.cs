using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random; // Para que sepa de donde saco el random.


public interface CombatesYEventos
{
    // Es como la herencia le definis algo que es obligatoria implementarla en todas la clases que la hereden.
    public Sprite GetSprite();
}

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Utilizado para algo seguramente... cambiar de overworld x ahi al terminar un nivel?.
    /// </summary>

    [SerializeField] GameObject transitionObject;
    [SerializeField] private Image transition;
    public float fillSpeed = 1f;
    public float waitTime = 1f;
    [SerializeField] private GAME_STATE currentGameState = GAME_STATE.OVERWORLD;
    private GAME_STATE newGameState = GAME_STATE.OVERWORLD;
    // Para el combate
    [SerializeField] private GameObject combatPanel;
    [SerializeField] private GameObject overWorldPanel = null;
    [SerializeField] List<CombatesYEventos> combatesYEventos = new List<CombatesYEventos>();
    public Enemy currentEnemy;// Indicador del enemigo actual


    public enum GAME_STATE
    {
        TUTORIAL,
        OVERWORLD,
        ON_COMBAT
    }

    public static GameManager instance = null;

    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(AssignOverWorld()); // Por alguna razon necesite correrlo como corutina para que lo encuentre.
    }

    private IEnumerator AssignOverWorld()
    {
        while (!overWorldPanel)
        {
            yield return new WaitForSeconds(0.1f);
            overWorldPanel = GameObject.FindGameObjectWithTag("LEVEL");
            if (!overWorldPanel)
            {
                break;
            }
        }
        //Debug.Log("Se asigno valor a " + overWorldPanel.name);
        yield return null;
    }

    public void SetGameState(GAME_STATE state)
    {
        newGameState = state;
    }

    void Update()
    {
        if (currentGameState != newGameState)
        {
            // Si cambiamos de pantalla actualizamos el estado y hacemos un efecto de transicion.
            currentGameState = newGameState;
            TransitionEffect();
        }
    }

    void TransitionEffect()
    {
        StartCoroutine(FillAndEmpty());
    }

    void SwitchPanels(GAME_STATE state)
    {
        switch (state)
        {
            case GAME_STATE.TUTORIAL:
                break;
            case GAME_STATE.OVERWORLD:
                overWorldPanel.SetActive(true);
                combatPanel.SetActive(false);
                break;
            case GAME_STATE.ON_COMBAT:
                overWorldPanel.SetActive(false);
                combatPanel.SetActive(true);
                // Cargamos la data del combate.
                CombatManager.instance.LoadCombatData();
                break;
        }
    }

    private IEnumerator FillAndEmpty()
    {
        transitionObject.SetActive(true);
        yield return Fill();
        yield return new WaitForSeconds(waitTime);

        // Codigo antes de vaciar la transicion
        SwitchPanels(newGameState);

        yield return Empty();
        transitionObject.SetActive(false);
        Debug.Log("Se termino transicion");
    }

    private IEnumerator Fill()
    {
        float currentValue = transition.fillAmount;
        while (currentValue < 1f)
        {
            currentValue += Time.deltaTime * fillSpeed;
            transition.fillAmount = currentValue;
            yield return null;
        }
    }

    private IEnumerator Empty()
    {
        float currentValue = transition.fillAmount;
        while (currentValue > 0f)
        {
            currentValue -= Time.deltaTime * fillSpeed;
            transition.fillAmount = currentValue;
            yield return null;
        }
    }

    public void BuildLevelMap()
    {
        // Primer y Segundo nodo combate
        combatesYEventos.Add(EnemyManager.instance.enemiesList[0]);
        combatesYEventos.Add(EnemyManager.instance.enemiesList[1]);
        // El resto puede ser cualquier cosa
        for (int i = 0; i < 3; i++)
        {
            int b = Random.Range(0, 2);
            if (b == 0)
            {
                //Agregar Combate
                combatesYEventos.Add(EnemyManager.instance.enemiesList[0]);
                EnemyManager.instance.enemiesList.RemoveAt(0);

            }
            else
            {
                // Agregar Evento
                combatesYEventos.Add(EventManager.instance.eventList[0]);
                EventManager.instance.eventList.RemoveAt(0);
            }
        }
    }

    public CombatesYEventos GetNodeData(int index)
    {
        return combatesYEventos[index];
    }

    // Se encarga de activar los eventos o los combates.
    public void HandleNodeAction(int index)
    {
        CombatesYEventos action = combatesYEventos[index];
        // Si es un combate
        if(action.GetType() == typeof(Enemy))
        {
            Debug.Log("Ejecutando Combate.");
            currentEnemy = (Enemy) action;
            //currentEnemy.DebugInfo();
            CombatManager.instance.EnterCombat();
        }
        // Si es un evento
        if(action.GetType() == typeof(Event))
        {
            Debug.Log("Ejecutando Evento.");
            EventManager.instance.ResolveEvent((Event) action);
        }

    }
}
