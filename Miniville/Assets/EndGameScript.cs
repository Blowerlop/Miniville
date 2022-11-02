using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text TMPtext;
    // Start is called before the first frame update
    void Start()
    {
        foreach(PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (player.IsMasterClient)
            {
                TMPtext.text = (string)player.CustomProperties["Winner"] + " gagne la partie avec: " + (int)player.CustomProperties["WinnerGold"] + " pieces";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
