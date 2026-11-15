using System.Collections.Generic;
using UnityEngine;

public class BlackjackManager : Singleton<BlackjackManager>
{
    PlayerHand playerHand;
    VampHand vampHand;
    Card vampStashedCard;
    public Deck deck;

    override protected void Awake()
    {
        base.Awake();
        playerHand = new();
        vampHand = new();
    }

    public void StartRound()
    {
        
    }
}

abstract class Hand
{
    public List<Card> cards;
    public GameObject[] cardDisplay;

    public virtual void AddCard(Card card)
    {
        cards.Add(card);
    }

    public virtual void ClearHand()
    {
        cards.Clear();
    }
}

class PlayerHand : Hand
{
    
}

class VampHand : Hand
{
    
}