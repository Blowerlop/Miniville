using System.Collections;
using System.Collections.Generic;
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
    public void CardEffetOnPlayer(string playerName)
    {
        SOCard playerCard;
        //Player[] playersArray = GameManager.instance.players;
        PhotonPlayer currentPlayer = Game.GetPlayerByName(playerName);

        for (int k = 0; k < ((int[])currentPlayer.CustomProperties["Deck"]).Length; k++)
        {
            playerCard = CardManager.GetCard(((int[])currentPlayer.CustomProperties["Deck"])[k]);

            foreach (int act in playerCard.activation)
            {
                if (playerCard.color == SOCard.EColor.Bleu && act == Die.face)
                {
                    //currentPlayer.money += playerCard.effect;
                    PlayerNetwork.AddGold(1);
                    Debug.Log($"{currentPlayer.NickName} Get coins --> Blue color now {PlayerNetwork.GetGold()} gold");
                }

                else if (playerCard.color == SOCard.EColor.Vert && act == Die.face)
                {
                    //currentPlayer.money += playerCard.effect;
                    PlayerNetwork.AddGold(1);
                    Debug.Log($"{currentPlayer.NickName} Get coins --> Green color now {PlayerNetwork.GetGold()} gold");
                }
            }
        }
    }
}
