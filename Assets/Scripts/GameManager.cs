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
    public bool myTurn = false;

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

        if (GameLogic.Instance.miID == GameLogic.Instance.playingPlayer)
        {
            foreach (GameObject hoyo in GameObject.FindGameObjectsWithTag("Hoyo"))
            {
                hoyo.GetComponent<HoleBehavior>().hasMole = false;
            }

            myTurn = true;
        }
        else
        {
            myTurn = false;
        }
        Debug.Log("Mi turno " + myTurn + " " + GameLogic.Instance.miID + " " + GameLogic.Instance.playingPlayer);
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
