using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlackjackManager : Singleton<BlackjackManager>
{
    PlayerHand playerHand;
    VampHand vampHand;
    Card vampStashedCard;
    public Deck deck;
<<<<<<< HEAD

    bool playerTurn;
    bool playerWinRound;
=======
    
    
    public CardDisplay[] cardDisplays;
    bool bustVamp;
    bool bustPlayer;
    bool playerTurn;
    bool playerBlackjack;
    bool vampBlackjack;

    DialogueManager dialogue;
    GameManager game;

    //input tracking
    InputAction move;
    InputAction enter;
    int selectedOption;
>>>>>>> gamestate

    override protected void Awake()
    {
        base.Awake();
        playerHand = new();
        vampHand = new();
        deck = new();
    }

    void Start()
    {
        dialogue = DialogueManager.Instance;
        move = InputSystem.actions.FindAction("NavigateVertical");
        move.performed += context => MoveCursor(context);
        enter = InputSystem.actions.FindAction("Enter");
        enter.performed += context => SelectAction();
    }

    public void test()
    {
        StartCoroutine(StartRound());
    }

    public IEnumerator StartRound()
    {
        playerTurn = true;
        bustVamp = false;
        bustPlayer = false;
        selectedOption = 1;
        WaitForSeconds pause = new(0.2f);

        // Both players draw two cards
        PlayerHit();
        yield return pause;
        VampireHit();
        yield return pause;
        PlayerHit();
        yield return pause;
        VampireHit();

        StartCoroutine(dialogue.RoundStart(game.vampConfidence));

        move.Enable();
        enter.Enable();
        PlayerActions.Instance.ActivateActions();


        yield return new WaitUntil(() => !playerTurn);
        move.Disable();
        enter.Enable();
        PlayerActions.Instance.DeactivateActions();
    }

    public void EndRound()
    {
        
    }

    public void PlayerHit()
    {
        Card card = deck.Draw();
        cardDisplays[playerHand.size].Draw(card);
        playerHand.AddCard(card);
<<<<<<< HEAD

        checkBust(playerHand);
    }

    public void checkBust(Hand h)
    {
        if (h.total > 21)
        {
            h.bust = true;
            EndRound();
        }
=======
>>>>>>> gamestate
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
<<<<<<< HEAD
}
=======

    public void PlayVampire()
    {
        if (bustPlayer || vampHand.total > playerHand.total)
        {
            EndRound();
        }

        while (vampHand.total < 17)
        {
            VampireHit();

            if (bustVamp || vampHand.cards.Count == 5) 
            {
                EndRound();
            }
        }

        EndRound();
    }

    private void MoveCursor(InputAction.CallbackContext context)
    {
        float cursor = context.ReadValue<float>();

        if (cursor < 0 && selectedOption > 0)
        {
            selectedOption -= 1;
        }   else if (cursor > 0 && selectedOption < 2)
        {
            selectedOption += 1;
        }
    }

    private void SelectAction()
    {
        switch (selectedOption)
        {
            case 1: 
                PlayerRaise();
                break;
            case 2: 
                PlayerHit();
                break;
            case 3:
                PlayerStand();
                break;
        }
    }
}

public abstract class Hand
{
    public List<Card> cards;
    public CardDisplay[] cardDisplays;
    public int total;
    private int aces;
    public int size;

    protected Hand()
    {
        cards = new();
    }

    public virtual void AddCard(Card card)
    {
        cards.Add(card);
        if (card.rank == Rank.ACE)
        {
            aces++;
            total += 11;
        }   else
        {
            int add = (int)card.rank + 1;
            // make sure face cards are all 10
            if (add > 10)
            {
                add = 10;
            }
            total += add;
        }

        // If we have an ace and bust out, we treat the ace as a 1 now to continue
        if (total > 21 && aces > 0)
        {
            total -= 10;
            aces--;
        }
        size++;
    }

    public virtual void ClearHand()
    {
        cards.Clear();
    }
}

public class PlayerHand : Hand
{
    // PlayerHand() 
    // {
    //     base();
    // }


}

public class VampHand : Hand
{
    
}

>>>>>>> gamestate
