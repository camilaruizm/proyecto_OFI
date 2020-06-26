using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject quickStartButton = null;
    [SerializeField] private GameObject quickCancelButton = null;
    [SerializeField] private int roomSize = 2;
    private string[] maps = new string[4] {"ToposMultiplayer", "DardosMultiplayer", "BeerPongMultiplayer", "PecesMultiplayer"};
    public int selectedGame = 0;
    private bool entering = false;

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && !PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        } else
        {
            quickStartButton.SetActive(false);
        }
    }
    // Start is called before the first frame update
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        quickStartButton.SetActive(true);
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void QuickStart()
    {
        if (!entering)
        {
            //quickStartButton.SetActive(false);
            quickCancelButton.SetActive(true);
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("QuickStart");
            entering = true;
        }
    }

    public void StartGameMode(int gamemode)
    {
        if (!entering)
        {
            selectedGame = gamemode;
            //quickStartButton.SetActive(false);
            quickCancelButton.SetActive(true);
            Debug.Log("Cuartos disponibles " + PhotonNetwork.CountOfRooms + " " + PhotonNetwork.GetCustomRoomList(TypedLobby.Default, "SELECTEDGAME"));
            Debug.Log(maps[selectedGame]);
            ExitGames.Client.Photon.Hashtable customParams = new ExitGames.Client.Photon.Hashtable { { "SELECTEDGAME", maps[selectedGame] } };
            Debug.Log("StartGamemode  " + customParams);
            PhotonNetwork.JoinRandomRoom(customParams, 0);
            entering = true;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //Debug.LogErrorFormat("Join Random Failed with error code {0} and error message {1}", returnCode, message);
        CreateRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo room in roomList)
        {
            Debug.Log("Room " + room.Name + " Params: " + room.CustomProperties);
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Entered Lobby");
    }

    public void CreateRoom()
    {
        Debug.Log("Creating room now");
        int randomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize};
        roomOps.CustomRoomPropertiesForLobby = new string[] { "SELECTEDGAME", maps[selectedGame] };
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { "SELECTEDGAME", maps[selectedGame] } };
        PhotonNetwork.CreateRoom("Room " + randomNumber, roomOps);
        Debug.Log("Created room " + randomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Create room failed... trying again");
        CreateRoom();
    }

    public void QuickCancel()
    {
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.Disconnect();
    }

}
