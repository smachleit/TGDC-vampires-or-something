using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : Singleton<DialogueManager>
{
    public PlayerDialogueBox player;
    public VampireDialogueBox vamp;
    public int complimentsRemaining;
    private InputAction proceed;
    private WaitUntil waitForPlayer;
    [SerializeField] Animator vampSprite;

    protected override void Awake()
    {
        base.Awake();
        complimentsRemaining = 3;
        proceed = InputSystem.actions.FindAction("Any");
        waitForPlayer = new WaitUntil(proceed.WasPerformedThisFrame);
    }

    public IEnumerator WinPrompt(Action<int> response)
    {
        int reply = 0;
        yield return StartCoroutine(player.Prompt("win", x =>
        {
            reply = x;
        }));

        if (reply == 1)
        {
            vampSprite.SetTrigger("heal");
            StartCoroutine(vamp.PlayDialogueFromEvent("heal"));
        }   else if (reply == 2)
        {
            vampSprite.SetTrigger("hurt");
            StartCoroutine(vamp.PlayDialogueFromEvent("hurt"));
        }

        vamp.nextIndicator.SetActive(true);
        yield return waitForPlayer;
        vamp.nextIndicator.SetActive(false);

        response(reply);
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
                        GameManager.Instance.VampAdjustConfidence(20);
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

    public IEnumerator RoundEnd(int confidence, string result, bool cheated)
    {
        string eventName = result + "Round";
        if (cheated)
        {
            eventName += "Cheated";
        }   else
        {
            eventName += ConfidenceString(confidence);
        }

        yield return StartCoroutine(player.PlayDialogueFromEvent(eventName));
        StartCoroutine(vamp.PlayDialogueFromEvent(eventName));
        vamp.nextIndicator.SetActive(true);
        yield return waitForPlayer;
        vamp.nextIndicator.SetActive(false);
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