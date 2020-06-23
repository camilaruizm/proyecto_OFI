using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.SceneManagement;

public class GameSetupController : PhotonSingleton<GameSetupController>
{
    // Start is called before the first frame update
    private PhotonView myPhotonView;
    private bool readyToStart = false;
    public int myId = 0;
    void Awake()
    {
        myPhotonView = GetComponent<PhotonView>();

        if (PhotonNetwork.IsMasterClient)
        {
            myId = 0;
        }
        else
        {
            myId = 1;
        }

        PlayerCountUpdate();
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        Debug.Log("Creating Player");
        GameObject newPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
        newPlayer.transform.SetParent(Camera.main.transform);
        newPlayer.transform.localPosition = Vector3.zero;
        newPlayer.transform.localRotation = Quaternion.identity;
        foreach (MeshRenderer mesh in newPlayer.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.enabled = false;
        }
    }

    public void PlayerCountUpdate()
    {
        int playerCount = PhotonNetwork.PlayerList.Length;
        int roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        if (playerCount == roomSize)
        {
            readyToStart = true;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
        else
        {
            readyToStart = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();
        if (PhotonNetwork.IsMasterClient)
        {
            if (readyToStart)
            {
                myPhotonView.RPC("RPC_StartGame", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void RPC_StartGame()
    {
        GameLogic.Instance.IniciarJuego();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (!GameLogic.Instance.gameEnded)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public void Salir()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
}
