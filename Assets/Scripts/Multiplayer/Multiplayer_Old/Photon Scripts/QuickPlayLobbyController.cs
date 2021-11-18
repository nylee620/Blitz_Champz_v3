using Photon.Pun;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class QuickPlayLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerScenceIndex; //this is the build index of the scene




    public override void OnEnable()
    {
        Debug.Log("QuickPlayLobbyController.CS _ Override  OnEnable()  RefCode 3937");

        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        Debug.Log("QuickPlayLobbyController.CS _ Override  OnDisable()  RefCode 3938");

        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        
        
        Debug.Log("QuickPlayLobbyController.CS _ Override  OnJoinedRoom()  RefCode 3938438");
        //Debug.Log("Joined Room");
        StartGame();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        //When a quickplayer Hosted lobby, encounters a 2ndplayer that joins that hosted lobby

        //This is when another player, enters a lobby that was created by another player
        Debug.Log("QuickPlayLobbyController.CS _ Override OnPlayerEnteredRoom()  RefCode 0011i2837");

        StartGame();
    }

    private void StartGame()
    {

        Debug.Log("QuickPlayLobbyController.CS _   StartGame()  RefCode 39308");

        // TODO: Fill with our load scripts
        if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount){ //if the room is full
            Debug.Log("QuickPlayLobbyController.CS _   StartGame() _ MaxPlayers has been achieved RefCode 39308");
            Debug.Log("Starting Game");
            //PhotonNetwork.LoadLevel("Game"); //since autosyncscene is set to true all players are loaded into the scene
            PhotonNetwork.LoadLevel("multi"); //since autosyncscene is set to true all players are loaded into the scene


        }



    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("QuickPlayLobbyController.CS _   Start()  RefCode 393");

    }

    // Update is called once per frame
    void Update()
    {
    //this seems to be called every instance 

    }
}
