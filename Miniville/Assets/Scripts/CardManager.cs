using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    CardManager instance;
    public static List<SOCard> _cards;
    public List<SOCard> cards;

    public void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        _cards = cards;
    }

    public static SOCard GetCard(int id)
    {
        foreach(SOCard card in _cards)
        {
            if(((int)card.name) == id)
            {
                return card;
            }
        }
        return null;
    }
}
