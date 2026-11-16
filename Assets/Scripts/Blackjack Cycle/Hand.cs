using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class Hand : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Card> cards;
    public CardDisplay[] cardDisplays;
    public int total;
    private int aces;
    public int size;

    public bool bust;
    // public bool winR;

    protected Hand()
    {
        cards = new();
        bust = false;
        // winR = false;
    }

    public virtual void AddCard(Card card)
    {
        cards.Add(card);
        if (card.rank == Rank.ACE)
        {
            aces++;
            total += 11;
        }
        else
        {
            int add = (int)card.rank + 1;
            // make sure face cards are all 10
            if (add > 10)
            {
                add = 10;
            }
            total += add;
        }

        // If we have an ace and bust out, we treat the ace as a 1 now to continue
        if (total > 21 && aces > 0)
        {
            total -= 10;
            aces--;
        }
        size++;
    }

    public virtual void ClearHand()
    {
        cards.Clear();
    }

    public int getHandTotal()
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
