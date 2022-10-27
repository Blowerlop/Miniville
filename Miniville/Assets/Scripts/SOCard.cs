using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Create New Card")]
public class SOCard : ScriptableObject
{
    public enum EName
    {
        ChampDeBle,
        Ferme,
        Boulangerie,
        Cafe,
        Superette,
        Foret,
        Restaurant,
        Stade,
        CentreAffaire,
        ChaineTelevision,
        Fromagerie,
        FabriqueMeubles,
        Mine,
        Verger,
        MarcheFruitsLegumes
    }

    public enum EColor
    {
        Bleu,
        Vert,
        Rouge,
        Violet
    }

    public enum EType
    {
        Culture,
        Agriculture,
        Magasin,
        Restauration,
        Environnement,
        Evenementiel,
        Industrie,
        Marche, 
        None
    }
    
    public new EName name;
    public EType type;
    public EColor color;
    public int cost;
    public int[] activation;
    public Sprite sprite;


    //public ECardEffect cardEffect;
    //public CardEffect _cardEffect;
    [Header("Effect")]
    public int coinEffect;
    public EType typeEffect;
}

public enum ECardEffect
{
    GetCoins,
    GetCoinsFromOther
}





