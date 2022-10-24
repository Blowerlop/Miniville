using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int name;
    public List<SOCards> deck;
    public int money = 3;

    public void BuyCard(Pile pile)
    {
        SOCards card = pile.PopCard();
        deck.Add(card);
        money -= card.cost;
    }
}
