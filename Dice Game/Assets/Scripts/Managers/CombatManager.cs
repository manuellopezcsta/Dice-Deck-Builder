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
    [Header("Cosas relacionadas a turnos")]
    public Turno currentTurn = Turno.P1; 
    // Crear clon del mazo de los jugadores.
    Deck p1InsideCombatDeck;
    Deck p2InsideCombatDeck;
    // Variable para controlar el Update
    bool fightingEnemy = false;
    Phase currentPhase = Phase.Drawing; // 0 waiting for draw, 1 after draw , 2 y 3 After you play your 2 cards.
    public List<Card> playerHand = new List<Card>();
    // Los controladores logicos de las cartas
    [Header("Cartas en Mano Scripts")]
    [SerializeField] CardLogicController card1;
    [SerializeField] CardLogicController card2;
    [SerializeField] CardLogicController card3;
    [SerializeField] CardLogicController card4;
    [SerializeField] CardLogicController card5;
    [SerializeField] CardLogicController card6;
    

    public enum Turno {
        P1,
        P2,
        ENEMY
    } // Agregar algo q cambie de turno.

    public void SwitchToNextPhase()
    {
        // Cambia el turno al que sigue.
        // MISING CASOS BORDES ? q pasa si el enum sobre pasa los limites ?
        currentPhase += 1;

    }

    public enum Phase {
        Drawing,
        WaitingForAction1,
        WaitingForAction2
    }




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
        // Seteamos los decks.
        p1InsideCombatDeck = ShuffleDeck(player1.currentDeck);
        p2InsideCombatDeck = ShuffleDeck(player2.currentDeck);
    }

    void Update()
    {
        if(fightingEnemy)
        {
            if(currentPhase == Phase.Drawing)
            {
                Debug.Log("Starting Draw Phase");
                // Actualizamos las cartas y cambiamos a la phase de esperar acciones.
                DrawHand();
                // Actualizamos el visual de la mano
                UpdatePlayerHandImages();
                Debug.Log("Waiting for action 1");
                currentPhase = Phase.WaitingForAction1;
            }
            // Checkeamos para ver si termina el combate.
            CheckForEndOfCombat();
        }
    }

    void DrawHand()
    {
        // Funcion que te arma la mano y muestra esas cartas.
        // Armamos una list de 6 cartas y las sacamos de p1InsideCombatDeck,
        //REVISANDO ANTES SU LENGH, si es menor, sacamos una cant igual a la leng y despues rehacemos p1InsideCombatDeck = player1.current deck
        // Y sacamos las que faltan.

        // Reseteamos la mano.
        playerHand = playerHand = new List<Card>();

        switch(currentTurn)
        {
            case Turno.P1:
                if(p1InsideCombatDeck.cardCount >= 6)
                {
                    // Robamos las 6 cartas.
                    for(int i = 0; i < 6; i++)
                    {
                        //Debug.Log("I: " + i +" V: " + p1InsideCombatDeck.cards[0]+ " LEN: " + p1InsideCombatDeck.cardCount);
                        playerHand.Add(p1InsideCombatDeck.cards[0]);
                        // Eliminamos el item que agregamos del mazo temporal.
                        p1InsideCombatDeck.cards.Remove(p1InsideCombatDeck.cards[0]);
                        p1InsideCombatDeck.cardCount -= 1;
                    }
                } else {
                    // Si hay menos de 6 cartas
                    int missingCards = 6 - p1InsideCombatDeck.cardCount;

                    // Robamos las cartas que si hay.
                    for(int i = 0;i < p1InsideCombatDeck.cardCount; i++)
                    {
                        playerHand.Add(p1InsideCombatDeck.cards[0]);
                        p1InsideCombatDeck.cards.Remove(p1InsideCombatDeck.cards[0]);
                        p1InsideCombatDeck.cardCount -= 1;
                    }
                    // Mezclamos el mazo
                    p1InsideCombatDeck = ShuffleDeck(player1.currentDeck);
                    // Robamos las que faltan.
                    for(int i = 0;i < missingCards; i++)
                    {
                        playerHand.Add(p1InsideCombatDeck.cards[0]);
                        p1InsideCombatDeck.cards.Remove(p1InsideCombatDeck.cards[0]);
                        p1InsideCombatDeck.cardCount -= 1;
                    }
                }
                break;

            case Turno.P2:
                if(p2InsideCombatDeck.cardCount >= 6)
                {
                    // Robamos las 6 cartas.
                    for(int i = 0; i < 6; i++)
                    {
                        Debug.Log("I: " + i +" V: " + p2InsideCombatDeck.cards[0]+ " LEN: " + p2InsideCombatDeck.cardCount);
                        playerHand.Add(p2InsideCombatDeck.cards[0]);
                        // Eliminamos el item que agregamos del mazo temporal.
                        p2InsideCombatDeck.cards.Remove(p2InsideCombatDeck.cards[0]);
                        p2InsideCombatDeck.cardCount -= 1;
                    }
                } else {
                    // Si hay menos de 6 cartas
                    int missingCards = 6 - p2InsideCombatDeck.cardCount;

                    // Robamos las cartas que si hay.
                    for(int i = 0;i < p2InsideCombatDeck.cardCount; i++)
                    {
                        playerHand.Add(p2InsideCombatDeck.cards[0]);
                        p2InsideCombatDeck.cards.Remove(p2InsideCombatDeck.cards[0]);
                        p2InsideCombatDeck.cardCount -= 1;
                    }
                    // Mezclamos el mazo
                    p2InsideCombatDeck = ShuffleDeck(player2.currentDeck);
                    // Robamos las que faltan.
                    for(int i = 0;i < missingCards; i++)
                    {
                        playerHand.Add(p2InsideCombatDeck.cards[0]);
                        p2InsideCombatDeck.cards.Remove(p2InsideCombatDeck.cards[0]);
                        p2InsideCombatDeck.cardCount -= 1;
                    }
                }
                break;
        }
    }

    void CheckForEndOfCombat()
    {
        if(player1.currentHp <= 0 || player2.currentHp <= 0 || enemy.currentHp <= 0)
        {
            fightingEnemy = false;
            //Si perdes
            if(player1.currentHp <= 0 || player2.currentHp <= 0)
            {
                SwitchToGameOver();
            }
            // Si vences al enemigo
            Debug.Log("HP ENEMIGO: " + enemy.currentHp);
            if(enemy.currentHp <= 0)
            {
                ExitCombat();
            }
        }
    }

    // COMPLETAR
    void SwitchToGameOver()
    {
        // Agregar panel de game Over y colocar aqui las cosas para cambiar ahi x ahi otra escena o algo para hacerlo mas facil ? .
        Debug.Log("PERDISTE, CAMBIANDO A GAME OVER SCREEN");
    }


    public void ExitCombat()
    {
        // Agregar algo q diga ganaste ?
        GameManager.instance.SetGameState(GameManager.GAME_STATE.OVERWORLD);
        Debug.Log("Saliendo de combate o accion.");
    }

    public void LoadCombatData()
    {
        // Cargamos al enemigo
        enemy = GameManager.instance.currentEnemy;
        //Debug.Log("EN LOADCOMBATDATA");
        //enemy.DebugInfo();
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
        // Activamos el combate
        fightingEnemy = true;
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

    private Deck ShuffleDeck(Deck deckToShuffle)
    {
        // Creamos un mazo vacio.
        Deck outputDeck = new Deck(new List<Card>());
        // Creamos una lista de numeros y los mezclamos
        List<int> cardNumbers = new List<int>();

        for(int i=0; i < deckToShuffle.cardCount; i++)
        {
            cardNumbers.Add(i);
        }
        ShuffleList(cardNumbers);
        //cardNumbers.ForEach(p => Debug.Log("CARD NUMBERS: " + p));

        // Ahora con la lista mezclada agregamos las cartas en ese orden.
        for(int j = 0; j <cardNumbers.Count; j++)
        {
            outputDeck.AddCard(deckToShuffle.cards[cardNumbers[j]]);
        }

        return outputDeck;

        // Sacada de internet , no la entiendo.
        void ShuffleList<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    void UpdatePlayerHandImages()
    {
        card1.SwitchCardImage();
        card2.SwitchCardImage();
        card3.SwitchCardImage();
        card4.SwitchCardImage();
        card5.SwitchCardImage();
        card6.SwitchCardImage();
    }
}
