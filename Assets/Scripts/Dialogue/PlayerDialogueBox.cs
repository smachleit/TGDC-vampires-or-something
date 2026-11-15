using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerDialogueBox : DialogueBox
{
    // Library of prompts and the source file
    public Prompts prompts;
    public TextAsset promptsSource;


    InputAction move;
    InputAction enter;
    private int selectedOption;
    private bool submit;

    // References to the option text fields for editing
    [SerializeField] private TextMeshProUGUI opt1;
    [SerializeField] private TextMeshProUGUI opt2;
    private Color activeColor;
    private Color defaultColor;

    protected override void Awake()
    {
        base.Awake();
        prompts = new();
        activeColor = new(1, 0.9489931f, 0.3254717f);
        defaultColor = new(1, 1, 1);
    }

    override protected void Start()
    {
        base.Start();
        move = InputSystem.actions.FindAction("NavigateHorizontal");
        enter = InputSystem.actions.FindAction("Enter");
        move.performed += context => MoveCursor(context);
        enter.performed += context => SubmitResponse();
        selectedOption = 1;
        prompts.LoadDialogue(promptsSource);
    }

    protected override void Update()
    {
        base.Update();

    }

    public override IEnumerator PlayDialogueFromEvent(string eventName)
    {
        string newMessage = dialogue.GetDialogueFromEvent(eventName);

        if (newMessage == null)
        {
            UpdateText(". . .");
        }   else
        {
            UpdateText(newMessage);
        }

        yield return new WaitUntil(() => _textFullyLoaded);

        // wait a bit for player to read
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator PlayDialogueFromPrompt(string promptName, string status)
    {
        string newMessage = prompts.GetDialogueFromPrompt(promptName, status);

        if (newMessage == null)
        {
            UpdateText(". . .");
        }   else
        {
            UpdateText(newMessage);
        }

        yield return new WaitUntil(() => _textFullyLoaded);

        // wait a bit for player to read
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator Prompt(string promptName, System.Action<int> response)
    {
        // Play prompt text
        yield return StartCoroutine(PlayDialogueFromPrompt(promptName, "prompt"));
        // Wait until done to display options

        opt1.text = prompts.GetOption1(promptName);
        opt2.text = prompts.GetOption2(promptName);
        
        move.Enable();
        enter.Enable();
        
        yield return new WaitUntil(() => submit);

        opt1.text = "";
        opt2.text = "";

        yield return StartCoroutine(PlayDialogueFromPrompt(promptName, "reply" + selectedOption));
        response(selectedOption);
    }

    private void MoveCursor(InputAction.CallbackContext context)
    {
        float cursor = context.ReadValue<float>();

        if (cursor < 0)
        {
            selectedOption = 1;
            opt1.color = activeColor;
            opt2.color = defaultColor;
        }   else if (cursor > 0)
        {
            selectedOption = 2;
            opt1.color = defaultColor;
            opt2.color = activeColor;
        }
    }

    private void SubmitResponse()
    {
        move.Disable();
        enter.Disable();

        submit = true;
    }
}