using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ShopWala : MonoBehaviour
{
    bool isInside;
    bool dialogueStart = false;
    TutorialSceneManager tutorialSceneManager;
    public CinemachineVirtualCamera shopWalaVirtualCam;

    public GameObject ShopPanel;

    void Start()
    {
        tutorialSceneManager = FindObjectOfType<TutorialSceneManager>();
    }

    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E) && !dialogueStart)
        {
            FindObjectOfType<PlayerManager>().SwitchCamera(shopWalaVirtualCam);
            if (tutorialSceneManager.task2)
            {
                TellAnyaToCollectTomato();
            }
            GameObject.FindGameObjectWithTag("ActionText").SetActive(false);
        }
        // if (isInside && Input.GetKeyDown(KeyCode.E))
        // {
        //     //Show Shop Menu
        //     ShopPanel.SetActive(true);
        // }
    }

    void TellAnyaToCollectTomato()
    {
        NpcDialogue npcDialogueSystem = FindObjectOfType<NpcDialogue>();
        npcDialogueSystem.dialogues = new string[]
        {
            "Im sorry Anya, but my tomatos were stolen by monkeys!",
            "If you want, there might be some leftovers on the road, you can go fetch them.",
            "Take my cart and collect them.",
        };
        npcDialogueSystem.StartDialogue();
        dialogueStart = true;
        tutorialSceneManager.task3 = true;
    }

    public void isNearShop()
    {
        isInside = !isInside;
    }
}
