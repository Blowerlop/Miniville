using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] public SOCard card;
    
    private void Start()
    {
        var imageUI = GetComponentInChildren<Image>();
        if (imageUI != null)
        {
            imageUI.sprite = card.sprite;
        }
    }

    private void OnValidate()
    {
        var imageUI = GetComponentInChildren<Image>();
        if (imageUI != null && card != null)
        {
            imageUI.sprite = card.sprite;
        }
    }
}
