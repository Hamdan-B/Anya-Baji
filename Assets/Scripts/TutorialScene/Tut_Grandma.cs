using Cinemachine;
using UnityEngine;

public class Tut_Grandma : MonoBehaviour
{
    bool isInside = false;
    bool dialogueStart = false;

    TutorialSceneManager tutorialSceneManager;

    public CinemachineVirtualCamera grandmaVirtualCam;

    void Start()
    {
        tutorialSceneManager = FindObjectOfType<TutorialSceneManager>();
    }

    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E) && !dialogueStart)
        {
            FindObjectOfType<PlayerManager>().SwitchCamera(grandmaVirtualCam);
            if (tutorialSceneManager.task3)
            {
                ThankAnyaForTomato();
            }
            else
            {
                AskAnyaToFindGrandpa();
            }
            GameObject.FindGameObjectWithTag("ActionText").SetActive(false);
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
        tutorialSceneManager.task5 = true;
    }
}
