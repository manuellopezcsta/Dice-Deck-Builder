using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using Random = UnityEngine.Random; // Para que sepa de donde saco el random.


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
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] List<CombatesYEventos> combatesYEventos = new List<CombatesYEventos>();
    public Enemy currentEnemy;// Indicador del enemigo actual
    public List<Dice> defaultDiceList = new List<Dice>(); // Lista de dados Default
    // Para los eventos
    [SerializeField] GameObject eventPanel;
    public int secretEndingCounter = 0;

    //Musica para los niveles (overworld, combate, eventos)

    [SerializeField] private SFXManager sfxManager;

    public enum GAME_STATE
    {
        OVERWORLD,
        ON_COMBAT,
        ON_EVENT
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
        sfxManager = GameObject.Find("Sound Manager").GetComponent<SFXManager>();
        BuildDefaultDiceList(); // Armamos la lista de dados
        StartCoroutine(AssignOverWorld()); // Por alguna razon necesite correrlo como corutina para que lo encuentre.
    }

    private IEnumerator AssignOverWorld()
    {
        while (!overWorldPanel)
        {
            yield return new WaitForSeconds(0.1f);
            overWorldPanel = GameObject.FindGameObjectWithTag("LEVEL");
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
            case GAME_STATE.OVERWORLD:
                CheckForLevelCompletition();
                overWorldPanel.SetActive(true);
                combatPanel.SetActive(false);
                eventPanel.SetActive(false);
                string currentLevel = OverWorldManager.instance.currentLevelName;
                switch (currentLevel)
                {
                    case "NIVEL 1(Clone)":
                        if (sfxManager.GetAudioClip() != sfxManager.nivel1)
                        {
                            sfxManager.PlaySong(SFXManager.MSName.Nivel1);

                        }
                        break;
                    case "NIVEL 2(Clone)":
                        if (sfxManager.GetAudioClip() != sfxManager.nivel2)
                        {
                            sfxManager.PlaySong(SFXManager.MSName.Nivel2);
                        }
                        
                        break;
                    case "NIVEL 3(Clone)":
                        if (sfxManager.GetAudioClip() != sfxManager.nivel3)
                        {
                            sfxManager.PlaySong(SFXManager.MSName.Nivel3);
                        }
                        break;
                }
                break;
            case GAME_STATE.ON_COMBAT:
                overWorldPanel.SetActive(false);
                combatPanel.SetActive(true);
                // Cargamos la data del combate.
                CombatManager.instance.LoadCombatData();
                // Updateamos el display
                CombatManager.instance.UIUpdateAfterCardPlayed();
                Enemy enemy = CombatManager.instance.GetEnemy();
                if (enemy.name == "Jefe1" || enemy.name == "Jefe2" || enemy.name == "Jefe3")
                {
                    sfxManager.PlaySong(SFXManager.MSName.CombateBoss);
                }
                else {
                    sfxManager.PlaySong(SFXManager.MSName.CombatDefault);
                }
                    break;
            case GAME_STATE.ON_EVENT:
                eventPanel.SetActive(true);
                break;
        }
    }

    public void CheckForLevelCompletition()
    {
        if (OverWorldManager.instance.levelCompleted)
        {
            // Seteamos de nuevo a false
            OverWorldManager.instance.levelCompleted = false;
            // Borramos el viejo e iniciamos el nuevo.
            Destroy(overWorldPanel);
            overWorldPanel = null;
            // Limpiamos combates y eventos y generamos uno nuevo.
            combatesYEventos.Clear();
            BuildLevelMap();
            // Cambiamos al nuevo
            OverWorldManager.instance.ChangeToNextMap(OverWorldManager.instance.currentLevel);
            StartCoroutine(AssignOverWorld());
            // Porque el asignado tarda tiempo esperamos hasta que este.
            while (!overWorldPanel)
            {
                overWorldPanel = GameObject.FindGameObjectWithTag("LEVEL");
            }
            Debug.Log("Se paso con exito al siguiente nivel");
        }
    }

    public void StartEvent()
    {
        SetGameState(GAME_STATE.ON_EVENT);
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

    /*public void BuildLevelMap()
    {
        // Primer y Segundo nodo combate
        combatesYEventos.Add(EnemyManager.instance.enemiesList[0]);
        combatesYEventos.Add(EnemyManager.instance.enemiesList[1]);
        // Saco los primeros 2 para que despues no se dupliquen los enemigos.

        EnemyManager.instance.enemiesList.RemoveAt(0); // ARREGLAR. BUG ACA BUG
        EnemyManager.instance.enemiesList.RemoveAt(0);
        // El resto puede ser cualquier cosa
        for (int i = 0; i < 22; i++)
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
        // Agregamos los eventos forzados que hagan falta.
        ReplaceNodesWithForcedData();
    }*/
    public void BuildLevelMap()
    {
        switch (OverWorldManager.instance.currentLevel)
        {
            case 1:  // Primer mapa de nodos.
                // Primer y Segundo nodo combate
                combatesYEventos.Add(EnemyManager.instance.enemiesListLv1[0]);
                combatesYEventos.Add(EnemyManager.instance.enemiesListLv1[1]);
                // Saco los primeros 2 para que despues no se dupliquen los enemigos.
                EnemyManager.instance.enemiesListLv1.RemoveAt(0);
                EnemyManager.instance.enemiesListLv1.RemoveAt(0);
                ChooseEnemiesForNodeLevel(5, EnemyManager.instance.enemiesListLv1);
                break;
            case 3:  // Segundo mapa de nodos.
                ChooseEnemiesForNodeLevel(22, EnemyManager.instance.enemiesListLv2);
                break;
            case 6:  // Tercer mapa de nodos.
                ChooseEnemiesForNodeLevel(22, EnemyManager.instance.enemiesListLv3);
                break;
            default: // Para el resto de los niveles.(JEFES ETC, tiene que tener algo para que puedan remplazarse despues)
                Debug.Log(" SE ENTRO AL DEFAULT !!! del generador");
                ChooseEnemiesForNodeLevel(2, EnemyManager.instance.enemiesListLv3);
                break;
        }

        // Agregamos los eventos forzados que hagan falta.
        ReplaceNodesWithForcedData();
    }
    void ChooseEnemiesForNodeLevel(int quantity, List<Enemy> list)
    {
        for (int i = 0; i < quantity; i++)
        {
            int b = Random.Range(0, 2);
            if (b == 0)
            {
                //Agregar Combate
                combatesYEventos.Add(list[0]);
                list.RemoveAt(0);
            }
            else
            {
                // Agregar Evento
                combatesYEventos.Add(EventManager.instance.eventList[0]);
                EventManager.instance.eventList.RemoveAt(0);
            }
        }
    }
    void ReplaceNodesWithForcedData()
    {
        // Aca ponemos los datos forzados como jefes y eventos segun el level..
        switch (OverWorldManager.instance.currentLevel)
        {
            case 2: // Mapa jefe 1
                combatesYEventos[0] = EnemyManager.instance.bossList[0];
                break;
            case 4: // Mapa jefe 2
                combatesYEventos[0] = EnemyManager.instance.bossList[1];
                break;
            case 5: // Mapa jefe 3
                combatesYEventos[0] = EnemyManager.instance.bossList[2];
                break;
            // PARA LOS EVENTOS DE HISTORIA
            case 3: // Mapa de nodos 2
                //combatesYEventos[2] = EventManager.instance.storyEventList[0];
                //combatesYEventos[9] = EventManager.instance.storyEventList[1];
                //combatesYEventos[7] = EventManager.instance.storyEventList[2];
                break;
            case 6: // Mapa de nodos 3
                //combatesYEventos[2] = EventManager.instance.storyEventList[3];
                //combatesYEventos[8] = EventManager.instance.storyEventList[4];
                //combatesYEventos[11] = EventManager.instance.storyEventList[5];
                //combatesYEventos[14] = EventManager.instance.storyEventList[6];
                //combatesYEventos[17] = EventManager.instance.storyEventList[7];
                //combatesYEventos[20] = EventManager.instance.storyEventList[8];
                break;
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
        if (action.GetType() == typeof(Enemy))
        {
            Debug.Log("Ejecutando Combate.");
            currentEnemy = (Enemy)action;
            //currentEnemy.DebugInfo();
            CombatManager.instance.EnterCombat();
        }
        // Si es un evento
        if (action.GetType() == typeof(Event))
        {
            Debug.Log("Ejecutando Evento.");
            EventManager.instance.ResolveEvent((Event)action);
        }

    }

    private void BuildDefaultDiceList()
    {
        // Armamos la lista default para despues crear players con dados.
        DiceList diceListScript = GameObject.Find("Dice List").GetComponent<DiceList>();
        List<Dice> globalDices = diceListScript.diceList;

        defaultDiceList.Add(globalDices[0]);
        defaultDiceList.Add(globalDices[1]);
        defaultDiceList.Add(globalDices[2]);
        defaultDiceList.Add(globalDices[3]);
    }

    public IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void SwitchToGameOverScreen()
    {
        combatPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void SwitchToRewardsScreen()
    {
        //combatPanel.SetActive(false);
        rewardPanel.SetActive(true);
    }
   public GameObject GetCombatPanel(){
    return combatPanel;
   }
   public GameObject GetOverworldPanel()
   {
    return overWorldPanel;
   }
}
