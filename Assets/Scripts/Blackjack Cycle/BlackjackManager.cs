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