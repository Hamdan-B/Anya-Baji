using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TicTacToeEvent : MonoBehaviour
{
    public PlayerManager playerManager;
    TutorialSceneManager tutorialSceneManager;
    public CinemachineVirtualCamera boardVirtualCam;
    public CinemachineVirtualCamera grandpaVirtualCam;
    public ProximityText promText;

    [SerializeField]
    FloatingText floatingText;
    bool isInside = false;

    bool dialogueStart = false;

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

    void Update() { }

    void onInteract(InputAction.CallbackContext context)
    {
        if (tutorialSceneManager.task1 && !tutorialSceneManager.task2 && isInside)
        {
            playerManager.SwitchCamera(boardVirtualCam);
            floatingText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tutorialSceneManager.task1)
        {
            isInside = true;
        }
    }

    public void AfterWin()
    {
        playerManager.SwitchCamera(grandpaVirtualCam);

        //Set GrandPa for Dialogues
        //grandPaAnim.CrossFade("StandingIdle", 0, 0);
        //chair.SetActive(false);
        tutorialSceneManager.task2 = true;
        AskAnyaToGetTomatoes();
    }

    void AskAnyaToGetTomatoes()
    {
        NpcDialogue npcDialogueSystem = FindObjectOfType<NpcDialogue>();
        npcDialogueSystem.dialogues = new string[]
        {
            "Oh Shoot! I forgot your grandma told me to get some tomatoes.",
            "Hurry! run to Mr.Jhinga shop, buy the tomatoes and give them to your grandma.",
        };
        npcDialogueSystem.StartDialogue();
        tutorialSceneManager.IndicateToShopwala();
    }
}
