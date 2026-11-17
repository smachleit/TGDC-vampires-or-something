using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class VampireDialogueBox : DialogueBox
{
    private InputAction proceed;
    private WaitUntil waitForPlayer;
    public GameObject nextIndicator;

    protected override void Awake()
    {
        base.Awake();
        proceed = InputSystem.actions.FindAction("Any");
        waitForPlayer = new WaitUntil(proceed.WasPerformedThisFrame);
        nextIndicator.SetActive(false);
    }

    public override IEnumerator PlayDialogueFromEvent(string eventName)
    {
        string newMessage = dialogue.GetDialogueFromEvent(eventName);

        if (newMessage == null || newMessage == "")
        {
            UpdateText(". . .");
        }   else
        {
            UpdateText(newMessage);
        }

        yield return new WaitUntil(() => _textFullyLoaded);

        // wait a bit for player to read
        yield return new WaitForSeconds(0.5f);
    }

    // For when we want to print multiple lines in succession
    public IEnumerator PlaySequenceFromEvent(string eventName)
    {
        string[] sequence = dialogue.paths[eventName];

        foreach (string message in sequence)
        {
            UpdateText(message);

            yield return new WaitUntil(() => _textFullyLoaded);

            nextIndicator.SetActive(true);
            yield return waitForPlayer;
            nextIndicator.SetActive(false);
        }        
    }
}