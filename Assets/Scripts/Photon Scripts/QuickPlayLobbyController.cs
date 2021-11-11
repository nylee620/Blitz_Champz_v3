using Photon.Pun;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;

public class QuickPlayLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerScenceIndex; //this is the build index of the scene

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        StartGame();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        StartGame();
    }

    private void StartGame()
    {
        // TODO: Fill with our load scripts
        if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount){ //if the room is full
            Debug.Log("Starting Game");
            PhotonNetwork.LoadLevel(multiplayerScenceIndex); //since autosyncscene is set to true all players are loaded into the scene
        }
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
