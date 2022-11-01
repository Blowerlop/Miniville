using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    private RectTransform _textMeshProUGUIRectTransform;

    private void Start()
    {
        _textMeshProUGUIRectTransform = _textMeshProUGUI.GetComponent<RectTransform>();
    }

    public async void UpdateText(string text)
    {
        _textMeshProUGUIRectTransform.localPosition = new Vector2(0, -Screen.currentResolution.height / 2 + 50);
        _textMeshProUGUI.text = $"{text}'s turn";

        _textMeshProUGUIRectTransform.DOScale(new Vector3(5, 5, 5), 0.0f);
        await Task.Delay(2000);
        _textMeshProUGUIRectTransform.DOLocalMoveY(0.0f, 2.0f);
        _textMeshProUGUIRectTransform.DOScale(Vector3.one, 2.0f);
    }
}
