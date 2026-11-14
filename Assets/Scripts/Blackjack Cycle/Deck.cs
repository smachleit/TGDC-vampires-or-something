using UnityEngine;

public class Deck : Singleton<Deck>
{
    public static Deck Instance { get; private set; }

    enum ranks {
        ONE, TWO, THREE,
        FOUR, FIVE, SIX,
        SEVEN, EIGHT, NINE,
        JACK, QUEEN, KING, ACE;
    }

    enum suits
    {
        SPADES, CLUBS, HEARTS, DIAMONDS;
    }

    struct card
    {
        int rank;
        int suit;
    }

    const int ROWS = 4;
    const int COLS = 13;

    void shuffle ()
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

        card[,] deck = new card[ROWS, COLS];

        for (int j = 0; j < COLS; j++)
        {
            for (int i = 0; i < ROWS; i++)
            {
                if (i == ONE)
                    deck[j][i].card.rank = ONE;
                else if (i == TWO)
                    deck[j][i].card.rank = TWO;
                else if (i == THREE)
                    deck[j][i].card.rank = THREE;
                else if (i == FOUR)
                    deck[j][i].card.rank = FOUR;
                else if (i == FIVE)
                    deck[j][i].card.rank = FIVE;
                else if (i == SIX)
                    deck[j][i].card.rank = SIX;
                else if (i == SEVEN)
                    deck[j][i].card.rank = SEVEN;
                else if (i == EIGHT)
                    deck[j][i].card.rank = EIGHT;
                else if (i == NINE)
                    deck[j][i].card.rank = NINE;
                else if (i == JACK)
                    deck[j][i].card.rank = JACK;
                else if (i == QUEEN)
                    deck[j][i].card.rank = QUEEN;
                else if (i == KING)
                    deck[j][i].card.rank = KING;
                else if (i == ACE)
                    deck[j][i].card.rank = ACE;


                if (j == SPADES)
                    deckCards[j][i].deck.suit = SPADES;
                if (j == SLUBS)
                    deckCards[j][i].deck.suit = CLUBS;
                if (j == SPADES)
                    deckCards[j][i].deck.suit = HEARTS;
                if (j == DIAMONDS)
                    deckCards[j][i].deck.suit = DIAMONDS;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
