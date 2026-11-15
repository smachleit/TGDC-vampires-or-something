using UnityEngine;

public class Hand : MonoBehaviour
{

    // Bet number of shots from 1-5, max # number of bets = MAX_BET
    void bet() { }

    // Repeatable until bust
    void hit() 
    {
        hand.Add(deck[i][j]);
        // deck.RemoveAt(i, j);
    }

    // Check for win
    void call() { }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //myDeck = getComponent<Deck>();
        //List<card> hand = new List<card>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
