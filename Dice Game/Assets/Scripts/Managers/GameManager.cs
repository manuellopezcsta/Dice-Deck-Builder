using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
}
