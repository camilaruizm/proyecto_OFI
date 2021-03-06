﻿using Photon.Pun;
using UnityEngine;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;

    private int[] puntajeRondaJugador = null;
    private int[] rondasGanadas = null;

    #region SINGLETON PATTERN
    public static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("ScoreManager");
                    _instance = container.AddComponent<ScoreManager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
    }

    public void GiveScore(int player, int score)
    {
        myPhotonView.RPC("GiveScoreRPC", RpcTarget.All, player, score);
    }

    [PunRPC]
    private void GiveScoreRPC(int player, int score)
    {
        puntajeRondaJugador[player] += score;
        UITurnManager.Instance.ChangeScore(player, puntajeRondaJugador[player]);
    }

    public int[] GetPlayerScoreList()
    {
        return puntajeRondaJugador;
    }

    public void SetPlayerNumber(int playerNumber)
    {
        puntajeRondaJugador = new int[playerNumber];
        rondasGanadas = new int[playerNumber];
    }

    public void RestoreRonda()
    {
        for (int j = 0; j < puntajeRondaJugador.Length; j++)
        {
            puntajeRondaJugador[j] = 0;
            UITurnManager.Instance.ChangeScore(j, 0);
        }
    }

    public void ScoreResumen()
    {
        int winningPlayer = 0;
        int mayorPuntaje = 0;
        for (int i = 0; i < puntajeRondaJugador.Length; i++)
        {
            if (puntajeRondaJugador[i] > mayorPuntaje)
            {
                winningPlayer = i;
                mayorPuntaje = puntajeRondaJugador[i];
            }
        }

        rondasGanadas[winningPlayer] += 1;
        UITurnManager.Instance.RondasGanadasPlayer(winningPlayer, rondasGanadas[winningPlayer]);
        UITurnManager.Instance.TurnRoundWinnerInterface(true, winningPlayer + 1);
    }

    public void GameEnds()
    {
        int winningPlayer = 0;
        int mayorPuntaje = 0;
        for (int i = 0; i < rondasGanadas.Length; i++)
        {
            if (rondasGanadas[i] > mayorPuntaje)
            {
                winningPlayer = i;
                mayorPuntaje = rondasGanadas[i];
            }
        }
        UITurnManager.Instance.TurnWinnerInterface(true, winningPlayer + 1);
        UITurnManager.Instance.GameStatus("Juego Terminado");
    }
}
