using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
        cardCount += 1;
    }

    public void RemoveCard(Card CardToRemove)
    {
        cards.Remove(CardToRemove);
        cardCount -= 1;
    }
}
