using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopWala : MonoBehaviour
{
    bool isInside;
    bool dialogueStart = false;
    TutorialSceneManager tutorialSceneManager;
    public CinemachineVirtualCamera shopWalaVirtualCam;

    public GameObject ShopPanel;

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
        tutorialSceneManager = FindObjectOfType<TutorialSceneManager>();
    }

    void Update()
    {
        // if (isInside && Input.GetKeyDown(KeyCode.E))
        // {
        //     //Show Shop Menu
        //     ShopPanel.SetActive(true);
        // }
    }

    void onInteract(InputAction.CallbackContext context)
    {
        if (tutorialSceneManager.task2 && !tutorialSceneManager.task3 && isInside && !dialogueStart)
        {
            FindObjectOfType<PlayerManager>().SwitchCamera(shopWalaVirtualCam);
            if (tutorialSceneManager.task2)
            {
                GiveTomatoes();
            }
        }
    }

    void GiveTomatoes()
    {
        NpcDialogue npcDialogueSystem = FindObjectOfType<NpcDialogue>();
        npcDialogueSystem.dialogues = new string[]
        {
            "Hello Anya!, you want tomaotes?",
            "Here you go!. Thankyou for shopping.",
        };
        npcDialogueSystem.StartDialogue();
        dialogueStart = true;
        tutorialSceneManager.task3 = true;
        tutorialSceneManager.IndicateToGrandma();
    }

    public void isNearShop()
    {
        isInside = !isInside;
    }
}
