using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject TitleMenu;
    public GameObject temp_IntroPanel;
    public GameObject levelScreen;

    public CinemachineVirtualCamera titleCam,
        storyCam;

    LoadingSystem loadingSystem;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        loadingSystem = GameObject
            .FindGameObjectWithTag("LoadingSystem")
            .GetComponent<LoadingSystem>();
    }

    void Update() { }

    public void StartBtn()
    {
        Debug.Log("Start");
        audioSource.Play();
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
        audioSource.Play();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartLevel1()
    {
        audioSource.Play();

        //Start Level After Cutscene
        temp_IntroPanel.SetActive(false);
        loadingSystem.LoadScene("Level1");
    }

    public void StartLevel2()
    {
        audioSource.Play();

        loadingSystem.LoadScene("Level2");
    }

    public void StartLevel3()
    {
        audioSource.Play();

        loadingSystem.LoadScene("Level2");
    }

    void StartIntro()
    {
        SwitchCamera(storyCam);
        TitleMenu.SetActive(false);
        temp_IntroPanel.SetActive(true);
        PlayerPrefs.SetInt("firstTime", 1);
    }

    void LevelScreen()
    {
        SwitchCamera(storyCam);
        TitleMenu.SetActive(false);
        levelScreen.SetActive(true);
    }

    public void DeleteSave()
    {
        audioSource.Play();

        PlayerPrefs.DeleteAll();
    }

    public void SwitchCamera(CinemachineVirtualCamera _virtualCam)
    {
        Debug.Log("SwitchCam start...");
        foreach (var v_cam in FindObjectsOfType<CinemachineVirtualCamera>())
        {
            v_cam.Priority = 5;
        }
        _virtualCam.Priority = 11;
        Debug.Log("SwitchCam complete...");
    }
}
