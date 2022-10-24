using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pile
{
    private Stack<SOCard> _cards = new Stack<SOCard>();

    public void AddCard(SOCard card)
    {
        _cards.Push(card);
    }

    public SOCard PopCard()
    {
        return _cards.Pop();
    }
}
