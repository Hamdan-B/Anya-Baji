using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Manager : MonoBehaviour
{
    public bool task1,
        task2,
        task3;

    public GameObject GrandpaObj,
        ShopwalaObj;

    PlayerManager playerManager;
    public GameObject playerControls;
    public NpcDialogue dialogueSystem;
    bool handleDialogueAssigned;
    public TMP_Text taskText;

    LoadingSystem loadingSystem;
    public GameObject EndLevelPanel;

    void Start()
    {
        playerControls.SetActive(true);
        Time.timeScale = 1;

        task1 = task2 = task3 = false;
        playerManager = FindObjectOfType<PlayerManager>();

        loadingSystem = GameObject
            .FindGameObjectWithTag("LoadingSystem")
            .GetComponent<LoadingSystem>();

        IndicateToGrandpa();
    }

    private void OnDestroy()
    {
        dialogueSystem.OnDialogueFinished -= HandleDialogueFinished;
    }

    void Update()
    {
        if (task3)
        {
            dialogueSystem.OnDialogueFinished += HandleDialogueFinished;
        }
        else if (task2)
        {
            taskText.text = "Give the glasses to Grandma.";
        }
        else if (task1)
        {
            taskText.text = "Grandma lost her glasses, go and find them!";
        }
        else
        {
            taskText.text = "Talk to Grandma.";
        }
    }

    private void HandleDialogueFinished()
    {
        Debug.Log("Dialogue is done. Running Dialogue Finished function...");
        if (task3)
        {
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        PlayerPrefs.SetInt("LevelsUnlocked", 3);

        EndLevelPanel.SetActive(true);
        Destroy(playerControls);
    }

    public void EndLevel()
    {
        //ScreenCapture.CaptureScreenshot("TutorialSS.png");
        loadingSystem.LoadScene("Title");
    }

    //Indications

    public void IndicateToGrandpa()
    {
        playerManager.indicatorTarger = GrandpaObj.transform;
    }

    public void IndicateToShopwala()
    {
        playerManager.indicatorTarger = ShopwalaObj.transform;
    }
}
