using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour
{
    private Stack<SOCards> _cards = new Stack<SOCards>();

    public void AddCard(SOCards card)
    {
        _cards.Push(card);
    }

    public SOCards PopCard()
    {
        return _cards.Pop();
    }
}
