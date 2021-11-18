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
        //Mh- my fix code for pun error on while connected
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

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //    public override void OnDisconnected(DisconnectCause cause)
        connectedImage.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        connectedText.GetComponent<Text>().text = "Disconnected";

    }





    void Update()
    {//this is called everyframe on the mainmenu, so don't overload it :) 

    }
}