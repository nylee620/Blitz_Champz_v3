using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateLobbyController : MonoBehaviourPunCallbacks
{ 
    [SerializeField]
    private GameObject createLobbyButton;
    [SerializeField]
    private GameObject closeLobbyButton;
    [SerializeField]
    private Text userMessage;
    [SerializeField]
    private Text roomId;
    [SerializeField]
    private Toggle isPublic;
    [SerializeField]
    private GameObject userName;
    [SerializeField]
    private int numPlayers;
    readonly int MAX_ROOM_VALUE = 10000;

    public void CreateLobby() //create your own lobby
    {
        PhotonNetwork.NickName = userName.GetComponent<TMP_InputField>().text; // sets local users name before connecting to a room
        userMessage.text = "Creating a new Lobby";
        int randomRoomNumber = Random.Range(0, MAX_ROOM_VALUE); //initialize a random room number
        RoomOptions roomOps = new RoomOptions() { IsVisible = isPublic.isOn, IsOpen = true, MaxPlayers = (byte)numPlayers }; //if match is public isVisible is true, allowing others to connect to this room
        PhotonNetwork.CreateRoom(randomRoomNumber.ToString(), roomOps);
        roomId.text = randomRoomNumber.ToString();
    }

    public void CloseLobby()
    {
        userMessage.text = "";
        roomId.text = "";
        PhotonNetwork.LeaveRoom();
        createLobbyButton.SetActive(true);
        closeLobbyButton.SetActive(false);
    }


    public override void OnJoinedRoom()
    {
        userMessage.text = "Lobby Joined. Waiting on another player to join.";
        createLobbyButton.SetActive(false);
        closeLobbyButton.SetActive(true);
    }

    public override void OnCreatedRoom()
    {
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        userMessage.text = newPlayer.NickName + " has joined!";
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
