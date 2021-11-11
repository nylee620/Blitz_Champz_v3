using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    /**********************************
    Documentation: https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
    Scripting API: https://doc-api.photonengine.com/en/pun/v2/index.html
    **********************************/
    [SerializeField]
    private GameObject connectedText;
    [SerializeField]
    private GameObject connectedImage;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //connect to Photon master servers
        connectedText.GetComponent<Text>().text = "Connecting...";
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        connectedImage.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        connectedText.GetComponent<Text>().text = "Connected";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectedImage.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        connectedText.GetComponent<Text>().text = "Disconnected";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
