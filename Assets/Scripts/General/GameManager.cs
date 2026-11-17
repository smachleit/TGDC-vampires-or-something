using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int playerHP {get; private set; }
    public int vampHP {get; private set; }
    public int vampConfidence {get; private set; }
    DialogueManager dialogue;
    BlackjackManager blackjack;

    // For tracking when either HP drops below certain thresholds to trigger events
    public int[] healthThresholds = {8, 13, 19};
    public int playerHPStatus;
    public int vampHPStatus;

    [SerializeField] private Image playerHPBar;
    [SerializeField] private Image vampHPBar;

    void Start()
    {
        dialogue = DialogueManager.Instance;
        blackjack = BlackjackManager.Instance;
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

        if (playerHP < healthThresholds[playerHPStatus - 1])
        {
            StartCoroutine(dialogue.HealthThresholdCrossed(playerHPStatus, -1));
            playerHPStatus--;
        }
        
        if (playerHP <= 0)
            StartCoroutine(GameOver("PlayerDeath"));
        
        playerHPBar.fillAmount = (float)playerHP / 25;
    }

    public void HealPlayerHP(int gain)
    {
        playerHP += gain;

        if (playerHP > 25)
        {
            playerHP = 25;
        }

        playerHPBar.fillAmount = (float)playerHP / 25;
    }

    public void LoseVampHP(int vampLoss) 
    {
        Debug.Log("Losing " + vampLoss + " hp");
        vampHP -= vampLoss;

        if (vampHP < healthThresholds[vampHPStatus - 1])
        {
            StartCoroutine(dialogue.HealthThresholdCrossed(-1, vampHPStatus));
            vampHPStatus--;
        }

        if (vampHP <= 0)
            StartCoroutine(GameOver("VampireDeath"));

        Debug.Log(vampHP);
        Debug.Log((float)vampHP / 25);
        vampHPBar.fillAmount = (float)vampHP / 25;
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