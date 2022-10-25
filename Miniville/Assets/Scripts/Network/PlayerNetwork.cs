using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerNetwork : MonoBehaviour
{
    bool master;
    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        master = PhotonNetwork.isMasterClient;
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGold()
    {
        return (int)PhotonNetwork.player.CustomProperties["Gold"];
    }

    public void SetGold(int gold)
    {
        Hashtable hash = new Hashtable();
        hash.Add("Gold", gold);
        PhotonNetwork.player.SetCustomProperties(hash);
    }

    public void AddGold(int gold)
    {
        Hashtable hash = new Hashtable();
        hash.Add("Gold", GetGold() + gold);
        PhotonNetwork.player.SetCustomProperties(hash);
    }


    [PunRPC]
    public void DisplayCards(int[] ids)
    {
        Game.DisplayCards(ids);
    }
}
