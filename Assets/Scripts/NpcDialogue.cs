using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NpcDialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text tmpText;
    public string[] dialogues;

    int currentInd = 0;
    bool isDialogueShown = false;
    public delegate void DialogueFinished();
    public event DialogueFinished OnDialogueFinished;

    PlayerManager playerManager;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        if (isDialogueShown)
        {
            if (
                dialogues.Length >= 1
                && Input.GetKeyDown(KeyCode.F)
                && currentInd < dialogues.Length
            )
            {
                tmpText.text = dialogues[currentInd];
                currentInd++;
            }
            else if (
                dialogues.Length >= 1
                && Input.GetKeyDown(KeyCode.F)
                && currentInd >= dialogues.Length
            )
            {
                playerManager.isDialogueFinished = true;
                OnDialogueFinished?.Invoke();

                tmpText.text = "";
                isDialogueShown = false;
                dialogueBox.SetActive(false);
                currentInd = 0;
                playerManager.ResetCamera();
            }
        }
    }

    public void StartDialogue()
    {
        playerManager.isDialogueFinished = false;
        dialogueBox.SetActive(true);
        if (dialogues.Length > 0)
        {
            tmpText.text = dialogues[currentInd];
            currentInd++;
            isDialogueShown = true;
        }
    }
}
