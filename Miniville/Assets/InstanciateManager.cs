using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class InstanciateManager : MonoBehaviour
{
    PhotonView pv;
    public GameObject prefab;
    bool loaded = false;
    // Start is called before the first frame update
    void Start()
    {
        Hashtable hash = new Hashtable();
        hash.Add("Loaded", true);
        PhotonNetwork.player.SetCustomProperties(hash);
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!loaded && PhotonNetwork.player.IsMasterClient)
        {
            bool test = false;
            foreach (PhotonPlayer pla in PhotonNetwork.playerList)
            {
                if (!(bool)pla.CustomProperties["Loaded"])
                {
                    test = true;
                }
            }
            if (!test)
            {
                Inst();
            }
        }
    }

    public void Inst()
    {
        loaded = true;
        foreach (PhotonPlayer pla in PhotonNetwork.playerList)
        {
            GameObject go = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
            go.GetComponent<PhotonView>().TransferOwnership(pla);
            PhotonNetwork.RPC(pv, "instanciate", go.GetComponent<PhotonView>().owner, false, go.GetComponent<PhotonView>().viewID);
        }
    }
    [PunRPC]
    public void instanciate(int ID)
    {
        foreach(PhotonPlayer pla in PhotonNetwork.playerList)
        {
            if(pla.IsLocal)
            {
                Game.pv = PhotonView.Find(ID);
                Debug.Log(pv.viewID);
            }
        }
        Game.Game1();
    }
}
