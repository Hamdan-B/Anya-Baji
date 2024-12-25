using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    Transform mainCam;
    Transform unit;
    public Transform worldSpaceCanvas;

    public Vector3 offset;

    private void Start()
    {
        mainCam = Camera.main.transform;
        unit = transform.parent;
        transform.SetParent(worldSpaceCanvas);
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(
            transform.position - mainCam.transform.position
        );

        transform.position = unit.position + offset;
    }
}
