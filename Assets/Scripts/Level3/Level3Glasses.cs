using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level3Glasses : MonoBehaviour
{
    bool isInside;
    bool dialogueStart = false;
    Level3Manager level3Manager;
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
        level3Manager = FindObjectOfType<Level3Manager>();
    }

    void Update() { }

    void onInteract(InputAction.CallbackContext context)
    {
        if (isInside && !dialogueStart)
        {
            if (level3Manager.task1 && !level3Manager.task2)
            {
                GetWatermelon();
            }
        }
    }

    void GetWatermelon()
    {
        level3Manager.task2 = true;
        level3Manager.IndicateToGrandpa();
        Destroy(floatingText);
        Destroy(gameObject);
    }

    public void isNear()
    {
        isInside = !isInside;
    }
}
