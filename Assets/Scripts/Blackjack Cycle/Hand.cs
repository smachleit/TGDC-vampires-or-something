using UnityEngine;

public abstract class Hand : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Card> cards;
    public GameObject[] cardDisplay;
    int total;
    bool bust;

    public void Hand();

    public virtual void AddCard(Card card)
    {
        cards.Add(card);
        total += (int)card.rank + 1;
    }

    public virtual void ClearHand()
    {
        cards.Clear();
    }

    public virtual void getHandTotal()
    {
        return total;
    }

    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {       
    }
}
