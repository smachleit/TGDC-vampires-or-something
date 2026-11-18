using System.Collections;
using UnityEngine;

public class VampHand : Hand
{
    public VampCardDisplay[] cardDisplays;
    public Animator animator;
    public WaitForSeconds waitSecond;

    void Start()
    {
        waitSecond = new(1);
        for (int i = 0; i < 5; i++)
        {
            cardDisplays[i].SetIndex(i);
        }
    }

    public override void AddCard(Card card)
    {
        cardDisplays[size].Draw(card);
        base.AddCard(card);
    }

    public void StartTurn()
    {
        StartCoroutine(MoveSequence());   
    }

    public override void ClearHand()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cardDisplays[i].Clear();
        }

        StartCoroutine(resetDelay());
        base.ClearHand();
    }

    private IEnumerator MoveSequence()
    {
        animator.SetTrigger("turnStart");

        yield return waitSecond;

        cardDisplays[1].RevealHoleCard();
    }

    private IEnumerator resetDelay()
    {
        yield return waitSecond;
        animator.SetTrigger("turnEnd");
    }
}
