using UnityEngine;

public class VampCardDisplay : CardDisplay
{
    public int index;

    public void SetIndex(int i)
    {
        animator.SetInteger("index", i);
        index = i;
    }

    public void RevealHoleCard()
    {
        if (index == 1)
        {
            animator.SetTrigger("reveal");
        }
    }
}