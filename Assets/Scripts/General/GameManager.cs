using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int playerHP {get; private set; }
    public int vampHP {get; private set; }
    public int vampConfidence {get; private set; }
    DialogueManager dialogue;

    // For tracking when either HP drops below certain thresholds to trigger events
    public int[] healthThresholds = {8, 13, 19};
    public int playerHPStatus;
    public int vampHPStatus;

    void Start()
    {
        dialogue = DialogueManager.Instance;
    }

    public void GameStart()
    {
        playerHP = 25;
        vampHP = 25;
        vampConfidence = 70;
        playerHPStatus = 3;
        vampHPStatus = 3;
    }

    public void LoseHP(int playerLoss, int vampLoss)
    {
        playerHP -= playerLoss;

        if (playerHP < healthThresholds[playerHPStatus - 1])
        {
            StartCoroutine(dialogue.HealthThresholdCrossed(playerHPStatus, -1));
            playerHPStatus--;
        }
        
        if (playerHP <= 0)
            StartCoroutine(GameOver("PlayerDeath");
    }

    public void LosePlayerHP(int vampLoss) 
    {
        vampHP -= vampLoss;

        if (vampHP < healthThresholds[vampHPStatus - 1])
        {
            StartCoroutine(dialogue.HealthThresholdCrossed(-1, vampHPStatus));
            vampHPStatus--;
        }

        if (vampHP <= 0)
            StartCoroutine(GameOver("VampireDeath"));
    }

    public void VampAdjustConfidence(int change)
    {
        vampConfidence += change;

        if (vampConfidence <= 0)
        {
            StartCoroutine(GameOver("Ragequit"));
        }   else if (vampConfidence > 100)
        {
            vampConfidence = 100;
        }
    }

    public IEnumerator GameOver(string ending)
    {
        //placeholder
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator PlayerDeath()
    {
        //placeholder
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator Ragequit()
    {
        //placeholder
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator VampireDeath()
    {
        //placeholder
        yield return new WaitForEndOfFrame();
    }
}