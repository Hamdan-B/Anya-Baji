using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

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

    public Animator grandPaAnim;
    public GameObject chair;

    bool dialogueStart = false;

    void Start()
    {
        tutorialSceneManager = FindObjectOfType<TutorialSceneManager>();
    }

    void Update()
    {
        if (
            tutorialSceneManager.task1
            && !tutorialSceneManager.task2
            && isInside
            && Input.GetKeyDown(KeyCode.E)
        )
        {
            playerManager.SwitchCamera(boardVirtualCam);
            GameObject.FindGameObjectWithTag("ActionText").SetActive(false);
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
        grandPaAnim.CrossFade("StandingIdle", 0, 0);
        chair.SetActive(false);
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
    }
}
