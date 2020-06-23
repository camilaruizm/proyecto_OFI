using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //Timer
    int playTime = 60;
    int seconds,minutes;
    [HideInInspector] public bool myTurn = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    public void IniciaJuego()
    {

    }

    public void CambiarTurnos()
    {

    }

    public void ComienzaRondaJugador()
    {
        if (GameLogic.Instance.playingPlayer == GameLogic.Instance.miID)
        {
            myTurn = true;
        } else
        {
            myTurn = false;
        }
    }

    public void TerminaRondaJugador()
    {
        foreach (GameObject topo in GameObject.FindGameObjectsWithTag("Topo"))
        {
            if (topo.GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(topo);
            }
        }
        myTurn = false;

    }

    public void PantallaResumen()
    {

    }

    public void TerminaJuego()
    {

    }
}
