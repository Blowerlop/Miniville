using System.Numerics;

public class Game
{
    private Player[] _players;
    private Pile[] _piles = new Pile[15];
    private bool isFinish;
    private Player player;
    private Random random = new Random();

    int iaDifficulty;

    public void Start()
    {
        for (int i = 0; i < _piles.Length; i++)
        {
            _piles[i] = new Pile();
        }

        _players = new Player[2]
        {
            new Player("Nathan"),
            new Player("IA")
        };

        List<Card> cards = new List<Card>();
        GenerateCards(ref cards);
        InitializePiles(cards);


        Game1();
    }

    public void Game1()
    { 
        // Turn
        int dieFace = 0;
        do
        {
            Console.WriteLine("Niveau de l'IA ?");
            Console.WriteLine("1 : Idiot");
            Console.WriteLine("2 : Normal");
        } while (int.TryParse(Console.ReadLine(), out iaDifficulty) == false || (iaDifficulty > 2 || iaDifficulty < 1));

        while (isFinish == false)
        {
            for (int i = 0; i < _players.Length; i++)
            {
                ShowGameResume();
                TurnInitialization(i, ref dieFace);
                

                
                CardEffectOnOtherPlayers(dieFace);
                CardEffetOnPlayer(dieFace);

                int cursorPosition = Console.CursorTop;
                Console.SetCursorPosition(0,0);
                ShowPlayerResume();
                Console.SetCursorPosition(0, cursorPosition);

                PlayerBuy(i);

                CheckWin();
                //ShowGameResume();
            }

            Console.WriteLine("---------------------------");
            ShowPlayerResume();
            Console.WriteLine("---------------------------");
        }
        

    }


    private void GenerateCards(ref List<Card> cards)
    {
        // Create all cards
        cards = new List<Card>()
        {
            new Card(Card.EName.Champ_De_Ble, Card.EColor.Bleu, 1, 1, 1),
            new Card(Card.EName.Ferme, Card.EColor.Bleu, 2, 1, 1),
            new Card(Card.EName.Boulangerie, Card.EColor.Vert, 1, 2, 2),
            new Card(Card.EName.Cafe, Card.EColor.Rouge, 2, 3, 1),
            new Card(Card.EName.Superette, Card.EColor.Vert, 2, 4, 3),
            new Card(Card.EName.Foret, Card.EColor.Bleu, 2, 5, 1),
            new Card(Card.EName.Restaurant, Card.EColor.Rouge, 4, 5, 2),
            new Card(Card.EName.Stade, Card.EColor.Bleu, 6, 6, 4)
        };

        // Fill the cards with 6 of them each
        int listCount = cards.Count;
        for (int i = 0; i < listCount; i++)
        {
            Card card = cards[i];
            for (int j = 0; j < 5; j++)
            {
                cards.Add(card);
            }
        }

        // Shuffle cards
        Random random = new Random();
        List<Card> temp = cards.OrderBy(x => random.Next()).ToList();
        cards = temp;

        //Console.WriteLine(cards.Count);

        /*
        foreach (var item in cards)
        {
            Console.WriteLine(item.name);
        }
        */
    }

