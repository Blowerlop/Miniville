using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string name;
    public List<SOCard> deck;


    public static bool TryBuyCard()
    {
        SOCard card = GameManager.instance.clickedCard;
        bool canBuy = PlayerNetwork.GetGold() >= card.cost;

        if (canBuy == false)
        {
            Debug.Log("You don't have enough money !");
        }
        else if (Game.GetCardNumberOfType(card.name.ToString()) < 1)
        {
            Debug.Log("NoMoreCard");
        }
        else
        {
            BuyCard(card);
        }

        return canBuy;
    }

    private static void BuyCard(SOCard card)
    {
        //SOCard card = pile.PopCard();
        PlayerNetwork.AddCard((int)card.name);
        PlayerNetwork.AddGold(-card.cost);
        Game.TakeCard(card.name.ToString());
        Game.DisplayCards();
    }
}
