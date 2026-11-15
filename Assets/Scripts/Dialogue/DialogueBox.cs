using System.Collections;
using TMPro;
using UnityEngine;

public abstract class DialogueBox : MonoBehaviour
{
    protected string _sourceText;
    protected string _activeText;
    protected bool _textFullyLoaded;
    protected int _currentCharacter;
    protected int _textSpeed;
    public TextAsset dialogueSource;
    public Dialogue dialogue;
    [SerializeField] private TextMeshProUGUI textMesh;

    virtual protected void Awake()
    {
        dialogue = new();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual protected void Start()
    {
        _textSpeed = 80;
        _currentCharacter = 0;
        dialogue.LoadDialogue(dialogueSource);
        _sourceText = "...";
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (!_textFullyLoaded)
        {
            textMesh.text = _activeText;
        }
    }

    void FixedUpdate()
    {
        // im putting this into FixedUpdate because the framerate is so high 
        // that the characters to add keeps rounding down to 0 so this way it
        // will be consistent regardless of frame rate
        if (!_textFullyLoaded)
        {
            LoadTextTypewriter();
        }
    }

    /// <summary>
    /// Called constantly whenever text is not fully loaded, appends text from source to active based on the text speed and time elapsed
    /// </summary>
    public void LoadTextTypewriter()
    {
        int charactersToAdd;
        if (_currentCharacter + (int)(_textSpeed * Time.fixedDeltaTime) > _sourceText.Length)
        {
            charactersToAdd = _sourceText.Length - _currentCharacter;
            _textFullyLoaded = true;
        }   else
        {
            charactersToAdd = (int)(_textSpeed * Time.fixedDeltaTime);
        }
        string textToAdd = _sourceText.Substring(_currentCharacter, charactersToAdd);
        _activeText += textToAdd;
        _currentCharacter += charactersToAdd;
    }

    public void UpdateText(string source)
    {
        _textFullyLoaded = false;
        _currentCharacter = 0;
        _sourceText = source;
        _activeText = "";
    }

   abstract public IEnumerator PlayDialogueFromEvent(string eventName);
}
