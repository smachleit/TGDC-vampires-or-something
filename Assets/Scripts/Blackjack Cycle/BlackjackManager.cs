using System.Collections.Generic;
using UnityEngine;

public class BlackjackManager : Singleton<BlackjackManager>
{
    PlayerHand playerHand;
    VampHand vampHand;
    Card vampStashedCard;
    public Deck deck;

    bool playerTurn;
    bool playerWinRound;

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
        Card card = deck.Draw();
        cardDisplays[playerHand.size].Draw(card);
        playerHand.AddCard(card);

        checkBust(playerHand);
    }

    public void checkBust(Hand h)
    {
        if (h.total > 21)
        {
            h.bust = true;
            EndRound();
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
        Card card = deck.Draw();
        cardDisplays[playerHand.size].Draw(card);
        playerHand.AddCard(card);

        checkBust(vampHand);
    }

    public void VampireStand()
    {
        
    }

    public void VampireRaise()
    {
        
    }
}