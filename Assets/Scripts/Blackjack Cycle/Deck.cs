using UnityEngine;

public class Deck : Singleton<Deck>
{
    public static Deck Instance { get; private set; }

    enum ranks {
        one, two, three,
        four, five, six,
        seven, eight, nine,
        jack, queen, king, ace
    }

    enum suits
    {
        spades, clubs, hearts, diamonds
    }

    struct deck
    {
        int rank;
        int suit;
    }

    const int ROWS = 4;
    const int COLS = 13;

    void shuffle (namespace 
    {
    // insert animation

    for (int j = COLS - 1; j > 0; j--)
    {
        for (int i = ROWS - 1; i > 0; i--)
        {
            int w = Random.Shared.Next(0, i);
            int z = Random.Shared.Next(0, j);

            temp = deck[i][j];
            deck[i][j] = deck[w][z];
            deck[w][z] = temp;
        }
    }
}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        deck[,] deckCards = new deck[ROWS, COLS];

        for (int j = 0; j < COLS; j++)
        {
            for (int i = 0; i < ROWS; i++)
            {
                DeckCards[j][i].deck.rank = i + 1;
                deckCards[j][i].deck.suit = i + 1;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
