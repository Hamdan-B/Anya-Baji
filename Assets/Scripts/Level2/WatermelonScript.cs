using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class WatermelonScript : MonoBehaviour
{
    bool isInside;
    bool dialogueStart = false;
    Level2Manager level2Manager;
    PlayerControls playerControls;
    public GameObject floatingText;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Interaction.Enable();
        playerControls.Interaction.NPC_Interact.performed += onInteract;
    }

    private void OnDisable()
    {
        playerControls.Interaction.Disable();
        playerControls.Interaction.NPC_Interact.performed -= onInteract;
    }

    void Start()
    {
        level2Manager = FindObjectOfType<Level2Manager>();
    }

    void Update()
    {
        // if (isInside && Input.GetKeyDown(KeyCode.E))
        // {
        //     //Show Shop Menu
        //     ShopPanel.SetActive(true);
        // }
    }

    void onInteract(InputAction.CallbackContext context)
    {
        if (isInside && !dialogueStart)
        {
            if (level2Manager.task1 && !level2Manager.task2)
            {
                GetWatermelon();
            }
        }
    }

    void GetWatermelon()
    {
        level2Manager.task2 = true;
        level2Manager.IndicateToGrandpa();
        Destroy(floatingText);
        Destroy(gameObject);
    }

    public void isNear()
    {
        isInside = !isInside;
    }
}
