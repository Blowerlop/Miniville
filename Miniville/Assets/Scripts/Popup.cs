using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;


    public void UpdateText(string text)
    {
        _textMeshProUGUI.text = $"{text}'s turn";
    }
}
