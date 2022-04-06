using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    public static PhotonLauncher launcherInstance;

    [SerializeField] TMP_InputField CreateInput;
    [SerializeField] TMP_Text ErrorText;
    [SerializeField] TMP_Text RoomNameText;
    [SerializeField] Transform RoomContainer;
    [SerializeField] GameObject RoomButtonPrefabs;
    [SerializeField] Transform PlayerListContainer;
    [SerializeField] GameObject PlayerNamePrefabs;

    private void Awake()
    {
        launcherInstance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        MenuSystem.MenuInstance.openMenu("Loading");
        Debug.Log("connecting to master...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined lobby");
        MenuSystem.MenuInstance.openMenu("Lobby");
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
    }

    public void createRoom()
    {
        if (string.IsNullOrEmpty(CreateInput.text))
        {
            return;
        }
        else
        {
            PhotonNetwork.CreateRoom(CreateInput.text);
            MenuSystem.MenuInstance.openMenu("Loading");
        }
    }

    public void joinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
        MenuSystem.MenuInstance.openMenu("Loading");

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room : " + PhotonNetwork.CurrentRoom.Name);
        MenuSystem.MenuInstance.openMenu("Room");
        RoomNameText.text = "Room Name : " + PhotonNetwork.CurrentRoom.Name;


        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(PlayerNamePrefabs, PlayerListContainer).GetComponent<playerListItem>().setUp(players[i]);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ErrorText.text = "Room create failed: " + message;
        Debug.Log("Room create failed: " + message);
        MenuSystem.MenuInstance.openMenu("Error");
    }

    public void leaveRoom()
    {
        Debug.Log("Leaving...");
        PhotonNetwork.LeaveRoom();
        MenuSystem.MenuInstance.openMenu("Loading");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Leaved Room");
        MenuSystem.MenuInstance.openMenu("Lobby");

        for (int i = 0; i < PlayerListContainer.childCount; i++)
        {
            Destroy(PlayerListContainer.GetChild(i).gameObject);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in RoomContainer)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(RoomButtonPrefabs, RoomContainer).GetComponent<RoomListButtons>().setUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerNamePrefabs, PlayerListContainer).GetComponent<playerListItem>().setUp(newPlayer);
    }
}
