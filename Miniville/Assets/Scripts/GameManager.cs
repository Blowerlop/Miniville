using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player[] players;
    public Pile[] piles = new Pile[15];

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log($"Plus d'une instance de {this.name}");
            return;
        }
        
        instance = this;
    }

    
}
