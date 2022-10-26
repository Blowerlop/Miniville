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
        Stade
    }

    public enum EColor
    {
        Bleu,
        Vert,
        Rouge
    }
    
    public new EName name;
    public EColor color;
    public int cost;
    public int[] activation;
    public Sprite sprite;
}