    private void InitializePiles(List<Card> cards)
    {
        int counter = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            if (counter > _piles.Length -1) counter = 0;
            _piles[counter].AddCard(cards[i]);
            counter++;
        }
    }

    private void ShowPiles()
    {
        for (int i = 0; i < _piles.Length; i++)
        {
            int index = i + 1;
            Console.Write(index.ToString() + String.Concat(Enumerable.Repeat("", CountDigits(index) - 1)) + " --- ");
            _piles[i].ShowCard();
        }


    }

    private void TurnInitialization(int i, ref int dieFace)
    {
        // i player turn
        player = _players[i];

        ShowGameResume();
        Console.WriteLine();
        Console.WriteLine($"[{player.name}]");
        // Roll the dice
        if (i % 2 == 1) // Tour de l'IA
        {
            Console.WriteLine();
            if (iaDifficulty == 1)
            {
                dieFace = Die.Roll(random.Next(1, 3));
            }
            else
            {
                for (int j = 0; j < player.deck.Count; j++)
                {
                    if (player.deck[j].activation > 6)
                    {
                        dieFace = Die.Roll(2);
                        return;
                    }
                }
                dieFace = Die.Roll(1);
                
            }
            Thread.Sleep(1500);
        }
        else
        {
            do
            {
                Console.WriteLine("How many die/dice to roll : 1-2");
            }while(int.TryParse(Console.ReadLine(), out dieFace) == false || (dieFace > 2 || dieFace < 1));
            Console.WriteLine();
            dieFace = Die.Roll(dieFace);
        }


    }

    public void CardEffectOnOtherPlayers(int dieFace)
    {
        Card playerCard;

        for (int m = 0; m < _players.Length; m++)
        {
            if (player.Equals(_players[m]))
            {
                continue;
            }

            for (int j = 0; j < _players[m].deck.Count; j++)
            {
                playerCard = _players[m].deck[j];

                if (playerCard.color == Card.EColor.Bleu && playerCard.activation == dieFace)
                {
                    _players[m].money++;
                    Console.WriteLine($"{_players[m].name} a reçu {playerCard.effect} pièce.s");
                }

                else if (playerCard.color == Card.EColor.Rouge && playerCard.effect == dieFace)
                {
                    player.money -= playerCard.effect;
                    _players[m].money += playerCard.effect;
                    Console.WriteLine($"{_players[m].name} a reçu {playerCard.effect} pièce.s de {player.name}");
                }
            }
        }    
    }

    public void CardEffetOnPlayer(int dieFace)
    {
        Card playerCard;

        for (int k = 0; k < player.deck.Count; k++)
        {
            playerCard = player.deck[k];

            if ((playerCard.color == Card.EColor.Bleu && playerCard.activation == dieFace) || (playerCard.color == Card.EColor.Vert && playerCard.activation == dieFace))
            {
                player.money += playerCard.effect;
                Console.WriteLine($"{player.name} a recu { playerCard.effect} pièce.s");
            }

            else if (playerCard.color == Card.EColor.Vert && playerCard.activation == dieFace)
            {
                player.money += playerCard.effect;
                Console.WriteLine($"{player.name} Get coins --> Vert color");
            }
        }
    }

    private void PlayerBuy(int i)
    {
        if (i % 2 == 1) // Tour de l'IA
        {
            

            if (iaDifficulty == 1)
            {
                List<Pile> availableCards = new List<Pile>();

                for (int j = 0; j < _piles.Length; j++)
                {
                    Card card = _piles[j].GetCard();
                    if (card.cost <= player.money)
                    {
                        availableCards.Add(_piles[j]);
                    }
                }
                player.BuyCard(availableCards[random.Next(0, availableCards.Count)]);

            }
            else
            {
                Pile mostExpensive = _piles[0];
                for (int j = 0; j < _piles.Length; j++)
                {
                    Card card = _piles[j].GetCard();
                    if (card.cost > mostExpensive.GetCard().cost)
                    {
                        mostExpensive = _piles[j];
                    }
                }

                player.BuyCard(mostExpensive);
            }

            Thread.Sleep(1500);
        }
        else
        {
            string reponse;
            do
            {
                Console.WriteLine("Voulez vous acheter une carte ? O/N");
                reponse = Console.ReadLine();
            } while (reponse.ToLower() != "o" && reponse.ToLower() != "n");

            if (reponse.ToLower() == "n")
            {
                return;
            }

            Console.WriteLine();

            bool playerBought;

            do
            {
                Console.WriteLine("Choissisez une carte ou annuler : exit");
                int choice = -1;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    playerBought = true;
                    return;
                }

                Console.WriteLine(choice);
                if (_piles[choice - 1].GetCard().cost > player.money)
                {
                    Console.WriteLine("Vous n'avez pas l'argent !");
                    playerBought = false;
                }
                else
                {
                    player.BuyCard(_piles[choice - 1]);
                    playerBought = true;
                }
            } while (playerBought == false);
        }

    }

    private void CheckWin()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i].money >= 20)
            {
                Console.WriteLine("WINNNNNNER");
                isFinish = true;
            }
        }
    }

    private void ShowGameResume()
    {
        Console.Clear();
        ShowPlayerResume();
        ShowPiles();

        Console.WriteLine("---------------------------");
    }

    private void ShowPlayerResume()
    {
        string offset = "   ";

        List<string> result = new List<string>();
        for (int i = 0; i < _players.Length; i++)
        {
            string[] temp = _players[i].ToString().Split("\n");
            for (int j = 0; j < temp.Length; j++)
            {
                result.Add(temp[j]);
            }
        }

        int phrases = result.Count / _players.Length;
        for (int i = 0; i < phrases; i++)
        {
            string firstPhrase = result[i];
            string secondPhrase = result[i + phrases];

            string finalPhrase = firstPhrase + string.Concat(Enumerable.Repeat(" ", Console.WindowWidth - secondPhrase.Length - firstPhrase.Length )) + secondPhrase;
            Console.WriteLine(finalPhrase);
        }
    }


    private int CountDigits(int number)
    {
        int result = 0;
        while (number != 0)
        {
            number /= 10;
            result++;
        }
        return result;
    }
}