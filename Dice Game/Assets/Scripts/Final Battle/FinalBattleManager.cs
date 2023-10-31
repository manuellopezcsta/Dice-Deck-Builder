using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class FinalBattleManager : MonoBehaviour
{

    /// Se va a encargar de manejar los combates.

    public Player player1;
    public Player player2;

    // Elementos de la Interfaz
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

    // Datos para las cartas mazos etc.
    [Header("Cosas relacionadas a turnos")]
    public Turno currentTurn = Turno.P1;
    // Crear clon del mazo de los jugadores.
    Deck p1InsideCombatDeck;
    Deck p2InsideCombatDeck;

    Phase currentPhase = Phase.Drawing; // 0 waiting for draw, 1 after draw , 2 y 3 After you play your 2 cards.
    public List<Card> playerHand = new List<Card>();
    // Los controladores logicos de las cartas
    [Header("Cartas en Mano Scripts")]
    [SerializeField] CardLogicFinalBattle card1;
    [SerializeField] CardLogicFinalBattle card2;
    [SerializeField] CardLogicFinalBattle card3;
    [SerializeField] CardLogicFinalBattle card4;
    [SerializeField] CardLogicFinalBattle card5;
    [SerializeField] CardLogicFinalBattle card6;

    [Header("Valores para balancear")]
    public int poisonDotValue = 5;

    [Header("Cosas de Displays")]
    [SerializeField] private List<DiceDisplay> diceDisplays = new List<DiceDisplay>();
    public PlayerDisplay p1Display;
    public PlayerDisplay p2Display;

    [Header("Cosas de UI")]
    //Aca veremos los objetos que cambiaran de posicion segun el turno;
    [SerializeField] private RectTransform dadosActuales;
    [SerializeField] private RectTransform dadosFalsos;
    [SerializeField] private RectTransform cartasActuales;
    [SerializeField] private RectTransform cartasFalsas;
    [SerializeField] private Vector3 ubicacionCarta1;
    [SerializeField] private Vector3 ubicacionCarta2;
    [SerializeField] private Vector3 ubicacionDado1;
    [SerializeField] private Vector3 ubicacionDado2;

    [Header("Cosas de GameOverScreen")]
    [SerializeField] GameObject combatPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI textGameOver;


    public enum Turno
    {
        P1,
        P2,
    } // Agregar algo q cambie de turno.

    public void SwitchToNextPhase()
    {
        switch (currentTurn)
        {
            case Turno.P1:

                cartasActuales.anchoredPosition = ubicacionCarta1;
                cartasFalsas.anchoredPosition = ubicacionCarta2;
                dadosActuales.anchoredPosition = ubicacionDado1;
                dadosFalsos.anchoredPosition = ubicacionDado2;
                currentPhase += 1;
                if ((int)currentPhase == 3)
                {
                    currentTurn = Turno.P2;
                    currentPhase = Phase.Drawing;
                    Debug.Log("//      Se inicio turno Player 2.");
                    StartCoroutine(PopUpManager.instance.ShowCurrentTurnPopUp("Turno de " + player2.name)); // REPARAR
                }
                break;
            case Turno.P2:

                cartasActuales.anchoredPosition = ubicacionCarta2;
                cartasFalsas.anchoredPosition = ubicacionCarta1;
                dadosActuales.anchoredPosition = ubicacionDado2;
                dadosFalsos.anchoredPosition = ubicacionDado1;
                currentPhase += 1;
                if ((int)currentPhase == 3)
                {
                    currentTurn = Turno.P1;
                    currentPhase = Phase.Drawing;
                    Debug.Log("//      Se inicio turno enemigo.");
                    StartCoroutine(PopUpManager.instance.ShowCurrentTurnPopUp("Turno de " + player1.name)); // REPARAR
                }
                break;
        }
    }

    public enum Phase
    {
        Drawing,
        WaitingForAction1,
        WaitingForAction2
    }

    public static FinalBattleManager instance = null;

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
        player1 = GuardaRopas.instance.player1;
        player2 = GuardaRopas.instance.player2;
        // Los curamos
        player1.currentHp = player1.MaxHp;
        player2.currentHp = player2.MaxHp;
        EnterCombat();
        // Cargamos los datos del combate.
        LoadCombatData();
    }

    public void EnterCombat()
    {
        Debug.Log("Entrando en Combate.");
        // Seteamos los decks.
        p1InsideCombatDeck = ShuffleDeck(player1.currentDeck);
        p2InsideCombatDeck = ShuffleDeck(player2.currentDeck);
    }

    void Update()
    {
        if (currentPhase == Phase.Drawing && (currentTurn == Turno.P1 || currentTurn == Turno.P2))
        {
            Debug.Log("Starting Draw Phase");
            ShowMissingHand(); // Nos aseguramos que esten todas las cartas al comenzar.

            ShowMissingDice();
            Debug.Log("PrendioTodo");
            // Actualizamos las cartas y cambiamos a la phase de esperar acciones.
            DrawHand();
            Debug.Log("Draw Phase");
            // Actualizamos el visual de la mano
            UpdatePlayerHandImages();
            RollDicesAndUpdate(); // Roleamos los dados y Mostramos las imagenes.
                                  //currentPhase = Phase.WaitingForAction1;
            SwitchToNextPhase();
            Debug.Log("Waiting for action 1");
            // Revisamos el veneno del player actual. y actualizamos la UI x las dudas de que tenga veneno.
            GetPlayerN(currentTurn).Envenenado();
            UIUpdateAfterCardPlayed();
        }

        // Checkeamos para ver si termina el combate.
        CheckForEndOfCombat();
    }


    void DrawHand()
    {
        // Funcion que te arma la mano y muestra esas cartas.
        // Armamos una list de 6 cartas y las sacamos de p1InsideCombatDeck,
        //REVISANDO ANTES SU LENGH, si es menor, sacamos una cant igual a la leng y despues rehacemos p1InsideCombatDeck = player1.current deck
        // Y sacamos las que faltan.

        // Reseteamos la mano.
        playerHand = playerHand = new List<Card>();

        switch (currentTurn)
        {
            case Turno.P1:

                //Debug.Log("CANTIDAD DE CARTAS ACTUALES EN EL MAZO: " + p1InsideCombatDeck.cardCount);
                if (p1InsideCombatDeck.cardCount >= 6)
                {
                    // Robamos las 6 cartas.
                    for (int i = 0; i < 6; i++)
                    {
                        //Debug.Log("I: " + i +" V: " + p1InsideCombatDeck.cards[0]+ " LEN: " + p1InsideCombatDeck.cardCount);
                        playerHand.Add(p1InsideCombatDeck.cards[0]);
                        // Eliminamos el item que agregamos del mazo temporal.
                        p1InsideCombatDeck.cards.Remove(p1InsideCombatDeck.cards[0]);
                        p1InsideCombatDeck.cardCount -= 1;
                    }
                }
                else
                {
                    // Si hay menos de 6 cartas
                    int missingCards = 6 - p1InsideCombatDeck.cardCount;

                    // Robamos las cartas que si hay.
                    int cardsLeftInsideTheDeck = p1InsideCombatDeck.cardCount;
                    for (int i = 0; i < cardsLeftInsideTheDeck; i++)
                    {
                        playerHand.Add(p1InsideCombatDeck.cards[0]);
                        p1InsideCombatDeck.cards.Remove(p1InsideCombatDeck.cards[0]);
                        p1InsideCombatDeck.cardCount -= 1;
                    }
                    //Debug.Log("Se agarraron las cartas que faltaban en el mazo quedan: " + p1InsideCombatDeck.cardCount);
                    // Mezclamos el mazo
                    p1InsideCombatDeck = ShuffleDeck(player1.currentDeck);
                    // Robamos las que faltan.
                    for (int i = 0; i < missingCards; i++)
                    {
                        playerHand.Add(p1InsideCombatDeck.cards[0]);
                        p1InsideCombatDeck.cards.Remove(p1InsideCombatDeck.cards[0]);
                        p1InsideCombatDeck.cardCount -= 1;
                    }
                }
                break;

            case Turno.P2:
                if (p2InsideCombatDeck.cardCount >= 6)
                {
                    // Robamos las 6 cartas.
                    for (int i = 0; i < 6; i++)
                    {
                        //Debug.Log("I: " + i + " V: " + p2InsideCombatDeck.cards[0] + " LEN: " + p2InsideCombatDeck.cardCount);
                        playerHand.Add(p2InsideCombatDeck.cards[0]);
                        // Eliminamos el item que agregamos del mazo temporal.
                        p2InsideCombatDeck.cards.Remove(p2InsideCombatDeck.cards[0]);
                        p2InsideCombatDeck.cardCount -= 1;
                    }
                }
                else
                {
                    // Si hay menos de 6 cartas
                    int missingCards = 6 - p2InsideCombatDeck.cardCount;

                    // Robamos las cartas que si hay.
                    int cardsLeftInsideTheDeck = p2InsideCombatDeck.cardCount;

                    for (int i = 0; i < cardsLeftInsideTheDeck; i++)
                    {
                        playerHand.Add(p2InsideCombatDeck.cards[0]);
                        p2InsideCombatDeck.cards.Remove(p2InsideCombatDeck.cards[0]);
                        p2InsideCombatDeck.cardCount -= 1;
                    }
                    // Mezclamos el mazo
                    p2InsideCombatDeck = ShuffleDeck(player2.currentDeck);
                    // Robamos las que faltan.
                    for (int i = 0; i < missingCards; i++)
                    {
                        playerHand.Add(p2InsideCombatDeck.cards[0]);
                        p2InsideCombatDeck.cards.Remove(p2InsideCombatDeck.cards[0]);
                        p2InsideCombatDeck.cardCount -= 1;
                    }
                }
                break;
        }
    }

    void RollDicesAndUpdate()
    {
        switch (currentTurn)
        {
            case Turno.P1:
                // Roleamos los dados
                foreach (Dice dado in player1.dices)
                {
                    dado.Roll();
                    // Si mi compa;ero esta potenciado , le sumamos su valor 
                    dado.currentValue += player2.potenciado;
                }
                // y despues lo volvemos 0.
                player2.potenciado = 0;
                // Actualizamos las imagenes de los dados
                int k = 0;
                foreach (DiceDisplay display in diceDisplays)
                {
                    display.UpdateDisplay(player1.dices[k].currentValue);
                    k++;
                }
                break;
            case Turno.P2:
                // Roleamos los dados
                foreach (Dice dado in player2.dices)
                {
                    dado.Roll();
                    // Si mi compa;ero esta potenciado , le sumamos su valor 
                    dado.currentValue += player1.potenciado;
                }
                // y despues lo volvemos 0.
                player1.potenciado = 0;
                // Actualizamos las imagenes de los dados
                int j = 0;
                foreach (DiceDisplay display in diceDisplays)
                {
                    display.UpdateDisplay(player2.dices[j].currentValue);
                    j++;
                }
                break;
        }
    }

    void CheckForEndOfCombat()
    {
        if (player1.currentHp <= 0)
        {
            SwitchToGameOver(player2);
        }
        if (player2.currentHp <= 0)
        {
            SwitchToGameOver(player1);
        }
    }

    // COMPLETAR
    void SwitchToGameOver(Player player) // REHACER
    {
        textGameOver.text = player.name + " obtiene la banana suprema";
        SwitchPanels();
        Debug.Log("PERDISTE, CAMBIANDO A GAME OVER SCREEN");
    }

    void SwitchPanels()
    {
        combatPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }


    public void LoadCombatData()
    {

        //Cargamos al player 1
        p1Sprite.sprite = player1.sprite;
        p1HpBar.fillAmount = (float)player1.currentHp / player1.MaxHp;
        p1ArmourText.text = player1.armour.ToString();
        p1MrText.text = player1.mArmour.ToString();

        //Cargamos al player 2
        p2Sprite.sprite = player2.sprite;
        p2HpBar.fillAmount = (float)player2.currentHp / player2.MaxHp;
        p2ArmourText.text = player2.armour.ToString();
        p2MrText.text = player2.mArmour.ToString();

        // Activamos el turno correctamente
        currentTurn = Turno.P1;
        currentPhase = Phase.Drawing; // Reseteamos la fase para que este listo para el proximo combate.
    }

    // Llamarla cuando hace algo una carta
    public void UpdateUI()
    {
        // Actualiza la UI si cambia un valor.
        //Cargamos al player 1
        p1HpBar.fillAmount = (float)player1.currentHp / player1.MaxHp;
        p1ArmourText.text = player1.armour.ToString();
        p1MrText.text = player1.mArmour.ToString();

        //Cargamos al player 2
        p2HpBar.fillAmount = (float)player2.currentHp / player2.MaxHp;
        p2ArmourText.text = player2.armour.ToString();
        p2MrText.text = player2.mArmour.ToString();
    }

    // La intencion es que esto se llame despues de jugar 1 carta.
    public void UIUpdateAfterCardPlayed()
    {
        // Updateamos el display de los players
        p1Display.UpdateDisplay();
        p2Display.UpdateDisplay();
    }

    private Deck ShuffleDeck(Deck deckToShuffle)
    {
        // Creamos un mazo vacio.
        Deck outputDeck = new Deck(new List<Card>());
        // Creamos una lista de numeros y los mezclamos
        List<int> cardNumbers = new List<int>();

        for (int i = 0; i < deckToShuffle.cardCount; i++)
        {
            cardNumbers.Add(i);
        }
        ShuffleList(cardNumbers);
        //cardNumbers.ForEach(p => Debug.Log("CARD NUMBERS: " + p));

        // Ahora con la lista mezclada agregamos las cartas en ese orden.
        for (int j = 0; j < cardNumbers.Count; j++)
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
        // Nos aseguramos que estan todas prendidas primero.
        ShowMissingHand();

        // Actualizamos las cartas.
        card1.SwitchCardImage();
        card2.SwitchCardImage();
        card3.SwitchCardImage();
        card4.SwitchCardImage();
        card5.SwitchCardImage();
        card6.SwitchCardImage();
    }

    void ShowMissingHand()
    {
        // Muestra las cartas que faltan cuando pasa de turno.
        card1.gameObject.SetActive(true);
        card2.gameObject.SetActive(true);
        card3.gameObject.SetActive(true);
        card4.gameObject.SetActive(true);
        card5.gameObject.SetActive(true);
        card6.gameObject.SetActive(true);
    }

    void ShowMissingDice()
    {
        // Muestra los dados faltantes.
        foreach (DiceDisplay dice in diceDisplays)
        {
            dice.gameObject.SetActive(true);
        }
    }

    public Player GetPlayerN(int number)
    {
        if (number == 1)
        {
            return player1;
        }
        else
        {
            return player2;
        }
    }

    public Player GetPlayerN(Turno turn)
    {
        if (turn == Turno.P1)
        {
            return player1;
        }
        else if (turn == Turno.P2)
        {
            return player2;
        }
        return null; // ERROR posible.
    }

    public Player GetAlly(Turno turn)
    {
        if (turn == Turno.P1)
        {
            return player2;
        }
        else if (turn == Turno.P2)
        {
            return player1;
        }
        return null; // ERROR posible.
    }
}