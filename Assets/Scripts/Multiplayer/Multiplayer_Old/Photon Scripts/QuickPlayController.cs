using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuickPlayController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject joinRandomLobbyButton;
    [SerializeField]
    private GameObject cancelLobbySearchButton;
    [SerializeField]
    private Text userMessage;
    [SerializeField]
    private GameObject userName;
    private int numPlayers;
    readonly int MAX_ROOM_VALUE = 10000;



    public override void OnConnectedToMaster()
    {
        Debug.Log("QuickPlayController_ Override_OnConnectedToMaster.CS");
        PhotonNetwork.AutomaticallySyncScene = true;
        joinRandomLobbyButton.SetActive(true);
    }

    public void QuickPlay()
    {//when the join quickplay button is pressed for the frist time on QP MENU
        Debug.Log("QuickPlayController.CS _ QuickPlay()  RefCode 208816");
        userMessage.text = userMessage.text + "Searching available rooms...";
        joinRandomLobbyButton.SetActive(false);
        cancelLobbySearchButton.SetActive(true);
        PhotonNetwork.NickName = userName.GetComponent<TMP_InputField>().text; // sets local users name before connecting to a room
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //This is called when a player trys to join a quick play lobby, and none was available Thus, infrom the user, and create a lobby for another quick player to join.
        //if they wish to cancel the hosted lobby, they can press the button
        Debug.Log("QuickPlayController.CS _ Override OnJoinRandomFailed()  RefCode 2218816");
        userMessage.text = userMessage.text + "\nNo rooms available";
        CreateLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("QuickPlayController.CS _ Override OnJoinedRoom()  RefCode 3912916");
        userMessage.text = "Lobby joined! Room: " + PhotonNetwork.CurrentRoom.Name;
    }

    void CreateLobby() //create your own lobby
    {
        
        userMessage.text = userMessage.text + "\nCreating a new Lobby";
        Debug.Log("QuickPlayController.CS _ CreateLobby()  RefCode 001");
        int randomRoomNumber = Random.Range(0, MAX_ROOM_VALUE); //initialize a random room number
        RoomOptions roomOps = new RoomOptions(){ IsVisible = true, IsOpen = true, MaxPlayers = (byte)numPlayers};
        PhotonNetwork.CreateRoom(randomRoomNumber.ToString(), roomOps);
        userMessage.text = userMessage.text + "\nJoined Room: " + randomRoomNumber;



    }


    public override void OnCreateRoomFailed(short returnCode, string message) //called if a lobby name is taken
    {
        Debug.Log("QuickPlayController.CS _ Override OnCreateRoomFailed()  RefCode 32918779");
        userMessage.text = userMessage.text + "\nFailed to create lobby... trying again\n" + message;
        CreateLobby(); //looping this call until a random room name is not already taken
    }

    public void CancelLobbySearch() //cancel room search
    {
        Debug.Log("QuickPlayController.CS _ CancelLobbySearch()  RefCode 3912779");
        userMessage.text = "";
        cancelLobbySearchButton.SetActive(false);
        joinRandomLobbyButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    void Start() // Start is called before the first frame update
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }
}
