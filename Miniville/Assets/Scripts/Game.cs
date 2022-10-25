using System;
using System.Collections.Generic;
using System.Linq;
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




        // Turn
        int dieFace = 0;

        while (isFinish == false)
        {
            for (int i = 0; i < GameManager.instance.players.Length; i++)
            {
                TurnInitialization(i);
                break;

                //CardEffectOnOtherPlayers(dieFace);
                //CardEffetOnPlayer(dieFace);
                //PlayerBuy();

                //CheckWin();

            }

            //ShowPlayerResume();
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

    private void TurnInitialization(int i)
    {
        // i player turn
        GameManager.instance.currentPlayer = GameManager.instance.players[i];
        Debug.Log($"{GameManager.instance.currentPlayer.name} turn");


    }
}
