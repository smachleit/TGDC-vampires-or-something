using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Dialogue
{
    public Dictionary<string, string[]> paths;
    private System.Random random = new();

    public void LoadDialogue(TextAsset source)
    {
        paths = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(source.text);
        Debug.Log(paths);
        Debug.Log("Loaded " + paths.Count + " events with " + paths.Values.Count + " lines.");
    }

    /// <summary>
    /// General dialogue generator that will be called every time we need to play a new line after some event (like a round start, or a loss, or when hp gets low, etc.)
    /// </summary>
    /// <param name="eventName"> String representing the event we want to find the diague for</param>
    /// <returns>A random line from the set of dialogue options</returns>
    /// <exception cref="KeyNotFoundException">Invalid event</exception>
    public string GetDialogueFromEvent(string eventName)
    {
        if (!paths.ContainsKey(eventName))
        {
            throw new KeyNotFoundException("Invalid event: " +eventName);
        }

        string[] allDialogue = paths[eventName];
        Debug.Log(allDialogue);

        int index = random.Next(0, allDialogue.Length);

        return allDialogue[index];
    }
}