using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private SOCards _card;


    private void OnValidate()
    {
        var imageUI = GetComponentInChildren<Image>();
        if (imageUI != null && _card != null)
        {
            imageUI.sprite = _card.sprite;
        }
    }
}
