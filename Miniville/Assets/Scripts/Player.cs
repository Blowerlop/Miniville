using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string name;
    public List<SOCard> deck;
    public static bool canBuyCard = true;

    public static bool TryBuyCard()
    {
        Card card = GameManager.instance.clickedCard;
        SOCard soCard = card.card;
        bool canBuy = PlayerNetwork.GetGold() >= soCard.cost;
        //canBuy = card.canBeBought;

        if (canBuy == false || !canBuyCard)
        {
            Debug.Log("You can't buy !");
        }
        else if (Game.GetCardNumberOfType(soCard.name.ToString()) < 1)
        {
            Debug.Log("NoMoreCard");
        }
        else
        {
            BuyCard(soCard);
            canBuyCard = false;
        }

        return canBuy;
    }

    private static void BuyCard(SOCard card)
    {
        //SOCard card = pile.PopCard();
        PlayerNetwork.AddCard((int)card.name);
        PlayerNetwork.AddGold(-card.cost);
        Game.TakeCard(card.name.ToString());
        Game.DisplayCardsLocal();
         // Debug.Log(  Game.DeckCards.IndexOf(GameManager.instance.clickedCard.gameObject));
        if (Game.GetCardNumberOfType(card.name.ToString()) < 1)
        {
            
            foreach (var go in CardManager._cards)
            {

                if (go == card)
                {
                    Game.cards.Remove(go);
                    Debug.Log($"Remove {card.name}");
                    Game.RemoveMiddleCard(go.name.ToString());

                }
            }
            Game.DisplayPiles();
        }
        if (PhotonNetwork.player.IsMasterClient)
        {
            PhotonNetwork.RPC(Game.pv, "DisplayCards", PhotonTargets.All, false, (int[])Game.players[GameManager.instance.turn].CustomProperties["Deck"], "None_Name");
            Game.DisplayCardsLocal();
        }
        else
        {
            foreach (PhotonPlayer pla in Game.players)
            {
                if (pla.IsMasterClient)
                {
                    Game.master = pla;
                }
            }
            PhotonNetwork.RPC(Game.pv, "MasterDisplay", Game.master, false);
        }
    }

    [PunRPC]
    public void RemoveCentercCard(string cardName)
    {
        foreach (SOCard go in CardManager._cards)
        {
            if(go.name.ToString() == cardName)
            {
                Game.cards.Remove(go);
            }
        }
        Game.DisplayPiles();
    }

    
}
