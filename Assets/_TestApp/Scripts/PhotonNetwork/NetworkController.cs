﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to " + PhotonNetwork.CloudRegion + " server!");
        
    }
}