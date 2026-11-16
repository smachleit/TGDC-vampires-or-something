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
    
    public CardDisplay[] cardDisplays;
    bool playerTurn;
    bool playerBlackjack;
    bool vampBlackjack;
    bool playerWinR;

    DialogueManager dialogue;
    GameManager game;

    //input tracking
    InputAction move;
    InputAction enter;
    int selectedOption;

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
        playerHand.bust = false;
        vampHand.bust = false;
        playerWinR = false;
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
        if (!playerHand.bust || !vampHand.bust)
        {
            if (playerHand.total > vampHand.total)
                playerWinR = true;
            else
                playerWinR = false;
        }
        else if (playerHand.bust)
        {
            playerWinR = false;
        }
        else
        {
            playerWinR = true;
        }
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

    public void PlayVampire()
    {
        if (playerHand.bust || vampHand.total > playerHand.total)
        {
            EndRound();
        }

        while (vampHand.total < 17)
        {
            VampireHit();

            if (playerHand.bust || vampHand.cards.Count == 5) 
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
