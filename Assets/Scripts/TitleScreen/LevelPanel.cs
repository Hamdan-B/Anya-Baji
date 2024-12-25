using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    private int levelsUnlocked;

    public GameObject[] levelBtns;

    void OnEnable()
    {
        levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 0);
        if (levelsUnlocked == 0)
        {
            levelBtns[0].SetActive(true);
            levelBtns[1].SetActive(false);
            levelBtns[2].SetActive(false);
            levelBtns[3].SetActive(false);
        }
        else if (levelsUnlocked == 1)
        {
            levelBtns[0].SetActive(true);
            levelBtns[1].SetActive(true);
            levelBtns[2].SetActive(false);
            levelBtns[3].SetActive(false);
        }
        else if (levelsUnlocked == 2)
        {
            levelBtns[0].SetActive(true);
            levelBtns[1].SetActive(true);
            levelBtns[2].SetActive(true);
            levelBtns[3].SetActive(false);
        }
        else if (levelsUnlocked == 3)
        {
            levelBtns[0].SetActive(true);
            levelBtns[1].SetActive(true);
            levelBtns[2].SetActive(true);
            levelBtns[3].SetActive(true);
        }
    }

    void Start() { }

    void Update() { }
}
