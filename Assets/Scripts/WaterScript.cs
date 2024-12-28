using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //GameOver
            Debug.Log("GameOver");
            FindObjectOfType<TutorialSceneManager>().LevelLost();
        }
    }
}
