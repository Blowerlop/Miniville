public class Card
{
    public enum EName
    {
        Champ_De_Ble,
        Ferme,
        Boulangerie,
        Cafe,
        Superette,
        Foret,
        Restaurant,
        Stade
    }

    public enum EColor
    {
        Bleu,
        Vert,
        Rouge
    }

    public EName name;
    public EColor color;
    public int cost;
    public int activation;
    public int effect;

    public Card(EName name, EColor color, int cost, int activation, int effect)
    {
        this.name = name;
        this.color = color;
        this.cost = cost;
        this.activation = activation;
        this.effect = effect;
    }

    public override string ToString()
    {
        string toString = $"[{activation}] {name.ToString()} | ";
        if (color == EColor.Rouge)
        {
            toString += $"Recevez {effect} pièce.s de l'adversaire";
        }
        else
        {
            toString += $"Recevez {effect} pièce.s";
        }
        toString += $" | {color}";
        return toString;
    }
}

/*
public class Champs_De_Ble : Card
{
    public Champs_De_Ble(EName name, EColor color, int cost, int activation) : base(name, color, cost, activation)
    {
        
    }

    public override void Effect(Player affectedPlayer)
    {
        throw new NotImplementedException();
    }
}

public class Ferme : Card
{
    public Ferme(EName name, EColor color, int cost, int activation) : base(name, color, cost, activation)
    {

    }

    public override void Effect(Player affectedPlayer)
    {
        throw new NotImplementedException();
    }
}

public class Boulangerie : Card
{
    public Boulangerie(EName name, EColor color, int cost, int activation) : base(name, color, cost, activation)
    {

    }

    public override void Effect(Player affectedPlayer)
    {
        throw new NotImplementedException();
    }
}

public class Cafe : Card
{
    public Cafe(EName name, EColor color, int cost, int activation) : base(name, color, cost, activation)
    {

    }

    public override void Effect(Player affectedPlayer)
    {
        throw new NotImplementedException();
    }
}

public class Superette : Card
{
    public Superette(EName name, EColor color, int cost, int activation) : base(name, color, cost, activation)
    {

    }

    public override void Effect(Player affectedPlayer)
    {
        throw new NotImplementedException();
    }
}

public class Foret : Card
{
    public Foret(EName name, EColor color, int cost, int activation) : base(name, color, cost, activation)
    {

    }

    public override void Effect(Player affectedPlayer)
    {
        throw new NotImplementedException();
    }
}

public class Restaurant : Card
{
    public Restaurant(EName name, EColor color, int cost, int activation) : base(name, color, cost, activation)
    {

    }

    public override void Effect(Player affectedPlayer)
    {
        throw new NotImplementedException();
    }
}

public class Stade : Card
{
    public Stade(EName name, EColor color, int cost, int activation) : base(name, color, cost, activation)
    {

    }

    public override void Effect(Player affectedPlayer)
    {
        throw new NotImplementedException();
    }
}

*/