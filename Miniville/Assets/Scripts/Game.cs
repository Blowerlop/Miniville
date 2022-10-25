using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class Game : MonoBehaviour
{
     [SerializeField] private List<SOCard> cardsList = new List<SOCard>();

     [SerializeField] private Transform _cardsUiParent;
     [SerializeField] private Card _cardPrefab;



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

        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.players[0].deck.Count; j++)
            {
                Card card = Instantiate(_cardPrefab, _cardsUiParent);
                card.card = GameManager.instance.players[i].deck[j];
            }
            
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
}
