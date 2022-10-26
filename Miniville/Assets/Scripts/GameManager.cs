using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Pile[] piles;
    public int turn = 0;
    public SOCard clickedCard;

    public Player localPlayer;


    public Player currentPlayer;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log($"Plus d'une instance de {this.name}");
            return;
        }
        
        instance = this;
    }

    public void Start()
    {
        piles = new Pile[15];
    }

}
