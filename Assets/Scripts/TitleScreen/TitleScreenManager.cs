using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject TitleMenu;
    public GameObject temp_IntroPanel;
    public GameObject levelScreen;

    LoadingSystem loadingSystem;

    void Start()
    {
        loadingSystem = GameObject
            .FindGameObjectWithTag("LoadingSystem")
            .GetComponent<LoadingSystem>();
    }

    void Update() { }

    public void StartBtn()
    {
        Debug.Log("Start");
        int firstTime = PlayerPrefs.GetInt("firstTime", 0);
        if (firstTime == 0)
        {
            //Start Anya's Story (Cutscene or Text)
            StartIntro();
        }
        else
        {
            //Go To Level Selection
            LevelScreen();
        }
    }

    public void SettingBtn()
    {
        Debug.Log("Setting");
    }

    public void ExitBtn()
    {
        Debug.Log("Exit");
    }

    public void StartLevel1()
    {
        //Start Level After Cutscene
        temp_IntroPanel.SetActive(false);
        loadingSystem.LoadScene("Level1");
    }

    public void StartLevel2()
    {
        loadingSystem.LoadScene("Level2");
    }

    void StartIntro()
    {
        TitleMenu.SetActive(false);
        temp_IntroPanel.SetActive(true);
        PlayerPrefs.SetInt("firstTime", 1);
    }

    void LevelScreen()
    {
        TitleMenu.SetActive(false);
        levelScreen.SetActive(true);
    }
}
