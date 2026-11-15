using System.Collections.Generic;
using UnityEngine;

public class BlackjackManager : Singleton<BlackjackManager>
{
    PlayerHand playerHand;
    VampHand vampHand;
    Card vampStashedCard;
    public Deck deck;

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
    }

    public void EndRound()
    {
        
    }

    public void PlayerHit()
    {
        
    }

    public void PlayerStand()
    {
        
    }

    public void PlayerBust()
    {
        
    }

    public void PlayerRaise()
    {
        
    }

    public void VampireHit()
    {
        
    }

    public void VampireStand()
    {
        
    }

    public void VampireBust()
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
    
}

class VampHand : Hand
{
    
}