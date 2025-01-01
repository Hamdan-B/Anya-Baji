using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level2Grandpa : MonoBehaviour
{
    bool isInside;
    bool dialogueStart = false;
    Level2Manager level2Manager;
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
        level2Manager = FindObjectOfType<Level2Manager>();
    }

    void Update() { }

    void onInteract(InputAction.CallbackContext context)
    {
        if (isInside && !dialogueStart)
        {
            if (!level2Manager.task1)
            {
                FindObjectOfType<PlayerManager>().SwitchCamera(grandpaVirtualCam);
                AskAnyaToGetWatermelon();
            }
            else if (level2Manager.task1 && !level2Manager.task3)
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
            "Anya!, Thank god you came!",
            "Im so hungry, can you go and find a watermelon for me.",
        };
        npcDialogueSystem.StartDialogue();
        dialogueStart = true;
        level2Manager.task1 = true;
        level2Manager.IndicateToShopwala();
    }

    void ThankAnya()
    {
        NpcDialogue npcDialogueSystem = FindObjectOfType<NpcDialogue>();
        npcDialogueSystem.dialogues = new string[]
        {
            "Thank you so much Anya!",
            "Now I can finally eat.",
        };
        npcDialogueSystem.StartDialogue();
        dialogueStart = true;
        level2Manager.task3 = true;
    }

    public void isNear()
    {
        isInside = !isInside;
        if (!isInside)
            dialogueStart = false;
    }
}
