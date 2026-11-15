using UnityEngine;

public class Deck : Singleton<Deck>
{
    enum Ranks
    {
        TWO, THREE,
        FOUR, FIVE, SIX,
        SEVEN, EIGHT, NINE, TEN,
        JACK, QUEEN, KING, ACE
    }

    enum Suits
    {
        SPADES, CLUBS, HEARTS, DIAMONDS
    }

    public class Card
    {
        int rank = 0;
        int suit = 0;
    }

    const int ROWS = 4;
    const int COLS = 13;

    int s_size = COLS;
    int c_size = COLS;
    int h_size = COLS;
    int d_size = COLS;

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

    void remove(int j) 
    {
        if (j == (int)Suits.SPADES)
            s_size -= 1;
        else if (z == (int)Suits.CLUBS)
            c_size -= 1;
        else if (z == (int)Suits.HEARTS)
            h_size -= 1;
        else if (z == (int)Suits.DIAMONDS)
            d_size -= 1;
    }

    void add(Card card21)
    {
        deck.Add(card);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<List<Card>> deck = new List<List<Card>>(ROWS);

        for (int i = 0; i < ROWS; i++)
        {
            deck.Add(new List<Card>(new List<Card>[COLS]));
        }

        int t = 0;

        for (int j = 0; j < COLS; j++)
        {
            for (t; t < ROWS; t++)
            {
                if (t == (int)Ranks.TWO)
                    deck[j][t].rank = Ranks.TWO;
                else if (t == (int)Ranks.THREE)
                    deck[j][t].rank = Ranks.THREE;
                else if (t == (int)Ranks.FOUR)
                    deck[j][t].rank = Ranks.FOUR;
                else if (t == (int)Ranks.FIVE)
                    deck[j][t].rank = Ranks.FIVE;
                else if (t == (int)Ranks.SIX)
                    deck[j][t].rank = Ranks.SIX;
                else if (t == (int)Ranks.SEVEN)
                    deck[j][t].rank = Ranks.SEVEN;
                else if (t == (int)Ranks.EIGHT)
                    deck[j][t].rank = Ranks.EIGHT;
                else if (t == (int)Ranks.NINE)
                    deck[j][t].card.rank = Ranks.NINE;
                else if (t == (int)Ranks.TEN)
                    deck[j][t].rank = Ranks.TEN;
                else if (t == (int)Ranks.JACK)
                    deck[j][t].rank = Ranks.JACK;
                else if (t == (int)Ranks.QUEEN)
                    deck[j][t].rank = Ranks.QUEEN;
                else if (t == (int)Ranks.KING)
                    deck[j][t].rank = Ranks.KING;
                else if (t == (int)Ranks.ACE)
                    deck[j][t].rank = Ranks.ACE;
            }

            if (j == (int)Suits.SPADES)
                deck[j][t].suit = Suits.SPADES;
            if (j == (int)Suits.CLUBS)
                deck[j][t].suit = Suits.CLUBS;
            if (j == (int)Suits.SPADES)
                deck[j][t].suit = Suits.HEARTS;
            if (j == (int)Suits.DIAMONDS)
                deck[j][t].suit = Suits.DIAMONDS;

        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
