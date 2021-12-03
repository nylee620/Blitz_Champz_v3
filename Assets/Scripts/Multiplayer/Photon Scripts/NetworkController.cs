using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class NetworkController : MonoBehaviourPunCallbacks
{
    //    Photon.PunBehaviour
    //      MonoBehaviourPunCallbacks
    /**********************************
    Documentation: https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
    Scripting API: https://doc-api.photonengine.com/en/pun/v2/index.html
    **********************************/
    [SerializeField]
    public GameObject connectedText;
    [SerializeField]
    public GameObject connectedImage;
    [SerializeField]
    public bool ConnectedUsingSettings;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("NetworkController_Start.CS");
        ConnectedUsingSettings = PhotonNetwork.ConnectUsingSettings();

        print(PhotonNetwork.CountOfPlayersOnMaster + ": Start Amount of Users In PUN Lobby");

        if (ConnectedUsingSettings == false)
        {
            //every connection after the first time connection (Used to handle the already connected status)
            Debug.Log("Disconnecting from pun");
            PhotonNetwork.Disconnect();
            connectedText.GetComponent<Text>().text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings(); //connect to Photon master servers
        }
        else
        {
            //frist time connection
            connectedText.GetComponent<Text>().text = "Connecting...";
        }





    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("NetworkController_OnConnectedToMaster.CS");
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        connectedImage.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        connectedText.GetComponent<Text>().text = "Connected"; //final output

//        TextMeshProUGUI tserv = GameObject.FindWithTag("server").GetComponent<TextMeshProUGUI>();
//        tserv.color = new Color32(0, 255, 0, 255);
//
//        tserv.text = "" + PhotonNetwork.CloudRegion;

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //    public override void OnDisconnected(DisconnectCause cause)
        connectedImage.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        connectedText.GetComponent<Text>().text = "Disconnected";

    }


    public void ResetPunConnection()
    {
//        TextMeshProUGUI tserv = GameObject.FindWithTag("server").GetComponent<TextMeshProUGUI>();
//        tserv.text = "Locating..";
//        tserv.color = new Color32(255, 0, 0, 255);
        Debug.Log("NetworkController_ResetPunConnection.CS");
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();
    }



    void Update()
    {//this is called everyframe on the mainmenu, so don't overload it :) 

        //update connected pun user count
//        TextMeshProUGUI t = GameObject.FindWithTag("punlobby").GetComponent<TextMeshProUGUI>();
//        TextMeshProUGUI g = GameObject.FindWithTag("publicgamecount").GetComponent<TextMeshProUGUI>();
//        t.text = "" + PhotonNetwork.CountOfPlayers;
//        t.color = new Color32(0, 255, 0, 255);

        //overall pun user count
        //update hosted games that are not yet filled
        //g.text = "" + PhotonNetwork.CountOfRooms; //rooms not yet filled
        //g.text = "" + PhotonNetwork.CountOfRooms;

        //if game is public, and not yet 2 players, and is being hosted, is it available via quickplay
        //get a public room, get the player count. If != 2 then add it to the number
        //      RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 }; //if match is public isVisible is true, allowing others to connect to this room

        //  if (RoomOptions.roomOps.isVisable == true){

        //  }
        //

    }
}