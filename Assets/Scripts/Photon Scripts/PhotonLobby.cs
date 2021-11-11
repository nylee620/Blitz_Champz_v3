using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    public GameObject joinRoomButton;
    public GameObject cancelJoinButton;

    private void Awake()
    {
        lobby = this; //Creates the singleton, lives within the Main menu scene
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        joinRoomButton.SetActive(true);
    }

    public void OnJoinButtonClicked()
    {
        joinRoomButton.SetActive(false);
        cancelJoinButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError("Tried to join a random game but failed. There must be no open game available");
        int randomRoomNumber = Random.Range(0, 10000); //initialize a random room number
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 }; //if match is public isVisible is true, allowing others to connect to this room
        PhotonNetwork.CreateRoom(randomRoomNumber.ToString(), roomOps);
    }

    void CreateRoom()
    {
        int randomRoomNumber = Random.Range(0, 10000); //initialize a random room number
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 }; //if match is public isVisible is true, allowing others to connect to this room
        PhotonNetwork.CreateRoom(randomRoomNumber.ToString(), roomOps);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("We are now in a room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Tried to join a random game but failed. There must be no open game available");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        joinRoomButton.SetActive(true);
        cancelJoinButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
}
