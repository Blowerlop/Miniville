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

     [SerializeField] private Card _cardPrefab;
     [SerializeField] private Transform _mainPlayerCardsUiParent;
     [SerializeField] private Transform _deckCardsUiParent;

    private bool isFinish = false;



    private void Start()
    {
        var tempGameManagerInstance = GameManager.instance;
        for (int i = 0; i < GameManager.instance.piles.Length; i++)
        {
            GameManager.instance.piles[i] = new Pile();
        }
        
        GenerateCards();
        InitializePiles();
        
        Game1();
    }

    private void Game1()
    {
        Debug.Log("Game is on !");

        DisplayCards();
        DisplayPiles();



        //PlayerBuy();

        //CheckWin();

        

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

    private void DisplayCards()
    {
        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.players[0].deck.Count; j++)
            {
                Card card = Instantiate(_cardPrefab, _mainPlayerCardsUiParent);
                card.card = GameManager.instance.players[i].deck[j];
            }

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
        GameManager.instance.currentPlayer = GameManager.instance.players[i];
        Debug.Log($"{GameManager.instance.currentPlayer.name} turn");


    }

    private static void CardEffectOnOtherPlayers()
    {
        SOCard playerCard;
        Player[] playersArray = GameManager.instance.players;
        Player currentPlayer = GameManager.instance.currentPlayer;

        for (int m = 0; m < playersArray.Length; m++)
        {
            if (GameManager.instance.currentPlayer.Equals(playersArray[m]))
            {
                Debug.Log("Si écrit c'est bon");
                continue;
            }

            for (int j = 0; j < playersArray[m].deck.Count; j++)
            {
                playerCard = playersArray[m].deck[j];

                if (playerCard.color == SOCard.EColor.Bleu && playerCard.activation == Die.face)
                {
                    //playersArray[m].money += playerCard.effect;
                    playersArray[m].money += 1;
                    Debug.Log($"{playersArray[m].name} Get coins --> Blue color");
                }

                else if (playerCard.color == SOCard.EColor.Rouge && playerCard.activation == Die.face)
                {
                    //currentPlayer.money -= playerCard.effect;
                    currentPlayer.money -= 1;
                    //playersArray[m].money += playerCard.effect;
                    playersArray[m].money += 1;
                    Debug.Log($"{playersArray[m].name} Get coins --> Red color");
                    Debug.Log($"{currentPlayer.name} Loose coins --> Red color");
                }
            }
        }
    }

    private static void CardEffetOnPlayer()
    {
        SOCard playerCard;
        Player[] playersArray = GameManager.instance.players;
        Player currentPlayer = GameManager.instance.currentPlayer;

        for (int k = 0; k < currentPlayer.deck.Count; k++)
        {
            playerCard = currentPlayer.deck[k];

            if (playerCard.color == SOCard.EColor.Bleu && playerCard.activation == Die.face)
            {
                //currentPlayer.money += playerCard.effect;
                currentPlayer.money += 1;
                Debug.Log($"{currentPlayer.name} Get coins --> Blue color");
            }

            else if (playerCard.color == SOCard.EColor.Vert && playerCard.activation == Die.face)
            {
                //currentPlayer.money += playerCard.effect;
                currentPlayer.money += 1;
                Debug.Log($"{currentPlayer.name} Get coins --> Green color");
            }
        }
    }

    private static void NextTurn()
    {
        GameManager.instance.turn++;

        if (GameManager.instance.turn >= GameManager.instance.players.Length)
        {
            GameManager.instance.turn = 0;
        }
    }

    public static void Play()
    {
        TurnInitialization(GameManager.instance.turn);
        CardEffectOnOtherPlayers();
        CardEffetOnPlayer();

        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            Player player = GameManager.instance.players[i];
            Debug.Log($"{player.name} à {player.money}");
        }

        NextTurn();
    }
}
