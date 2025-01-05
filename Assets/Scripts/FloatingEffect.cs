using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [Header("Floating Settings")]
    public RectTransform rectTransform; // The RectTransform to float
    public float floatAmplitude = 20f; // The distance to move up and down
    public float floatDuration = 2f; // Time to complete one up-down cycle
    public bool loop = true; // Whether the movement should loop

    private Vector2 initialPosition;

    void Start()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        if (rectTransform != null)
        {
            // Store the initial position
            initialPosition = rectTransform.anchoredPosition;

            // Start floating
            StartFloating();
        }
        else
        {
            Debug.LogError("RectTransform is not assigned!");
        }
    }

    private void StartFloating()
    {
        // Animate to the "up" position
        rectTransform
            .DOAnchorPosY(initialPosition.y + floatAmplitude, floatDuration)
            .SetEase(Ease.InOutSine) // Smooth movement
            .SetLoops(loop ? -1 : 0, LoopType.Yoyo) // Yoyo loop if loop is enabled
            .OnComplete(() =>
            {
                // Reset position after animation completes (if not looping)
                if (!loop)
                {
                    rectTransform.anchoredPosition = initialPosition;
                }
            });
    }
}
