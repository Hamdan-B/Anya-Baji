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
    public NpcDialogue dialogueSystem;
    bool handleDialogueAssigned;
    public TMP_Text taskText;

    public GameObject cart;
    public CinemachineVirtualCamera cartCam;
    public GameObject collectableContainer;
    bool cartEventComplete = false;

    LoadingSystem loadingSystem;
    public GameObject EndLevelPanel;

    void Start()
    {
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
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        // dialogueSystem.OnDialogueFinished -= HandleDialogueFinished;
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

    private void StartCartThing()
    {
        Debug.Log("Cart action executed.");

        cart.SetActive(true);
        cartCam.gameObject.SetActive(true);
        collectableContainer.SetActive(true);
        //playerManager.gameObject.SetActive(false);
        playerManager.HidePlayer();
        StartCoroutine(changeCamAfterDelay());
    }

    IEnumerator changeCamAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        playerManager.SwitchCamera(cartCam);
    }

    private void StopCartThing()
    {
        //playerManager.gameObject.SetActive(true);
        playerManager.UnHidePlayer();
        cart.SetActive(false);
        cartCam.gameObject.SetActive(false);
        collectableContainer.SetActive(false);
        playerManager.ResetCamera();
        cartEventComplete = true;
    }

    void LevelComplete()
    {
        EndLevelPanel.SetActive(true);
    }

    public void EndLevel()
    {
        PlayerPrefs.SetInt("tutorialEnd", 1);
        PlayerPrefs.SetInt("LevelsUnlocked", 1);
        //ScreenCapture.CaptureScreenshot("TutorialSS.png");
        loadingSystem.LoadScene("Title");
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
