using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class InputManager : MonoBehaviour
{
    public GameObject corona;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.instance.myTurn)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Topo")
                    {
                        GameObject topo = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", corona.name), hit.point, Quaternion.identity);
                    }
                }
            }
        }
    }
}
