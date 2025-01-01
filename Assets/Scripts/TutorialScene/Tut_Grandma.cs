using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tut_Grandma : MonoBehaviour
{
    bool isInside = false;
    bool dialogueStart = false;

    TutorialSceneManager tutorialSceneManager;

    PlayerManager playerManager;
    public CinemachineVirtualCamera grandmaVirtualCam;
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
        playerManager = FindObjectOfType<PlayerManager>();
    }

    void Update() { }

    void onInteract(InputAction.CallbackContext context)
    {
        if (isInside && !dialogueStart)
        {
            // playerManager.HidePlayer();

            if (tutorialSceneManager.task3)
            {
                FindObjectOfType<PlayerManager>().SwitchCamera(grandmaVirtualCam);
                ThankAnyaForTomato();
            }
            else if (!tutorialSceneManager.task1)
            {
                FindObjectOfType<PlayerManager>().SwitchCamera(grandmaVirtualCam);
                AskAnyaToFindGrandpa();
            }
            //GameObject.FindGameObjectWithTag("NpcInteractBtn").SetActive(false);
        }
    }

    public void isNearGrandMa()
    {
        isInside = !isInside;
        if (!isInside)
            dialogueStart = false;
    }

    void AskAnyaToFindGrandpa()
    {
        NpcDialogue npcDialogueSystem = FindObjectOfType<NpcDialogue>();
        npcDialogueSystem.dialogues = new string[]
        {
            "Go and find your grandpa, I sent him to get some tomatoes!",
        };
        npcDialogueSystem.StartDialogue();
        dialogueStart = true;
        tutorialSceneManager.task1 = true;
        tutorialSceneManager.IndicateToGrandpa();
    }

    void ThankAnyaForTomato()
    {
        NpcDialogue npcDialogueSystem = FindObjectOfType<NpcDialogue>();
        npcDialogueSystem.dialogues = new string[]
        {
            "OMG! ThankGod you bought the tomatoes or else I would be embaressed infront of the guests.",
        };
        npcDialogueSystem.StartDialogue();
        dialogueStart = true;
        tutorialSceneManager.task4 = true;
    }
}
