using UnityEngine;

public class GlassDisplay : Singleton<GlassDisplay>
{
    int bet;
    [SerializeField] private Animator[] animators;

    public void Raise()
    {
        animators[bet].SetTrigger("place");
        bet++;
    }

    public void Clear()
    {
        while (bet > 0)
        {
            animators[bet - 1].SetTrigger("clear");
            animators[bet - 1].SetInteger("winner", 0);
            bet--;
        }
    }

    public void Fill(bool playerWin)
    {
        if (playerWin)
        {
            for (int i = 0; i < bet; i++)
            {
                animators[i].SetInteger("winner", 1);
            }
        }   else
        {
            for (int i = 0; i < bet; i++)
            {
                animators[i].SetInteger("winner", 2);
            }
        }
    }
}