using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject localPlayerName;
    [SerializeField]
    private GameObject remotePlayerName;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values) // adds players names to the new scene.
        {
            if (player.IsLocal)
            {
                localPlayerName.GetComponent<Text>().text = player.NickName;
            }
            else
            {
                remotePlayerName.GetComponent<Text>().text = player.NickName;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
