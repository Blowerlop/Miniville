using UnityEngine;

public class SOCard : ScriptableObject
{
    public enum EName
    {
    
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
    public Sprite sprite;

    public virtual void Effect()
    {
        
    }
}

