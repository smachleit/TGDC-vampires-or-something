using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class GameOverMenu : MonoBehaviour
{
    InputAction move;
    InputAction enter;
    public TextMeshProUGUI restart;
    public TextMeshProUGUI exit;
    private Color active;
    private Color inactive;

    int selection = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        active = restart.color;
        inactive = exit.color;
        restart.enabled = false;
        exit.enabled = false;

        StartCoroutine(DelayInsruction());
    }

    private void Move(InputAction.CallbackContext context)
    {
        float cursor = context.ReadValue<float>();

        if (cursor < 0)
        {
            restart.color = active;
            exit.color = inactive;
            selection = 1;
        }   else if (cursor > 0)
        {
            restart.color = inactive;
            exit.color = active;
            selection = 2;
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

    private IEnumerator DelayInsruction()
    {
        yield return new WaitForSeconds(3);
        move = InputSystem.actions.FindAction("NavigateHorizontal");
        move.performed += Move;
        enter = InputSystem.actions.FindAction("Enter");
        enter.performed += Select;
        restart.enabled = true;
        exit.enabled = true;
    }
}
