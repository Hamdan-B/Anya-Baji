using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level3Grandma : MonoBehaviour
{
    bool isInside;
    bool dialogueStart = false;
    Level3Manager level3Manager;
    public CinemachineVirtualCamera grandpaVirtualCam;

    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Interaction.Enable();
        playerControls.Interaction.NPC_Interact.performed += onInteract;
    }

    private void OnDisable()
    {
        playerControls.Interaction.Disable();
        playerControls.Interaction.NPC_Interact.performed -= onInteract;
    }

    void Start()
    {
        level3Manager = FindObjectOfType<Level3Manager>();
    }

    void Update() { }

    void onInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Out Function");
        if (isInside && !dialogueStart)
        {
            Debug.Log("Inside 1 if");
            if (!level3Manager.task1)
            {
                FindObjectOfType<PlayerManager>().SwitchCamera(grandpaVirtualCam);
                AskAnyaToGetWatermelon();
            }
            else if (level3Manager.task1 && !level3Manager.task3)
            {
                FindObjectOfType<PlayerManager>().SwitchCamera(grandpaVirtualCam);
                ThankAnya();
            }
        }
    }

    void AskAnyaToGetWatermelon()
    {
        NpcDialogue npcDialogueSystem = FindObjectOfType<NpcDialogue>();
        npcDialogueSystem.dialogues = new string[]
        {
            "Oh Anya!, Is that you?",
            "I lost my glasses, can you go and find them?.",
            "And be wary of one thing!",
            "Not every path leads somewhere, dear—some are as pointless as chasing the wind.",
        };
        npcDialogueSystem.StartDialogue();
        dialogueStart = true;
        level3Manager.task1 = true;
        level3Manager.IndicateToShopwala();
    }

    void ThankAnya()
    {
        NpcDialogue npcDialogueSystem = FindObjectOfType<NpcDialogue>();
        npcDialogueSystem.dialogues = new string[]
        {
            "Thank you so much Anya!",
            "Now I can see again properly.",
        };
        npcDialogueSystem.StartDialogue();
        dialogueStart = true;
        level3Manager.task3 = true;
    }

    public void isNear()
    {
        isInside = !isInside;
        if (!isInside)
            dialogueStart = false;
    }
}
