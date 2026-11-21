using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameManager : Singleton<GameManager>
{
    public int playerHP {get; private set; }
    public int vampHP {get; private set; }
    public int vampConfidence {get; private set; }
    DialogueManager dialogue;
    BlackjackManager blackjack;

    // For tracking when either HP drops below certain thresholds to trigger events
    public int[] healthThresholds = {0, 8, 13, 19};
    public int playerHPStatus;
    public int vampHPStatus;

    [SerializeField] private Image playerHPBar;
    [SerializeField] private Image vampHPBar;
    [SerializeField] private Animator lowHealthIndicator;

    void Start()
    {
        dialogue = DialogueManager.Instance;
        blackjack = BlackjackManager.Instance;
        GameStart();
        Debug.Log(healthThresholds[0]);
    }

    public void GameStart()
    {
        playerHP = 25;
        vampHP = 25;
        vampConfidence = 60;
        playerHPStatus = 3;
        vampHPStatus = 3;

        StartCoroutine(blackjack.StartRound());
    }

    public void LosePlayerHP(int playerLoss)
    {
        playerHP -= playerLoss;

        if (playerHP <= 0)
        {
            GameOver();
        }   else if (playerHP < healthThresholds[playerHPStatus])
        {
            StartCoroutine(dialogue.HealthThresholdCrossed(playerHPStatus, -1));
            lowHealthIndicator.SetTrigger("flash");
            playerHPStatus--;
        }
        
        lowHealthIndicator.SetInteger("health", playerHP);

        playerHPBar.fillAmount = (float)playerHP / 25;
    }

    public void HealPlayerHP(int gain)
    {
        playerHP += gain;

        if (playerHP > 25)
        {
            playerHP = 25;
        }

        lowHealthIndicator.SetInteger("health", playerHP);

        playerHPBar.fillAmount = (float)playerHP / 25;
    }

    public void LoseVampHP(int vampLoss) 
    {
        Debug.Log("Losing " + vampLoss + " hp");
        vampHP -= vampLoss;

        if (vampHP < healthThresholds[vampHPStatus])
        {
            StartCoroutine(dialogue.HealthThresholdCrossed(-1, vampHPStatus));
            switch (vampHPStatus)
            {
                case 3:
                    List<Card> kings = new();
                    for (int i = 1; i <= 4; i++)
                    {
                        kings.Add(new Card((i * 13) - 1));
                    }
                    blackjack.deck.RemoveFromDeck(kings);
                    break;
                case 2:
                    // NO EVENT YET, MIGHT DO ONE LATER
                    break;
                case 1:
                    break;
            }
            vampHPStatus--;
        }

        if (vampHP <= 0)
            VampireDeath();

        vampHPBar.fillAmount = (float)vampHP / 25;
    }

    public void VampAdjustConfidence(int change)
    {
        vampConfidence += change;

        if (vampConfidence <= 0)
        {
            GameOver();
        }   else if (vampConfidence > 100)
        {
            vampConfidence = 100;
        }
    }

    public void GameOver()
    {
        lowHealthIndicator.SetTrigger("death");
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

    public void VampireDeath()
    {
        //placeholder
        lowHealthIndicator.SetTrigger("win");
    }
}