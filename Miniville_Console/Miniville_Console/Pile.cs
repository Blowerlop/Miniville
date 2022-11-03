public class Pile
{
    private Stack<Card> _cards = new Stack<Card>();

    public void ShowCard()
    {
        Card card = _cards.Peek();
        Console.Write(card);
        Console.WriteLine($" | {card.cost}$");
    }

    public void AddCard(Card card)
    {
        _cards.Push(card);
    }

    public Card PopCard()
    {
        return _cards.Pop();
    }

    public Card GetCard()
    {
        return _cards.Peek();
    }
}