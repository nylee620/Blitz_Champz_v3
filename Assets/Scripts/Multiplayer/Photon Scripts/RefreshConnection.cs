using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{
    //NetworkController connectedText;

    public void Onclick()
    {
        //When refresh button clicked, execute this stuff - mh
        Debug.Log("RefreshConnection_OnClick.CS");

/*
        connectedText.GetComponent<Text>().text = "Refreshing";

        //first disconnect
        PhotonNetwork.Disconnect();
        

        //second reconnect
        if (PhotonNetwork.ConnectUsingSettings() == false)
        {//if connection fails
           connectedText.GetComponent<Text>().text = "Oh no lol";
        }
        else
        {//connection passed
            connectedText.GetComponent<Text>().text = "Connected";
        }
*/
    }


}
