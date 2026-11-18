using UnityEngine;

public class PlayerHand : Hand
{
    public CardDisplay[] cardDisplays;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void AddCard(Card card)
    {
        cardDisplays[size].Draw(card);
        base.AddCard(card);
    }

    public override void ClearHand()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cardDisplays[i].Clear();
        }
        base.ClearHand();
    }
}
