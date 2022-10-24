using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Create New Card")]
public class SOCards : ScriptableObject
{
    public new EName name;
    public EColor color;
    public int cost;
    public int activation;
    public Sprite sprite;
}

public enum EName
{
    ChampsDeBle,
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

