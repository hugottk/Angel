using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    [SerializeField] public int mainMenuIndex;
    // Start is called before the first frame update
    public void LeaveGame()
    {
        Debug.Log("Leaving the room");
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(mainMenuIndex);
    }
}
