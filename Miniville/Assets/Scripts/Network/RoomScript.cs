using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomScript : MonoBehaviour
{
    public GameObject locker, sc1, sc2, forceButton;
    public GameObject[] champs, profils, team1, team2;
    public Sprite[] sourceImages;
    public bool ready = false, load = false, forceStart = false;
    public string levelToLoad;
    public TMPro.TMP_Text playersNames;
    public void Start()
    {
        Hashtable hash = new Hashtable();
        hash.Add("Ready", false);
        hash.Add("Loaded", false);
        hash.Add("Forced", false);
        hash.Add("Gold", 300);
        hash.Add("Deck",new int[] { });
        if (PhotonNetwork.player.IsMasterClient)
        {
            foreach(SOCard card in CardManager._cards)
            {
                hash.Add(card.name.ToString(), 6);
            }
        }
        PhotonNetwork.player.SetCustomProperties(hash);
        forceButton.SetActive(PhotonNetwork.isMasterClient);
    }
    public void Update()
    {
        try
        {
            NewPlayer();
            bool test = false;
            foreach (PhotonPlayer pla in PhotonNetwork.playerList)
            {
                if ((bool)pla.CustomProperties["Forced"] == true)
                {
                    //PhotonNetwork.isMessageQueueRunning = false;
                    if (!load)
                    {
                        load = true;
                        sc2.SetActive(true);
                        sc1.SetActive(false);
                        StartCoroutine(LoadScenneWithDelay(3, levelToLoad));
                    }
                    break;
                }
                if ((bool)pla.CustomProperties["Ready"] == false)
                {
                    test = true;
                }
            }
            if (!test || forceStart)
            {
                PhotonNetwork.isMessageQueueRunning = false;
                if (!load)
                {
                    load = true;
                    sc2.SetActive(true);
                    sc1.SetActive(false);
                    StartCoroutine(LoadScenneWithDelay(3, levelToLoad));
                }
            }
        }
        catch
        {
            print("pas encore log");
        }
    }
    public void Ready()
    {
        if (!ready)
        {
            ready = true;
            Hashtable hash = new Hashtable();
            hash.Add("Ready", true);
            PhotonNetwork.player.SetCustomProperties(hash);
            locker.SetActive(false);
        }
    }
    
    public void ForceStart()
    {
        Hashtable hash = new Hashtable();
        hash.Add("Forced", true);
        PhotonNetwork.player.SetCustomProperties(hash);
    }

    public IEnumerator LoadScenneWithDelay(int delay, string scenne)
    {
        yield return new WaitForSeconds(delay);
        PhotonNetwork.LoadLevelAsync(scenne);
    }


    void NewPlayer()
    {
        playersNames.text = "";
        foreach (PhotonPlayer pla in PhotonNetwork.playerList)
        {
            playersNames.text += "\n" + pla.NickName;
        }
    }
    
}
