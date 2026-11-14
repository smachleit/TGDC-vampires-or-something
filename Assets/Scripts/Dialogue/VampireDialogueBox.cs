using UnityEngine;
using System.Collections;

public class VampireDialogueBox : DialogueBox
{
    public override IEnumerator PlayDialogueFromEvent(string eventName)
    {
        string newMessage = dialogue.GetDialogueFromEvent(eventName);

        if (newMessage == null)
        {
            UpdateText(". . .");
        }   else
        {
            UpdateText(newMessage);
        }

        yield return new WaitUntil(() => _textFullyLoaded);
    }
}