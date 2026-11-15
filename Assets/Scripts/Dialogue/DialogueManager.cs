using System;
using System.Collections;
using System.Threading.Tasks;
using Packages.Rider.Editor.UnitTesting;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : Singleton<DialogueManager>
{
    public PlayerDialogueBox player;
    public VampireDialogueBox vamp;
    public int complimentsRemaining;
    private InputAction proceed;
    private WaitUntil waitForPlayer;

    protected override void Awake()
    {
        base.Awake();
        complimentsRemaining = 3;
        proceed = InputSystem.actions.FindAction("Any");
        waitForPlayer = new WaitUntil(proceed.WasPerformedThisFrame);
    }

    void Start()
    {
        
    }

    public IEnumerator RoundStart(int confidence)
    {
        // CHECK CONFIDENCE LEVELS, PLAY DIALOGUE BASED ON SCENARIO

        // CASE 1: VERY LOW CONFIDENCE
        // Offer the player a chance at a compliment as a way to boost his ego
        if (confidence <= 20)
        {
            yield return StartCoroutine(player.Prompt("compliment", x =>
            {
                if (x == 1)
                {
                    if (complimentsRemaining > 0)
                    {
                        complimentsRemaining--;
                        //TODO: add to his confidence
                        StartCoroutine(vamp.PlayDialogueFromEvent("recieveCompliment"));
                    }   else
                    {
                        StartCoroutine(vamp.PlayDialogueFromEvent("rejectCompliment"));
                    }
                }
            }));

            vamp.nextIndicator.SetActive(true);
            yield return waitForPlayer;
            vamp.nextIndicator.SetActive(false);
        }  
        yield return StartCoroutine(player.PlayDialogueFromEvent("startRound" + ConfidenceString(confidence)));
        StartCoroutine(vamp.PlayDialogueFromEvent("startRound" + ConfidenceString(confidence)));
    }

    public IEnumerator RoundEnd(int confidence, bool win, bool cheated)
    {
        if (win)
        {
            if (cheated)
            {
                yield return StartCoroutine(player.PlayDialogueFromEvent("winRoundCheated"));
                StartCoroutine(vamp.PlayDialogueFromEvent("loseRoundCheated"));
            }   else
            {
                yield return StartCoroutine(player.PlayDialogueFromEvent("winRound" + ConfidenceString(confidence)));
                StartCoroutine(vamp.PlayDialogueFromEvent("loseRound" + ConfidenceString(confidence)));
            }
        }   else
        {
            if (cheated)
            {
                yield return StartCoroutine(player.PlayDialogueFromEvent("loseRoundCheated"));
                StartCoroutine(vamp.PlayDialogueFromEvent("winRoundCheated"));
            }   else
            {
                yield return StartCoroutine(player.PlayDialogueFromEvent("loseRound" + ConfidenceString(confidence)));
                StartCoroutine(vamp.PlayDialogueFromEvent("winRound" + ConfidenceString(confidence)));
            }
        }
    }

    public IEnumerator HealthThresholdCrossed(int playerhp, int vamphp)
    {
        switch (vamphp)
        {
            case 3: 
                yield return StartCoroutine(vamp.PlaySequenceFromEvent("healthThreeFourths"));
                break;
            case 2: 
                yield return StartCoroutine(vamp.PlaySequenceFromEvent("healthOneHalf"));
                break;
            case 1: 
                yield return StartCoroutine(vamp.PlaySequenceFromEvent("healthOneFourth"));
                break;
        }

        switch (playerhp)
        {
            case 3: 
                yield return StartCoroutine(player.PlayDialogueFromEvent("healthThreeFourths"));
                break;
            case 2: 
                yield return StartCoroutine(player.PlayDialogueFromEvent("healthOneHalf"));
                break;
            case 1: 
                yield return StartCoroutine(player.PlayDialogueFromEvent("healthOneFourth"));
                break;
        }
    }


    /// <summary>
    /// nice little helper so we dont need 3 branches for each confidence state every time
    /// </summary>
    /// <returns>the modifier string to add to the main event</returns>
    /// <exception cref="ArgumentOutOfRangeException"> confidence is somehow over 100</exception>
    private string ConfidenceString(int confidence)
    {
        if (confidence <= 40) 
        {
            return "Unconfident";
        }   else if (confidence > 40 && confidence <= 70) 
        {
            return "Neutral";
        }   else if (confidence > 70 && confidence <= 100) 
        {
            return "Confident";
        }

        throw new ArgumentOutOfRangeException("confidence is wrong");
    }

    public void tester()
    {
        StartCoroutine(HealthThresholdCrossed(-1, 3));
    }
}