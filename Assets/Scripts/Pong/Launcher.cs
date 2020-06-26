using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Launcher : MonoBehaviour
{
    public GameObject proyectilPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button. Beer " + BeerGameLogic.Instance.myTurn + " " + BeerGameLogic.Instance.gameEnded);
            if (BeerGameLogic.Instance.myTurn == true && !BeerGameLogic.Instance.gameEnded)
            {
                Debug.Log("Pressed primary button.");
                GameObject ball = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Ball"), transform.position, Quaternion.identity);
                ball.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                BeerGameLogic.Instance.myTurn = false;
            }
        }
    }
}
