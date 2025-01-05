using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatAmplitude = 0.5f;
    public float floatDuration = 2f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 360f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;

        StartFloating();

        StartRotating();
    }

    private void StartFloating()
    {
        transform
            .DOMoveY(initialPosition.y + floatAmplitude, floatDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void StartRotating()
    {
        transform
            .DORotate(
                new Vector3(0f, 360f, 0f),
                1f / (rotationSpeed / 360f),
                RotateMode.FastBeyond360
            )
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }
}
