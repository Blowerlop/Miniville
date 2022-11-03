public class Player
{
    public string name { get; private set; }
    public List<Card> deck { get; private set; }
    public int money = 3;

    public Player(string name)
    {
        this.name = name;
        deck = new List<Card>()
        {
            new Card(Card.EName.Champ_De_Ble, Card.EColor.Bleu, 1, 1, 1),
            new Card(Card.EName.Boulangerie, Card.EColor.Vert, 1, 2, 1)
        };
    }

    public override string ToString()
    {
        string toString = $"{name} \nCoins: {money}\n";
        for (int i = 0; i < deck.Count; i++)
        {
            toString += deck[i] + "\n";
        }
        toString += "\n";
        return toString;
    }

    public void ShowCards()
    {
        Console.WriteLine("Player cards : ");
        for (int i = 0; i < deck.Count; i++)
        {
            Console.WriteLine(deck[i]);
        }
        Console.WriteLine();
    }

    public void BuyCard(Pile pile)
    {
        Card card = pile.PopCard();
        deck.Add(card);
        money -= card.cost;
    }




}