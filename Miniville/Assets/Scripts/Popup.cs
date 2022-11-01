using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;


    public void UpdateText(string text)
    {
        _textMeshProUGUI.GetComponent<RectTransform>().transform.position = new Vector2(0, Screen.currentResolution.height / 2);
        _textMeshProUGUI.text = $"{text}'s turn";
    }
}
