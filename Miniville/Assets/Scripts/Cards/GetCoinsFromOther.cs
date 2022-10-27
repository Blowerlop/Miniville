using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetCoinsFromOther : CardEffect
{
    private int coins;

    public GetCoinsFromOther(int coins)
    {
        this.coins = coins;  
    }

    public override void Activate(Player target)
    {
        //PlayerNetwork.AddGold()
    }
}
