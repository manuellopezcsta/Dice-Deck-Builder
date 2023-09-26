using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public int cardCount;
    public List<Card> cards;
    public Deck(List<Card> cards)
    {
        cardCount = cards.Count;
        this.cards = cards;
    }

    public void AddCard(Card cardToAdd)
    {
        cards.Add(cardToAdd);
    }

    public void RemoveCard(Card CardToRemove)
    {
        cards.Remove(CardToRemove);
    }
}
