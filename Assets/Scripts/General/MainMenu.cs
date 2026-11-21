using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class MainMenu : MonoBehaviour
{
    InputAction move;
    InputAction enter;
    public SpriteResolver start;
    public SpriteResolver exit;

    int selection = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("NavigateVertical");
        move.performed += Move;
        enter = InputSystem.actions.FindAction("Enter");
        enter.performed += Select;
    }

    private void Move(InputAction.CallbackContext context)
    {
        float cursor = context.ReadValue<float>();

        if (cursor < 0)
        {
            start.SetCategoryAndLabel("start", "inactive");
            exit.SetCategoryAndLabel("exit", "active");
            selection = 2;
        }   else if (cursor > 0)
        {
            start.SetCategoryAndLabel("start", "active");
            exit.SetCategoryAndLabel("exit", "inactive");
            selection = 1;
        }
    }

    private void Select(InputAction.CallbackContext context)
    {
        if (selection == 1)
        {
            enter.performed -= Select;
            move.performed -= Move;
            SceneManager.LoadScene("Main");
        }   else if (selection == 2)
        {
            Application.Quit();
        }
    }
}
