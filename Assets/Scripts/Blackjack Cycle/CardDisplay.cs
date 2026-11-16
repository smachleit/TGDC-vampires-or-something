using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(SpriteResolver))]
public class CardDisplay : MonoBehaviour
{
    public Card card;
    public SpriteResolver spriteResolver;
    public Animator animator;

    public void test()
    {
        card = new(Suite.SPADE, Rank.FOUR);
        Draw(card);
    }

    public void Draw(Card card)
    {
        spriteResolver.SetCategoryAndLabel(card.suite.ToString(), card.rank.ToString());
        animator.SetTrigger("spawn");
    }

    public void RoundEnd()
    {
        animator.SetTrigger("hide");
    }

}