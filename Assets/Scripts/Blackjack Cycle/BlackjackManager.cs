using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlackjackManager : Singleton<BlackjackManager>
{
    public PlayerHand playerHand;
    public VampHand vampHand;
    Card vampStashedCard;
    int bet;
    const int MAX_BET = 3;
    public Deck deck;
    
    bool playerTurn;
    bool playerBlackjack;
    bool vampBlackjack;
    bool playerWinR;

    DialogueManager dialogue;
    GameManager game;

    //input tracking
    InputAction move;
    bool listenMove;
    InputAction enter;
    bool listenEnter;
    int selectedOption;

    override protected void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        dialogue = DialogueManager.Instance;
        game = GameManager.Instance;
        move = InputSystem.actions.FindAction("NavigateVertical");
        move.performed += context => MoveCursor(context);
        enter = InputSystem.actions.FindAction("Enter");
        enter.performed += context => SelectAction();

        listenEnter = false;
        listenMove = false;
    }

    public void test()
    {
        StartCoroutine(StartRound());
    }

    public IEnumerator StartRound()
    {
        playerTurn = true;
        selectedOption = -1;
        deck.Shuffle();
        playerHand.bust = false;
        vampHand.bust = false;
        playerWinR = false;
        bet = 0;
        WaitForSeconds pause = new(2.5f);

        // Both players draw two cards
        PlayerHit();
        yield return pause;
        VampireHit();
        yield return pause;
        PlayerHit();
        yield return pause;
        VampireHit();

        yield return StartCoroutine(dialogue.RoundStart(game.vampConfidence));

        selectedOption = 1;
        listenEnter = true;
        listenMove = true;
        PlayerActions.Instance.ActivateActions();

        // END PLAYER PHASE
        yield return new WaitUntil(() => !playerTurn);
        move.Disable();
        enter.Enable();
        PlayerActions.Instance.DeactivateActions();

        StartCoroutine(PlayVampire());
    }

    public void EndRound()
    {
        if (!playerHand.bust && !vampHand.bust)
        {
            if (playerHand.total > vampHand.total)
                playerWinR = true;
            else
                playerWinR = false;
        }
        else if (playerHand.bust)
            playerWinR = false;
        else
            playerWinR = true;

        if (playerWinR)
            game.LosePlayerHP(bet);
        else
            game.LoseVampHP(bet);
    }

    public void PlayerHit()
    {
        Card card = deck.Draw();
        playerHand.AddCard(card);

        CheckBust(playerHand);
    }

    public void CheckBust(Hand h)
    {
        if (h.total > 21)
        {
            h.bust = true;
            playerTurn = false;
        }   else if (h.total == 21)
        {
            playerTurn = false;
        }   else if (h.cards.Count == 5)
        {
            playerTurn = false;
        }
    }

    public void PlayerStand()
    {
        playerTurn = false;
    }

    public void PlayerRaise()
    {
        bet += 1;
        GlassDisplay.Instance.Raise();

        // Temporary switch for blurring out raise button
        if (bet == 3)
        {
            selectedOption = 1;
            PlayerActions.Instance.DeactivateRaise();
        }
    }

    public void VampireHit()
    {
        Card card = deck.Draw();
        vampHand.AddCard(card);

        // CheckBust(vampHand);
    }

    public IEnumerator PlayVampire()
    {
        vampHand.StartTurn();
        yield return new WaitForSeconds(2);

        if (playerHand.bust || vampHand.total > playerHand.total)
        {
            EndRound();
        }

        while (vampHand.total < 17 || (vampHand.total < playerHand.total && !playerHand.bust))
        {
            VampireHit();

            yield return new WaitForSeconds(1);

            if (playerHand.bust || vampHand.cards.Count == 5) 
            {
                EndRound();
            }
        }

        EndRound();
    }

    private void MoveCursor(InputAction.CallbackContext context)
    {
        if (!listenMove)
        {
            return;
        }
        float cursor = context.ReadValue<float>();
        int disableRaise = (bet == MAX_BET) ? 1 : 0;

        if (cursor > 0 && selectedOption > disableRaise)
        {
            selectedOption -= 1;
        }   else if (cursor < 0 && selectedOption < 2)
        {
            selectedOption += 1;
        }

        PlayerActions.Instance.UpdateCurrentAction(selectedOption);

        // Debug.Log("Cursor moved: " + selectedOption);
    }

    private void SelectAction()
    {
        if (!listenEnter)
        {
            return;
        }

        switch (selectedOption)
        {
            case 0: 
                PlayerRaise();
                break;
            case 1: 
                PlayerHit();
                break;
            case 2:
                PlayerStand();
                break;
            
        }
    }
}
