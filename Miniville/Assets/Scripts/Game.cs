using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class Game : MonoBehaviour
{
     [SerializeField] private List<SOCard> cardsList = new List<SOCard>();

     [SerializeField] private static Card _cardPrefab;
     [SerializeField] private static Transform _otherPlayerCardsUiParent, _mainPlayerCardsUiParent;
     [SerializeField] private Transform _deckCardsUiParent;

    public TMPro.TMP_Text debug;

    private bool isFinish = false;

    public static PhotonPlayer[] players;

    static PhotonView pv;

    private void Start()
    {
        _mainPlayerCardsUiParent = GameObject.Find("Cards MainPlayer").transform;
        _otherPlayerCardsUiParent = GameObject.Find("Cards OtherPlayers").transform;
        GameObject go = (GameObject)Resources.Load("Card");
        _cardPrefab = go.GetComponent<Card>();
        go = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
        pv = go.GetComponent<PhotonView>();
        var tempGameManagerInstance = GameManager.instance;
        for (int i = 0; i < GameManager.instance.piles.Length; i++)
        {
            GameManager.instance.piles[i] = new Pile();
        }

        players = PhotonNetwork.playerList;
        
        GenerateCards();
        InitializePiles();
        
        Game1();
    }

    private void Game1()
    {
        DisplayCardsLocal((int[])PhotonNetwork.player.CustomProperties["Deck"]);
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("Game is on !");

            PhotonNetwork.RPC(pv, "DisplayCards", PhotonTargets.All,false,(int[])players[0].CustomProperties["Deck"]);
        }
    }

    private void GenerateCards()
    {
        // Fill the initial cardsList with 6 of cards each
        int listCount = cardsList.Count;
        for (int i = 0; i < listCount; i++)
        {
            SOCard card = cardsList[i];
            for (int j = 0; j < 5; j++)
            {
                cardsList.Add(card);
            }
        }

        // Shuffle cards
        Random random = new Random();
        List<SOCard> temp = cardsList.OrderBy(x => random.Next()).ToList();
        cardsList = temp;
        
        Debug.Log("Generating cards");
    }
    
    private void InitializePiles()
    {
        int counter = 0;
        for (int i = 0; i < cardsList.Count; i++)
        {
            if (counter > GameManager.instance.piles.Length - 1)
            {
                counter = 0;
            }
            
            GameManager.instance.piles[counter].AddCard(cardsList[i]);
            counter++;
        }
        Debug.Log("Initializing piles");

    }


    public static void DisplayCards(int[] ids)
    {
        for (int j = 0; j < ids.Length; j++)
        {
            Card card;
            card = Instantiate(_cardPrefab, _otherPlayerCardsUiParent);

            card.card = CardManager.GetCard(ids[j]);
            card.LoadImage();
        }
    }
    public static void DisplayCardsLocal(int[] ids)
    {
        for (int j = 0; j < ids.Length; j++)
        {
            Card card;
            card = Instantiate(_cardPrefab, _mainPlayerCardsUiParent);

            card.card = CardManager.GetCard(ids[j]);
            card.LoadImage();
        }
    }

    public void DisplayPiles()
    {
        for (int i = 0; i < GameManager.instance.piles.Length; i++)
        {
            SOCard cardData = GameManager.instance.piles[i].ShowCard();
            Card card = Instantiate(_cardPrefab, _deckCardsUiParent);
            card.card = cardData;
        }
    }

    private static void TurnInitialization(int i)
    {
        // i player turn
        //GameManager.instance.currentPlayer = GameManager.instance.players[i];
        PhotonNetwork.RPC(pv, "DisplayCards", PhotonTargets.All, false, (int[])players[i].CustomProperties["Deck"]);
    }

    private static void CardEffectOnOtherPlayers()
    {
        SOCard playerCard;
        //Player[] playersArray = GameManager.instance.players;

        Player currentPlayer = GameManager.instance.currentPlayer;

        for (int m = 0; m < players.Length; m++)
        {
            if (players[m].NickName == players[GameManager.instance.turn].NickName)
            {
                Debug.Log("Si écrit c'est bon");
                continue;
            }

            for (int j = 0; j < ((int[])players[m].CustomProperties["Deck"]).Length; j++)
            {
                playerCard = CardManager.GetCard(((int[])players[m].CustomProperties["Deck"])[j]);

                foreach (int act in playerCard.activation)
                {
                    if (playerCard.color == SOCard.EColor.Bleu && act == Die.face)
                    {
                        //playersArray[m].money += playerCard.effect;
                        PlayerNetwork.AddGold(1);
                        Debug.Log($"{players[m].NickName} Get coins --> Blue color now {PlayerNetwork.GetGold()} gold");
                    }

                    else if (playerCard.color == SOCard.EColor.Rouge && act == Die.face)
                    {
                        //currentPlayer.money -= playerCard.effect;
                        PhotonNetwork.RPC(pv, "AddGoldRPC", players[GameManager.instance.turn], true, -1);
                        //playersArray[m].money += playerCard.effect;
                        PlayerNetwork.AddGold(1);
                        Debug.Log($"{players[m].NickName} Get coins --> Red color now {PlayerNetwork.GetGold()} gold");
                        Debug.Log($"{players[GameManager.instance.turn].NickName} Loose coins --> Red color now {players[GameManager.instance.turn].CustomProperties["Gold"]} gold");
                    }
                }
            }
        }
    }
    

    private static void NextTurn()
    {
        GameManager.instance.turn++;

        if (GameManager.instance.turn >= players.Length)
        {
            GameManager.instance.turn = 0;
        }
    }

    public static void Play()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            TurnInitialization(GameManager.instance.turn);
            CardEffectOnOtherPlayers();
            PhotonNetwork.RPC(pv, "CardEffetOnPlayer", players[GameManager.instance.turn], true, players[GameManager.instance.turn].NickName);
            //CardEffetOnPlayer(players[GameManager.instance.turn].NickName);

            //for (int i = 0; i < GameManager.instance.players.Length; i++)
            //{
            //    Player player = GameManager.instance.players[i];
            //    Debug.Log($"{player.name} à {player.money}");
            //}

            NextTurn();
        }
    }

    public static PhotonPlayer GetPlayerByName(string name)
    {
        foreach(PhotonPlayer player in players)
        {
            if (player.NickName == name)
                return player;
        }
        return null;
    }

    public void Update()
    {
        debug.text = players[0].CustomProperties["Gold"] + " / " + players[1].CustomProperties["Gold"];

    }
}
