using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // Reference list of all cards currently in play
    List<int> allCards;

    // Current deck state
    Queue<int> deckState;

    System.Random random;

    void Awake()
    {
        allCards = (List<int>)Enumerable.Range(0, 52);
        deckState = new();
        random = new();
    }

    public void Shuffle()
    {
        List<int> copy = new(allCards);
        deckState.Clear();

        while (copy.Count > 0)
        {
            int i = random.Next(0, copy.Count);
            deckState.Enqueue(copy[i]);
            copy.Remove(copy[i]);
        }
    }

    public Card Draw()
    {
        int pulledCard = deckState.Dequeue();

        Card card = new(pulledCard);
        return card;
    }

    public void StealCard(Card card)
    {
        allCards.Remove(card.index);
    }

    public void ReturnCard(Card card)
    {
        allCards.Add(card.index);
    }

    public void RemoveFromDeck(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            allCards.Remove(card.index);
        }
    }
}

public class Card
{
    public readonly Suite suite;
    public readonly Rank rank;
    public readonly int index;

    public Card(int i)
    {
        suite = (Suite)(i / 13);
        rank = (Rank)(i % 13);
        index = i;
    }

    public Card(Suite suite, Rank rank)
    {
        this.suite = suite;
        this.rank = rank;
        index = (int)rank + (int)suite * 13;
    }
}

public enum Suite
{
    HEART, DIAMOND, SPADE, CLUB
}

public enum Rank
{
    ACE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING
}