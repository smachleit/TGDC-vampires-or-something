using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;


/// <summary>
/// Wrapper class for Raise, Hit, and Stand that manages sprite state
/// </summary>
public class PlayerActions : Singleton<PlayerActions>
{
    // 0 is raise, 1 is hit, 2 is stand
    [SerializeField] private SpriteResolver[] buttons;
    int selected;

    enum controls
    {
        RAISE, HIT, STAND
    }

    public void UpdateCurrentAction(int current)
    {
        buttons[selected].SetCategoryAndLabel(((controls)selected).ToString(), "active");
        buttons[current].SetCategoryAndLabel(((controls)current).ToString(), "selected");
        selected = current;
    }

    public void DeactivateActions()
    {
        foreach (SpriteResolver button in buttons)
        {
            button.SetCategoryAndLabel(button.GetCategory(), "inactive");
        }
    }

    public void ActivateActions()
    {
        foreach (SpriteResolver button in buttons)
        {
            button.SetCategoryAndLabel(button.GetCategory(), "active");
        }
        buttons[1].SetCategoryAndLabel("HIT", "selected");
    }
}