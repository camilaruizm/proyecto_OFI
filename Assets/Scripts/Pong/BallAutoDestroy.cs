using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallAutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(4);
        if (gameObject.GetComponent<PhotonView>().IsMine)
        {
            BeerGameLogic.Instance.TerminarTurno();

            PhotonNetwork.Destroy(gameObject);
        }
    }
}
