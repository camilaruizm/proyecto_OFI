using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        StartGame();
    }

    void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting Game");
            ExitGames.Client.Photon.Hashtable RoomCustomProps = PhotonNetwork.CurrentRoom.CustomProperties;
            Debug.Log("Cargando escena: " + RoomCustomProps);
            PhotonNetwork.LoadLevel(""+ RoomCustomProps["SELECTEDGAME"]);
        }
    }
}
