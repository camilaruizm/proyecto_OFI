﻿using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;
public class GameLogic : PhotonSingleton<GameLogic>
{
    private PhotonView myPhotonView;
    [SerializeField] private Transform myPlayer = null;
    public Transform lugarDeEspera = null;
    public Transform lugarDeJuego = null;
    public int playingPlayer = 0;
    private int rondaActual = 0;
    [SerializeField] private int maxRondas = 5;
    public bool gameEnded = false;
    [HideInInspector] public int miID = 0;
    [Space]
    public int tiempoRonda = 30;
    public int tiempoPantallaResumen = 10;
    public int tiempoPreparacion = 3;

    [SerializeField] private UnityEvent startGameEvent = new UnityEvent();
    [SerializeField] private UnityEvent cambiarTurnos = new UnityEvent();
    [SerializeField] private UnityEvent comienzaRondaJugador = new UnityEvent();
    [SerializeField] private UnityEvent terminaRondaJugador = new UnityEvent();
    [SerializeField] private UnityEvent pantallaResumen = new UnityEvent();
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
        if (miID == playingPlayer)
        {
            //Acción del jugador 1
            myPlayer.position = lugarDeJuego.position;
        }
        else
        {
            //Acción del jugador 2
            myPlayer.position = lugarDeEspera.position;
        }
        UITurnManager.Instance.RondaActual(rondaActual + 1);
        UITurnManager.Instance.TurnoPlayer(playingPlayer + 1);
        ScoreManager.Instance.SetPlayerNumber(PhotonNetwork.PlayerList.Length);
        UITurnManager.Instance.TurnScoreInterface(true);
        StartCoroutine(Contador());
        startGameEvent.Invoke();
    }

    [PunRPC]
    private void CambiarTurnos(int playPlay)
    {
        playingPlayer = playPlay;
        UITurnManager.Instance.TurnRoundWinnerInterface(false, 0);
        UITurnManager.Instance.GameStatus("Jugando");
        UITurnManager.Instance.RondaActual(rondaActual + 1);
        UITurnManager.Instance.TurnoPlayer(playingPlayer + 1);
        //Cambia Turnos
        if (miID == playingPlayer)
        {
            //Acción del jugador 1
            myPlayer.position = lugarDeJuego.position;
        }
        else
        {
            //Acción del jugador 2
            myPlayer.position = lugarDeEspera.position;
        }
        cambiarTurnos.Invoke();
        StartCoroutine(Contador());
    }

    IEnumerator Contador()
    {
        yield return new WaitForSeconds(tiempoPreparacion);
        int counter = tiempoRonda;
        comienzaRondaJugador.Invoke();
        while (counter > 0)
        {
            //Comienza a contar
            yield return new WaitForSeconds(1);
            counter -= 1;
        }
        terminaRondaJugador.Invoke();
        playingPlayer += 1;
        if (playingPlayer > PhotonNetwork.PlayerList.Length - 1)
        {
            playingPlayer = 0;
            if (PhotonNetwork.IsMasterClient)
            {
                myPhotonView.RPC("PantallaResumen", RpcTarget.All);
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                myPhotonView.RPC("CambiarTurnos", RpcTarget.All, playingPlayer);
            }
        }
    }

    [PunRPC]
    private void PantallaResumen()
    {
        pantallaResumen.Invoke();
        StartCoroutine(Resumen());
    }
    IEnumerator Resumen()
    {
        UITurnManager.Instance.GameStatus("Resumen de Ronda");
        ScoreManager.Instance.ScoreResumen();
        int counter = tiempoPantallaResumen;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter -= 1;
        }
        ScoreManager.Instance.RestoreRonda();
        rondaActual += 1;
        if (rondaActual > maxRondas)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                myPhotonView.RPC("JuegoAcaba", RpcTarget.All);
            }
            yield break;
        } else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                myPhotonView.RPC("CambiarTurnos", RpcTarget.All, playingPlayer);
            }
        }
    }

    [PunRPC]
    private void JuegoAcaba()
    {
        ScoreManager.Instance.GameEnds();
        UITurnManager.Instance.GameEnds();
        endGameEvent.Invoke();
        gameEnded = true;
    }

}
