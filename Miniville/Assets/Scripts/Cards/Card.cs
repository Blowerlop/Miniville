using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SOCard _card;

    private void Start()
    {
        _card.Effect();
    }
}
