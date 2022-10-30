using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerNetwork : MonoBehaviour
{
    bool master;
    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        master = PhotonNetwork.isMasterClient;
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int GetGold()
    {
        return (int)PhotonNetwork.player.CustomProperties["Gold"];
    }

    public static void SetGold(int gold)
    {
        Hashtable hash = new Hashtable();
        hash.Add("Gold", gold);
        PhotonNetwork.player.SetCustomProperties(hash);
    }


    public static void AddGold(int gold)
    {
        Hashtable hash = new Hashtable();
        hash.Add("Gold", GetGold() + gold);
        PhotonNetwork.player.SetCustomProperties(hash);
    }

    public static void AddCard(int ID)
    {
        Hashtable hash = new Hashtable();
        hash.Add("Deck", GetCards().Concat(new int[] { ID }).ToArray());
        PhotonNetwork.player.SetCustomProperties(hash);

    }

    public void RemoveCard(int ID)
    {
        int[] myArray = GetCards();
        myArray = myArray.Where((source, index) => index != ID).ToArray();
        Hashtable hash = new Hashtable();
        hash.Add("Deck", myArray);
        PhotonNetwork.player.SetCustomProperties(hash);
    }

    public static int[] GetCards()
    {
        return (int[])PhotonNetwork.player.CustomProperties["Deck"];
    }

    [PunRPC]
    public void AddGoldRPC(int gold)
    {
        Hashtable hash = new Hashtable();
        hash.Add("Gold", GetGold() + gold);
        PhotonNetwork.player.SetCustomProperties(hash);
    }


    [PunRPC]
    public void DisplayCards(int[] ids)
    {
        Game.DisplayCards(ids);
    }

    [PunRPC]
    public void MasterDisplay()
    {
        PhotonNetwork.RPC(Game.pv, "DisplayCards", PhotonTargets.All, false, (int[])Game.players[GameManager.instance.turn].CustomProperties["Deck"]);
        Game.DisplayCardsLocal();
    }

    [PunRPC]
    public void CardEffetOnPlayer(string playerName, int face)
    {
        SOCard playerCard;
        //Player[] playersArray = GameManager.instance.players;
        PhotonPlayer currentPlayer = Game.GetPlayerByName(playerName);
        for (int k = 0; k < ((int[])PhotonNetwork.player.CustomProperties["Deck"]).Length; k++)
        {
            playerCard = CardManager.GetCard(((int[])PhotonNetwork.player.CustomProperties["Deck"])[k]);

            foreach (int act in playerCard.activation)
            {
                if (playerCard.color == SOCard.EColor.Bleu && act == face)
                {
                    //currentPlayer.money += playerCard.effect;
                    PlayerNetwork.AddGold(playerCard.coinEffect);
                    Debug.Log($"{currentPlayer.NickName} Get coinEffect --> Blue color now {PlayerNetwork.GetGold()} gold");
                }

                else if (playerCard.color == SOCard.EColor.Vert && act == face && playerName == PhotonNetwork.player.NickName)
                {
                    //currentPlayer.money += playerCard.effect;
                    PlayerNetwork.AddGold(playerCard.coinEffect);
                    Debug.Log($"{currentPlayer.NickName} Get coinEffect --> Green color now {PlayerNetwork.GetGold()} gold");
                }
            }
        }
    }
}
