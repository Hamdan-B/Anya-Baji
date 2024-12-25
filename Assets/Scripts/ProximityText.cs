using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProximityText : MonoBehaviour
{
    public GameObject messageUI;

    public UnityEvent onTriggerEnterAction;
    public UnityEvent onTriggerExitAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageUI.SetActive(true);
            onTriggerEnterAction?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageUI.SetActive(false);
            onTriggerExitAction?.Invoke();
        }
    }
}
