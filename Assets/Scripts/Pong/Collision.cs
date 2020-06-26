using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Collision : MonoBehaviour
{
    public int scorePoints = 1;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            if (collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                GetComponent<PhotonView>().RPC("Touched", RpcTarget.All);
                if (gameObject.transform.parent.gameObject.tag == "VasoA")
                {
                    ScoreManager.Instance.GiveScore(1, scorePoints);
                    Debug.Log("Punto jugador 2");
                }
                else if (gameObject.transform.parent.gameObject.tag == "VasoB")
                {
                    ScoreManager.Instance.GiveScore(0, scorePoints);
                    Debug.Log("Punto jugador 1");
                }
                BeerGameLogic.Instance.TerminarTurno();
                PhotonNetwork.Destroy(collider.gameObject);
            }
        }
    }

    [PunRPC]
    private void Touched()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
