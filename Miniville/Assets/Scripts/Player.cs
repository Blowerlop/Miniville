using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string name;
    public List<SOCard> deck;
    public int money = 3;

    public void BuyCard(Pile pile)
    {
        SOCard card = pile.PopCard();
        deck.Add(card);
        money -= card.cost;
    }
}
