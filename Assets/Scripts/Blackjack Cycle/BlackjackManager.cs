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
    string roundResult;
    int streak;

    DialogueManager dialogue;
    GameManager game;
    [SerializeField]private Animator vampSprite;

    //input tracking
    InputAction move;
    bool listenMove;
    InputAction enter;
    bool listenEnter;
    int selectedOption;
    

    private readonly WaitForSeconds pause = new(1.5f);
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

        // Both players draw two cards
        PlayerHit();
        yield return pause;
        VampireHit();
        yield return pause;
        PlayerHit();
        yield return pause;
        VampireHit();
        yield return pause;

        // run dialogue
        yield return StartCoroutine(dialogue.RoundStart(game.vampConfidence));

        // free player action
        selectedOption = 1;
        listenEnter = true;
        listenMove = true;
        PlayerActions.Instance.ActivateActions();
        if (bet == 3)
        {
            PlayerActions.Instance.DeactivateRaise();
        }   else if (game.vampConfidence > 80)
        {
            //all in baby
            PlayerActions.Instance.DeactivateRaise();
            while (bet < 5)
            {
                bet += 1;
                GlassDisplay.Instance.Raise();
            }
        }

        // END PLAYER PHASE
        yield return new WaitUntil(() => !playerTurn);
        PlayerActions.Instance.DeactivateActions();

        // vampire turn
        StartCoroutine(PlayVampire());
    }

    public IEnumerator EndRound()
    {
        listenEnter = false;
        listenMove = false;

        Debug.Log("PlayerHand: " + playerHand.total + " Vampire Hand: " + vampHand.total);
        roundResult = DetermineWinner();
        CalculateConfidence();
        Debug.Log(roundResult);

        //let everything finish first
        yield return pause;
        vampHand.ClearHand();
        playerHand.ClearHand();

        //fill shot glasses
        if (roundResult == "win")
        {
            vampSprite.SetTrigger("lose");
            GlassDisplay.Instance.Fill(true);
            yield return StartCoroutine(dialogue.WinPrompt(x =>
            {
                if (x == 1)
                {
                    game.HealPlayerHP(bet);
                    
                }   else if (x == 2)
                {
                    game.LoseVampHP(bet);
                }
            }));
            yield return StartCoroutine(dialogue.RoundEnd(game.vampConfidence, roundResult, false));
            GlassDisplay.Instance.Clear();
            bet = 0;
        }   else if (roundResult == "tie")
        {
            yield return StartCoroutine(dialogue.RoundEnd(game.vampConfidence, roundResult, false));
        }   else if (roundResult == "lose")
        {
            vampSprite.SetTrigger("win");
            GlassDisplay.Instance.Fill(false);
            yield return StartCoroutine(dialogue.RoundEnd(game.vampConfidence, roundResult, false));
            GlassDisplay.Instance.Clear();
            game.LosePlayerHP(bet);
            bet = 0;
        }

        StartCoroutine(StartRound());
    }

    public void PlayerHit()
    {
        Card card = deck.Draw();
        playerHand.AddCard(card);

        CheckBust(playerHand);
    }

    public void CheckBust(Hand h)
    {
        if (h.bust)
        {
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
        if (bet == MAX_BET)
        {
            selectedOption = 1;
            PlayerActions.Instance.DeactivateRaise();
        }
    }

    public void VampireHit()
    {
        Card card = deck.Draw();
        vampHand.AddCard(card);
    }

    public IEnumerator PlayVampire()
    {
        vampHand.StartTurn();
        yield return new WaitForSeconds(2);

        if (playerHand.bust || vampHand.total > playerHand.total)
        {
            StartCoroutine(EndRound());
            yield break;
        }

        while (vampHand.total < 17 || (vampHand.total < playerHand.total && !playerHand.bust))
        {
            VampireHit();

            yield return pause;

            if (vampHand.cards.Count == 5 || vampHand.bust)
            {
                break;
            }
        }

        StartCoroutine(EndRound());
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

    private string DetermineWinner()
    {
        //first check for bust
        if (playerHand.bust && vampHand.bust)
        {
            return "tie";
        }   else if (playerHand.bust)
        {
            return "lose";
        }   else if (vampHand.bust)
        {
            return "win";
        }

        //compare hands
        if (playerHand.total > vampHand.total)
        {
            return "win";
        }   else if (playerHand.total == vampHand.total)
        {
            return "tie";
        }   else if (playerHand.total < vampHand.total)
        {
            return "lose";
        }

        // this shouldnt happen
        return "tie";
    }

    private void CalculateConfidence () {
        int adjustment = 0;
        if (roundResult == "lose")
        {
            if (streak < 0)
            {
                streak--;
            }   else
            {
                streak = -1;
            }
            adjustment += 10;
            if (vampHand.total - playerHand.total <= 3)
            {
                //close victory
                adjustment += 5;
            }

            if (streak < -2)
            {
                //winstreak
                adjustment += 5;
            }

            adjustment += 5 * bet;
        }   else if (roundResult == "tie")
        {
            streak = 0;
            adjustment = 5;
        }   else if (roundResult == "win")
        {
            if (streak > 0)
            {
                streak++;
            }   else
            {
                streak = 1;
            }
            adjustment -= 5;
            if (streak > 2)
            {
                adjustment -= 5;
            }

            if (bet == 5)
            {
                //crash and burn
                adjustment = -50;
            }
        }

        game.VampAdjustConfidence(adjustment);
        vampSprite.SetInteger("confidence", game.vampConfidence);
    }
}
