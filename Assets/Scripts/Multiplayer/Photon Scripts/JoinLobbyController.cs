using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject lobbyName;
    [SerializeField]
    private GameObject joinLobbyButton;
    [SerializeField]
    private GameObject leaveLobbyButton;
    [SerializeField]
    private Text userMessage;
    [SerializeField]
    private GameObject userName;

    public void JoinLobby()
    {
        string text = lobbyName.GetComponent<TMP_InputField>().text; //used to get the text from an input field of a TextMeshPro Object
        if (string.IsNullOrWhiteSpace(text))
        {
            userMessage.text = "You must enter a room number.";
        }
        else
        {
            leaveLobbyButton.SetActive(true);
            joinLobbyButton.SetActive(false);
            PhotonNetwork.NickName = userName.GetComponent<TMP_InputField>().text; // sets local users name before connecting to a room
            PhotonNetwork.JoinRoom(text);
        }
        
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        userMessage.text = "Room does not exist or is closed.";
    }

    public override void OnJoinedRoom()
    {
        userMessage.text = "Lobby joined!";
        
    }

    public void LeaveLobby()
    {
        userMessage.text = "";
        lobbyName.GetComponent<TMP_InputField>().text = "";
        PhotonNetwork.LeaveRoom();
        leaveLobbyButton.SetActive(false);
        joinLobbyButton.SetActive(true);
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
