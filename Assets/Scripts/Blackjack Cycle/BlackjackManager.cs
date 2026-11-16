using System.Collections.Generic;
using UnityEngine;

public class BlackjackManager : Singleton<BlackjackManager>
{
    PlayerHand playerHand;
    VampHand vampHand;
    Card vampStashedCard;
    public Deck deck;
    
    bool vampBust;
    bool playerBust;

    bool playerTurn;

    override protected void Awake()
    {
        base.Awake();
        playerHand = new();
        vampHand = new();
    }

    public void StartRound()
    {
        playerTurn = true;
        bustVamp = false;
        bustPlayer = false;
    }

    public void EndRound()
    {
        
    }

    public void PlayerHit()
    {
        if (playerTurn) 
        {
            playerHand.AddCard(deck.Draw());
        }
    }

    public void PlayerStand()
    {
        
    }

    public void PlayerRaise()
    {
        
    }

    public void VampireHit()
    {
        if (playerTurn)
        {
            if (!playerBust)
            vampHand.AddCard(deck.Draw());
        }
    }

    public void VampireStand()
    {
        
    }

    public void check(Hand h)
    {
        
    }

    public void VampireRaise()
    {
        
    }
}

abstract class Hand
{
    public List<Card> cards;
    public GameObject[] cardDisplay;
    int total;

    public void Hand();

    public virtual void AddCard(Card card)
    {
        cards.Add(card);
        total += (int)card.rank + 1;
    }

    public virtual void ClearHand()
    {
        cards.Clear();
    }
}

class PlayerHand : Hand
{
    PlayerHand() 
    {
        base();
    }
}

class VampHand : Hand
{
    VampHand() 
    {
        base();
    }
}