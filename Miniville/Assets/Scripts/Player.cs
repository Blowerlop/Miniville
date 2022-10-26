using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string name;
    public List<SOCard> deck;
    public int money = 3;


    public bool TryBuyCard()
    {
        SOCard card = GameManager.instance.clickedCard;
        bool canBuy = card.cost > money;

        if (canBuy == false)
        {
            Debug.Log("You don't have enough money !");
        }
        else
        {
            BuyCard(card);
        }

        return canBuy;
    }

    private void BuyCard(SOCard card)
    {
        //SOCard card = pile.PopCard();
        deck.Add(card);
        money -= card.cost;
    }
}
