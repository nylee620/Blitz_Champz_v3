using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject controlPanel;

    [SerializeField]
    private Text feedbackText;

    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    bool isConnecting;

    string gameVersion = "1";

    [Space(10)]
    [Header("Custom Variables")]
    public InputField playerNameField;
    public InputField roomNameField;

    [Space(5)]
    public Text playerStatus;
    public Text connectionStatus;

    [Space(5)]
    public GameObject roomJoinUI;
    public GameObject buttonLoadArena;
    public GameObject buttonJoinRoom;

    public string playerName = "";
    public string roomName = "";
    void Start()
    {
        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.Disconnect();
        }
        PlayerPrefs.DeleteAll();

        Debug.Log("Connecting to Photon Network");

        //2
        roomJoinUI.SetActive(false);
        buttonLoadArena.SetActive(false);

        //3
        ConnectToPhoton();
    }

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void SetPlayerName()
    {
        playerName = playerNameField.text;
        Debug.Log("Changed player name" + name);
    }

    public void SetRoomName()
    {
		roomName = roomNameField.text;
        Debug.Log("Changed room name" + name);
    }
    
    // Tutorial Methods
    void ConnectToPhoton()
    {
        connectionStatus.text = "Connecting...";
        PhotonNetwork.GameVersion = gameVersion; //1
        PhotonNetwork.ConnectUsingSettings(); //2
    }

    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = playerName; //1
            Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " + roomNameField.text);
            RoomOptions roomOptions = new RoomOptions(); //2
            TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default); //3
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby); //4
        }
    }
    public void JoinRandom() {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            Debug.Log("Trying to join random lobby");
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public void LoadArena()
    {
        // 5
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonNetwork.LoadLevel("BlitzChampzGame");
        }
        else
        {
            playerStatus.color = Color.red;
            playerStatus.text = "Minimum 2 Players \nrequired to Load Arena!";
        }
    }

    // Photon Methods
    public override void OnConnected()
    {
        // 1
        base.OnConnected();
        // 2
        connectionStatus.text = "Connected!";
        connectionStatus.color = Color.green;
        roomJoinUI.SetActive(true);
        buttonLoadArena.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // 3
        isConnecting = false;
        controlPanel.SetActive(true);
        Debug.LogError("Disconnected. Please check your Internet connection.");
    }

    public override void OnJoinedRoom()
    {
        // 4
        if (PhotonNetwork.IsMasterClient)
        {
            buttonLoadArena.SetActive(true);
            buttonJoinRoom.SetActive(false);
            playerStatus.color = Color.blue;
            playerStatus.text = "You are Lobby Leader";
        }
        else
        {
            playerStatus.text = "Connected to Lobby";
        }
    }
}