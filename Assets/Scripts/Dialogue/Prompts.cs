using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class Prompts
{
    public Dictionary<string, Prompt> paths;
    private System.Random random = new();

    public void LoadDialogue(TextAsset source)
    {
        paths = JsonConvert.DeserializeObject<Dictionary<string, Prompt>>(source.text);
        Debug.Log(paths);
        Debug.Log("Loaded " + paths.Count + " events with " + paths.Values.Count + " lines.");
    }

    /// <summary>
    /// General dialogue generator that will be called every time we need to play a new line after some event (like a round start, or a loss, or when hp gets low, etc.)
    /// </summary>
    /// <param name="eventName"> String representing the event we want to find the diague for</param>
    /// <returns>A random line from the set of dialogue options</returns>
    /// <exception cref="KeyNotFoundException">Invalid event</exception>
    public string GetDialogueFromPrompt(string eventName, string status)
    {
        if (!paths.ContainsKey(eventName))
        {
            throw new KeyNotFoundException("Invalid event");
        }


        string[] allDialogue;
        if (status == "prompt")
        {
            allDialogue = paths[eventName].prompt;
        }   else if (status == "reply1")
        {
            allDialogue = paths[eventName].reply1;
        }   else if (status == "reply2")
        {
            allDialogue = paths[eventName].reply2;
        }   else
        {
            throw new ArgumentException("Invalid status");
        }

        int index = random.Next(0, allDialogue.Length);

        return allDialogue[index];
    }

    public string GetOption1(string eventName)
    {
        if (!paths.ContainsKey(eventName))
        {
            throw new KeyNotFoundException("Invalid event");
        }
        return paths[eventName].options[0];
    }

    public string GetOption2(string eventName)
    {
        if (!paths.ContainsKey(eventName))
        {
            throw new KeyNotFoundException("Invalid event");
        }
        return paths[eventName].options[1];
    }
}

public class Prompt
{
    public string[] prompt;
    public string[] options;
    public string[] reply1;
    public string[] reply2;
}