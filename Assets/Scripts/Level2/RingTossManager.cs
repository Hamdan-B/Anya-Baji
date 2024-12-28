using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RingTossManager : MonoBehaviour
{
    bool isInside = false;

    public int ringTossPoints = 0;
    public CinemachineVirtualCamera ringTossVirtualCam;
    PlayerManager playerManager;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<PlayerManager>().SwitchCamera(ringTossVirtualCam);
            playerManager.HidePlayer();
        }
    }

    public void isNearRingTossGame()
    {
        isInside = !isInside;
    }

    public void RingTossWin()
    {
        Debug.Log("Event Win");
        playerManager.UnHidePlayer();
        playerManager.ResetCamera();
    }
}
