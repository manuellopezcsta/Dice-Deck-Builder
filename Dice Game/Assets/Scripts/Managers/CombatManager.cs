using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    /// <summary>
    /// Se va a encargar de manejar los combates.
    /// </summary>

    private Enemy enemy;
    private Player player1;
    private Player player2;
    [SerializeField] DeckLists decks; // Script que tiene los mazos de todos los players

    // Datos que van a ser cargados en combate ( algunos los sacamos de las clases).

    // Elementos de la Interfaz
    [Header("Enemy")]
    [SerializeField] private TextMeshProUGUI enemyName;
    [SerializeField] private Image enemySprite;
    [SerializeField] private Image enemyHpBar;
    [SerializeField] private TextMeshProUGUI enemyArmourText;
    [SerializeField] private TextMeshProUGUI enemyMrText;

    [Header("Player 1")]
    [SerializeField] private Image p1Sprite;
    [SerializeField] private Image p1HpBar;
    [SerializeField] private TextMeshProUGUI p1ArmourText;
    [SerializeField] private TextMeshProUGUI p1MrText;

    [Header("Player 2")]
    [SerializeField] private Image p2Sprite;
    [SerializeField] private Image p2HpBar;
    [SerializeField] private TextMeshProUGUI p2ArmourText;
    [SerializeField] private TextMeshProUGUI p2MrText;

    [Header("Temporal")]
    [SerializeField] private Sprite imagenJ1;
    [SerializeField] private Sprite imagenJ2;

    // Datos para las cartas mazos etc.
    [Header("Cosas de Mazos")]



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

    void Start()
    {
        // Creamos los mazos de los players.
        Deck p1Deck = decks.deckList[0];
        Deck p2Deck = decks.deckList[1];
        // Creamos los players
        player1 = new Player("Jugador1", p1Deck, 100, 10, 0,imagenJ1);
        player2 = new Player("Jugador2", p2Deck, 200, 0, 10, imagenJ2);
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

    public void LoadCombatData()
    {
        // Cargamos al enemigo
        enemy = GameManager.instance.currentEnemy;
        enemyName.text = enemy.name;
        enemySprite.sprite = enemy.img;
        enemyHpBar.fillAmount = 1;
        enemyArmourText.text = enemy.armour.ToString();
        enemyMrText.text = enemy.mArmour.ToString();

        //Cargamos al player 1
        p1Sprite.sprite = player1.sprite;
        p1HpBar.fillAmount = 1;
        p1ArmourText.text = player1.armour.ToString();
        p1MrText.text = player1.mArmour.ToString();

        //Cargamos al player 2
        p2Sprite.sprite = player2.sprite;
        p2HpBar.fillAmount = 1;
        p2ArmourText.text = player2.armour.ToString();
        p2MrText.text = player2.mArmour.ToString();
    }

    // Llamarla cuando hace algo una carta
    public void UpdateUI()
    {
        // Actualiza la UI si cambia un valor.
        enemyHpBar.fillAmount = enemy.currentHp/ enemy.hp;
        enemyArmourText.text = enemy.armour.ToString(); 
        enemyMrText.text = enemy.mArmour.ToString();

        //Cargamos al player 1
        p1HpBar.fillAmount = player1.currentHp/ player1.MaxHp;
        p1ArmourText.text = player1.armour.ToString();
        p1MrText.text = player1.mArmour.ToString();

        //Cargamos al player 2
        p2HpBar.fillAmount = player2.currentHp/ player2.MaxHp;
        p2ArmourText.text = player2.armour.ToString();
        p2MrText.text = player2.mArmour.ToString();
    }
}
