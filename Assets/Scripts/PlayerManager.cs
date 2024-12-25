using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CinemachineFreeLook tppCam;
    public GameObject mesh;

    public bool isDialogueFinished;

    public List<string> inventoryItems = new List<string>();

    void Start() { }

    void Update() { }

    public void HidePlayer()
    {
        TPP_Controller movementScript = GetComponent<TPP_Controller>();
        movementScript.enabled = false;
        mesh.SetActive(false);
    }

    public void UnHidePlayer()
    {
        TPP_Controller movementScript = GetComponent<TPP_Controller>();
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
    }
}
