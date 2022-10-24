using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log($"Plus d'une instance de {this.name}");
            return;
        }
        
        instance = this;
    }

    [SerializeField] private Player[] _players;
    [SerializeField] private Pile[] _piles = new Pile[15];
}
