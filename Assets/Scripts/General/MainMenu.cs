using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    InputAction move;
    InputAction enter;
    public TextMeshProUGUI start;
    public TextMeshProUGUI exit;
    Color yellow = new(1,1,0.5f);
    Color white = new(1,1,1);

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
            start.color = white;
            exit.color = yellow;
            selection = 2;
        }   else if (cursor > 0)
        {
            start.color = yellow;
            exit.color = white;
            selection = 1;
        }
    }

    private void Select(InputAction.CallbackContext context)
    {
        if (selection == 1)
        {
            enter.performed -= Select;
            SceneManager.LoadScene("Main");
        }   else if (selection == 2)
        {
            Application.Quit();
        }
    }
}
