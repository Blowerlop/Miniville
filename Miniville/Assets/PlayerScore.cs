using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private Text scoreText;

    public void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    public void UpdateScore(int value)
    {
        scoreText.text = value.ToString();
    }
}
