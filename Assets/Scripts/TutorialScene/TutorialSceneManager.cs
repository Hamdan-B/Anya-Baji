using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManager : MonoBehaviour
{
    public bool task1,
        task2,
        task3,
        task4;

    public GameObject GrandmaObj,
        GrandpaObj,
        ShopwalaObj;

    PlayerManager playerManager;
    public GameObject playerControls;
    public NpcDialogue dialogueSystem;
    bool handleDialogueAssigned;
    public TMP_Text taskText;

    public GameObject cart;
    public CinemachineVirtualCamera cartCam;
    public GameObject collectableContainer;
    bool cartEventComplete = false;

    LoadingSystem loadingSystem;
    public GameObject EndLevelPanel;
    AudioSource audioSource;

    void Start()
    {
        playerControls.SetActive(true);
        Time.timeScale = 1;

        task1 = task2 = task3 = task4 = false;
        playerManager = FindObjectOfType<PlayerManager>();

        cart.SetActive(false);
        cartCam.gameObject.SetActive(false);
        collectableContainer.SetActive(false);

        if (!cart.activeInHierarchy)
        {
            Debug.Log("isNotActive");
        }

        loadingSystem = GameObject
            .FindGameObjectWithTag("LoadingSystem")
            .GetComponent<LoadingSystem>();

        IndicateToGrandma();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        dialogueSystem.OnDialogueFinished -= HandleDialogueFinished;
    }

    void Update()
    {
        if (task4)
        {
            //Tutorial Complete
            dialogueSystem.OnDialogueFinished += HandleDialogueFinished;
        }
        else if (task3)
        {
            taskText.text = "Go to grandma and give her the Tomaotes.";
        }
        else if (task2)
        {
            taskText.text = "Go and get the tomatoes from Shop.";
        }
        else if (task1)
        {
            taskText.text = "Help Grandpa win his TICTACTOE game.";
        }
        else
        {
            taskText.text = "Grandma looks angry, talk to Grandma.";
        }

        // if (task3 && !handleDialogueAssigned)
        // {
        //     dialogueSystem.OnDialogueFinished += HandleDialogueFinished;
        //     handleDialogueAssigned = true;
        // }

        // Hide the tutorial text after timer end
    }

    private void HandleDialogueFinished()
    {
        Debug.Log("Dialogue is done. Running Dialogue Finished function...");
        if (task4)
        {
            //ss
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        PlayerPrefs.SetInt("tutorialEnd", 1);
        PlayerPrefs.SetInt("LevelsUnlocked", 1);
        EndLevelPanel.SetActive(true);
        Destroy(playerControls);
    }

    public void EndLevel()
    {
        //ScreenCapture.CaptureScreenshot("TutorialSS.png");
        loadingSystem.LoadScene("Title");
    }

    public void NextLevel()
    {
        audioSource.Play();

        loadingSystem.LoadScene("Level2");
    }

    //Indications
    public void IndicateToGrandma()
    {
        playerManager.indicatorTarger = GrandmaObj.transform;
    }

    public void IndicateToGrandpa()
    {
        playerManager.indicatorTarger = GrandpaObj.transform;
    }

    public void IndicateToShopwala()
    {
        playerManager.indicatorTarger = ShopwalaObj.transform;
    }
}
