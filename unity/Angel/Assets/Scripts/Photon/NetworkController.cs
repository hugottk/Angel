﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connect to Photon master servers
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server " + PhotonNetwork.CloudRegion);
    }
}
