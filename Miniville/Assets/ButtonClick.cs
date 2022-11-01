using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    private Button _dieButton;

    private void Awake()
    {
        _dieButton = GetComponent<Button>();
    }

    public void Start()
    {
        _dieButton.onClick.AddListener(RollDie);
    }

    public void RollDie()
    {
        //Die.Roll(1);
    }


}
