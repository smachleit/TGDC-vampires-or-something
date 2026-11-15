using UnityEngine;

public class Blackjack_Manager : MonoBehaviour
{
    // private Deck myDeck;

    const int WIN_NUM = 21;
    const int MAX_BET = 5;

    // check if over WIN_NUM
    bool bust() { }

    // if loss, fill blood shot
    void bloodShot() { }

    // if win, fill holy shot
    void holyShot() { }

    void deal()
    {
        //int w = Random.Shared.Next(0, ROWS - 1);
        //int z = Random.Shared.Next(0, COLS - 1);

        //int i = Random.Shared.Next(0, ROWS - 1);
        //int j = Random.Shared.Next(0, COLS - 1);

        // myDeck.add(w, z);
        // myDeck.add(i, j);

        // myDeck.remove(z);
        // myDeck.remove(j);

        // deck.trim();
    }

    void startRound() { }

    void endRound() { }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //myDeck = getComponent<Deck>();
        //myPlayHand = getComponent<Hand>();
        //myVampHand = getComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
