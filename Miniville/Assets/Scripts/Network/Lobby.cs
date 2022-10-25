using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    static Lobby instance = null;
    GameObject settingsTab;
    public string mapName;

    void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        settingsTab = KILLER.Setting.instance.gameObject;
        PhotonNetwork.ConnectUsingSettings("1.0"); 
    }
    public void RejRoom(int room = 0)
    {
        PhotonNetwork.JoinRoom(room.ToString());
    }
    public void JoinOrCreateRoom()
    {
        RoomOptions MyRoomOption = new RoomOptions();
        MyRoomOption.MaxPlayers = 20;
        MyRoomOption.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom("Main", MyRoomOption, TypedLobby.Default);
    }
    void OnJoinedRoom()
    {
        Debug.Log("Connected");
        PhotonNetwork.isMessageQueueRunning = false;
        PhotonNetwork.LoadLevelAsync(mapName);
    }
    public void OnPhotonCreateRoomFailed()
    {
        JoinOrCreateRoom();
    }
    public void OnPhotonJoinRoomFailed()
    {
        Debug.Log("Error");
    }
    public void SettingsTab()
    {
        if (settingsTab.active == true)
        {
            settingsTab.SetActive(false);
        }
        else
        {
            settingsTab.SetActive(true);
        }
    }
    public void JoinMain()
    {
        RoomOptions MyRoomOption = new RoomOptions();
        MyRoomOption.MaxPlayers = 6;
        MyRoomOption.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom("Main", MyRoomOption, TypedLobby.Default);
    }

    public void SetMap(string map)
    {
        mapName = map;
    }
}