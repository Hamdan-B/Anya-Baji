using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class NpcDialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text tmpText;
    public string[] dialogues;

    int currentInd = 0;
    public bool isDialogueShown = false;
    public delegate void DialogueFinished();
    public event DialogueFinished OnDialogueFinished;

    PlayerManager playerManager;

    PlayerControls playerControls;
    GameObject playerTouchControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Interaction.Enable();
        playerControls.Interaction.DialogueSkip.performed += SkipDialogue;
    }

    private void OnDisable()
    {
        playerControls.Interaction.Disable();
        playerControls.Interaction.DialogueSkip.performed -= SkipDialogue;
    }

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerTouchControls = GameObject.FindGameObjectWithTag("PlayerTouchControls");
    }

    void Update() { }

    public void StartDialogue()
    {
        if (playerTouchControls)
        {
            playerTouchControls.SetActive(false);
        }
        playerManager.isDialogueFinished = false;
        dialogueBox.SetActive(true);
        if (dialogues.Length > 0)
        {
            tmpText.text = dialogues[currentInd];
            currentInd++;
            isDialogueShown = true;
        }
    }

    void SkipDialogue(InputAction.CallbackContext context)
    {
        Debug.Log("SkipBtn Pressed");
        if (isDialogueShown)
        {
            if (dialogues.Length >= 1 && currentInd < dialogues.Length)
            {
                tmpText.text = dialogues[currentInd];
                currentInd++;
            }
            else if (dialogues.Length >= 1 && currentInd >= dialogues.Length)
            {
                playerManager.isDialogueFinished = true;
                OnDialogueFinished?.Invoke();

                tmpText.text = "";
                isDialogueShown = false;
                dialogueBox.SetActive(false);
                currentInd = 0;
                playerManager.ResetCamera();
                playerTouchControls.SetActive(true);
            }
        }
    }
}
