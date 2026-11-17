using System.Collections;
using UnityEngine;

public class VampHand : Hand
{
    public VampCardDisplay[] cardDisplays;
    public Animator animator;

    void Start()
    {
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
        animator.SetTrigger("turnEnd");
        base.ClearHand();
    }

    private IEnumerator MoveSequence()
    {
        animator.SetTrigger("turnStart");

        yield return new WaitForSeconds(1);

        cardDisplays[1].RevealHoleCard();
    }
}
