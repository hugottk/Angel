using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStartLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject quickStartButton;

    [SerializeField] private GameObject quickCancelButton;

    [SerializeField] private int RoomSize;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        quickStartButton.SetActive(true);
    }

    public void QuickStart() //Paired to the Quick Start button
    {
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom(); //First tries to join an existing room
        Debug.Log("Quick start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    void CreateRoom() //trying to create our own room
    {
        Debug.Log("Creating room");
        int randomRoomNumber = Random.Range(0, 10000); //Creating a random number name for the room
        RoomOptions roomOps = new RoomOptions() {IsVisible =  true, IsOpen = true, MaxPlayers = (byte)RoomSize};
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps); //Attempting to create a new room
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom(); //Retrying to create a room with a different name
    }

    public void QuickCancel()
    {
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
