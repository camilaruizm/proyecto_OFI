using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;
public class BeerGameLogic : MonoBehaviourPunCallbacks
{

    #region SINGLETON PATTERN
    public static BeerGameLogic _instance;
    public static BeerGameLogic Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BeerGameLogic>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("BeerGameLogic");
                    _instance = container.AddComponent<BeerGameLogic>();
                }
            }

            return _instance;
        }
    }
    #endregion

    private PhotonView myPhotonView;
    [SerializeField] private Transform myPlayer = null;
    public Transform[] playerPos = null;
    public int playingPlayer = 0;
    public bool myTurn = false;
    public bool gameEnded = false;
    public int pointsToWin = 7;

    [HideInInspector] public int miID = 0;

    [SerializeField] private UnityEvent startGameEvent = new UnityEvent();
    [SerializeField] private UnityEvent cambiarTurnos = new UnityEvent();
    [SerializeField] private UnityEvent endGameEvent = new UnityEvent();


    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        UITurnManager.Instance.GameStatus("Waiting for Players");
    }

    public void IniciarJuego()
    {
        miID = GameSetupController.Instance.myId;
        playingPlayer = 0;
        //Cambia Turnos
        UITurnManager.Instance.YourPlayer(miID + 1);
        UITurnManager.Instance.GameStarts();
        UITurnManager.Instance.GameStatus("Jugando");
        if (miID == playingPlayer)
        {
            //Acción del jugador 1
            myTurn = true;
            myPlayer.position = playerPos[0].position;
            myPlayer.rotation = playerPos[0].rotation;
        }
        else
        {
            //Acción del jugador 2
            myTurn = false;
            myPlayer.position = playerPos[1].position;
            myPlayer.rotation = playerPos[1].rotation;
        }
        UITurnManager.Instance.TurnoPlayer(playingPlayer + 1);
        ScoreManager.Instance.SetPlayerNumber(PhotonNetwork.PlayerList.Length);
        UITurnManager.Instance.TurnScoreInterface(true);
        Debug.Log("Inicia el juego");
        startGameEvent.Invoke();
    }

    public void TerminarTurno()
    {
        StartCoroutine(TurnoWait());
    }

    IEnumerator TurnoWait()
    {
        yield return new WaitForSeconds(1f);
        myPhotonView.RPC("TerminarTurnoRPC", RpcTarget.All);
    }


    [PunRPC]
    private void TerminarTurnoRPC()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int winningPlayer = 0;
            int mayorPuntaje = 0;
            int[] scoreList = ScoreManager.Instance.GetPlayerScoreList();

            for (int i = 0; i < scoreList.Length; i++)
            {
                if (scoreList[i] > mayorPuntaje)
                {
                    winningPlayer = i;
                    mayorPuntaje = scoreList[i];
                }
            }

            if (mayorPuntaje >= pointsToWin)
            {
                AcabarJuego(winningPlayer);
            } else
            {
                playingPlayer += 1;
                if (playingPlayer > PhotonNetwork.PlayerList.Length - 1)
                {
                    playingPlayer = 0;
                }

                myPhotonView.RPC("NextPlayer", RpcTarget.All, playingPlayer);
            }
        }
        Debug.Log("Siguiente jugador es: " + playingPlayer);
    }

    [PunRPC]
    private void NextPlayer(int playerNumber)
    {
        playingPlayer = playerNumber;
        UITurnManager.Instance.TurnoPlayer(playingPlayer + 1);
        if (miID == playingPlayer)
        {
            myTurn = true;
        }
        else
        {
            myTurn = false;
        }
        cambiarTurnos.Invoke();
    }

    public void AcabarJuego(int winningPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("JuegoAcaba", RpcTarget.All, winningPlayer);
        }
    }

    [PunRPC]
    private void JuegoAcaba(int winningPlayer)
    {
        UITurnManager.Instance.GameStatus("Gana el jugador " + (winningPlayer+1));
        UITurnManager.Instance.GameEnds();
        endGameEvent.Invoke();
        gameEnded = true;
    }

}
