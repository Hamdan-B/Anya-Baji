using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    RingTossManager ringTossManager;

    void Start()
    {
        ringTossManager = FindObjectOfType<RingTossManager>();
    }

    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball" && ringTossManager.ringTossPoints < 3)
        {
            Debug.Log("Point");
            ringTossManager.ringTossPoints++;
            if (ringTossManager.ringTossPoints == 3)
            {
                ringTossManager.RingTossWin();
            }
        }
    }
}
