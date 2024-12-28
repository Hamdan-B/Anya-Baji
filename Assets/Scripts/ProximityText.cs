using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProximityText : MonoBehaviour
{
    public GameObject interactBtn;

    public UnityEvent onTriggerEnterAction;
    public UnityEvent onTriggerExitAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactBtn.SetActive(true);
            onTriggerEnterAction?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactBtn.SetActive(false);
            onTriggerExitAction?.Invoke();
        }
    }
}
