using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate("Player", spawnPoint.transform.position, Quaternion.identity);
    }
}