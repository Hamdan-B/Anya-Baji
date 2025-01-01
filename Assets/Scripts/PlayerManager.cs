using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public CinemachineVirtualCamera playerCam;
    public GameObject mesh;

    NpcDialogue npcDialogue;
    public bool isDialogueFinished;

    public List<string> inventoryItems = new List<string>();

    public GameObject arrow;
    public Transform indicatorTarger;

    public float stopDistance = 1f;
    public GameObject RestartLevelUI;

    void Start()
    {
        npcDialogue = FindObjectOfType<NpcDialogue>();
    }

    void Update()
    {
        //Arrow indicator
        if (indicatorTarger != null)
        {
            float distance = Vector3.Distance(transform.position, indicatorTarger.position);

            if (distance > stopDistance && !npcDialogue.isDialogueShown)
            {
                Vector3 direction = indicatorTarger.position - transform.position;
                direction.y = 0;
                arrow.transform.rotation = Quaternion.LookRotation(direction);
                arrow.SetActive(true);
            }
            else
            {
                arrow.SetActive(false);
            }
        }

        if (transform.position.y < -3f)
        {
            PlayerGameOver();
        }
    }

    public void HidePlayer()
    {
        TPP_Controller movementScript = GetComponent<TPP_Controller>();
        playerCam.Follow = null;
        movementScript.enabled = false;
        mesh.SetActive(false);
    }

    public void UnHidePlayer()
    {
        TPP_Controller movementScript = GetComponent<TPP_Controller>();
        playerCam.Follow = gameObject.transform;
        movementScript.enabled = true;
        mesh.SetActive(true);
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

    public void ResetCamera()
    {
        foreach (var v_cam in FindObjectsOfType<CinemachineVirtualCamera>())
        {
            v_cam.Priority = 5;
        }
        playerCam.Priority = 10;
    }

    void PlayerGameOver()
    {
        //GameOver
        Debug.Log("GameOver");
        Time.timeScale = 0;
        RestartLevelUI.SetActive(true);
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
