using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//this is source
public class GameTable : MonoBehaviourPun
{

    public DeckOfCard draw_deck;
    public List<GameObject> discard;
    public Offensive_Card last_card;
    public int player_count;
    public BlitzPlayer pov_player;
    public BlitzPlayer blitzPlayer1;
    public BlitzPlayer blitzPlayer2;
    public BlitzPlayer blitzPlayer3;
    public BlitzPlayer blitzPlayer4;
    public Text p1;
    public Text p2;
    public Text p3;
    public Text p4;
    public BlitzPlayer currentBlitzPlayer;
    public GameObject gameOver;
    public GameObject gameVic;
    private bool reversed = false;
    public bool ready = true;
    public LinkedListNode<BlitzPlayer> currentNode;
    public LinkedList<BlitzPlayer> listBlitzPlayers = new LinkedList<BlitzPlayer>();

    void Start()
    {

        bool IdontHavePhotonPlayerAndIamMasterClient = PhotonNetwork.IsMasterClient;

        if (IdontHavePhotonPlayerAndIamMasterClient)
        {
            Debug.Log("B");

            LinkedList<Photon.Realtime.Player> listPhotonPlayers = new LinkedList<Photon.Realtime.Player>();

            foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            {
                listPhotonPlayers.AddLast(p);
            }

            LinkedListNode<Photon.Realtime.Player> photonPlayerNode = listPhotonPlayers.First;

            for (int a = 0; a < listPhotonPlayers.Count; a++)
            {
                Debug.Log("A");
                if (photonPlayerNode.Value == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("This player equals this player");
                    GameObject instancedPhotonPlayer = PhotonNetwork.Instantiate("player", new Vector3(-8f, -4.4f, 1f), transform.rotation);
                    blitzPlayer1 = instancedPhotonPlayer.GetComponent<BlitzPlayer>();
                    listBlitzPlayers.AddLast(blitzPlayer1);
                    blitzPlayer1.table = this;
                    photonPlayerNode = photonPlayerNode.Next ?? photonPlayerNode.List.First;
                    for (int b = 0; b < listPhotonPlayers.Count - 1; b++)
                    {
                        Debug.Log(b);
                        Debug.Log("C");
                        if (b == 0)
                        {
                            Debug.Log("blitzPlayer2 obj - ref 9912894781");
                            GameObject player2_object = PhotonNetwork.Instantiate("player", new Vector3(-8f, 3.4f, 1f), Quaternion.Euler(0, 0, 180f));
                            blitzPlayer2 = player2_object.GetComponent<BlitzPlayer>();
                            listBlitzPlayers.AddLast(blitzPlayer2);
                            blitzPlayer2.table = this;
                            player2_object.GetComponent<PhotonView>().TransferOwnership(photonPlayerNode.Value);
                            photonPlayerNode = photonPlayerNode.Next ?? photonPlayerNode.List.First;
                        }
                        else if (b == 1)
                        {
                            GameObject player3_object = PhotonNetwork.Instantiate("player", new Vector3(8f, 3.4f, 1f), Quaternion.Euler(0, 0, 180f));
                            blitzPlayer3 = player3_object.GetComponent<BlitzPlayer>();
                            listBlitzPlayers.AddLast(blitzPlayer3);
                            blitzPlayer3.table = this;
                            player3_object.GetComponent<PhotonView>().TransferOwnership(photonPlayerNode.Value);
                            photonPlayerNode = photonPlayerNode.Next ?? photonPlayerNode.List.First;
                        }
                        else if (b == 2)
                        {
                            GameObject player4_object = PhotonNetwork.Instantiate("player", new Vector3(8f, -4.4f, 1f), transform.rotation);
                            blitzPlayer4 = player4_object.GetComponent<BlitzPlayer>();
                            listBlitzPlayers.AddLast(blitzPlayer4);
                            blitzPlayer4.table = this;
                            player4_object.GetComponent<PhotonView>().TransferOwnership(photonPlayerNode.Value);
                            photonPlayerNode = photonPlayerNode.Next ?? photonPlayerNode.List.First;
                        }
                    }
                    break;
                }
                else
                {
                    photonPlayerNode = photonPlayerNode.Next ?? photonPlayerNode.List.First;
                }
            }
            if (listBlitzPlayers.Count == 2)
            {
                photonView.RPC("UpdateTable", RpcTarget.Others, blitzPlayer1.gameObject.GetComponent<PhotonView>().ViewID, blitzPlayer2.gameObject.GetComponent<PhotonView>().ViewID);
                Debug.Log("Table updated");
            }
            else if (listBlitzPlayers.Count == 3)
            {
                photonView.RPC("UpdateTable", RpcTarget.Others, blitzPlayer1.gameObject.GetComponent<PhotonView>().ViewID, blitzPlayer2.gameObject.GetComponent<PhotonView>().ViewID, blitzPlayer3.gameObject.GetComponent<PhotonView>().ViewID);
            }
            else if (listBlitzPlayers.Count == 4)
            {
                photonView.RPC("UpdateTable", RpcTarget.Others, blitzPlayer1.gameObject.GetComponent<PhotonView>().ViewID, blitzPlayer2.gameObject.GetComponent<PhotonView>().ViewID, blitzPlayer3.gameObject.GetComponent<PhotonView>().ViewID, blitzPlayer4.gameObject.GetComponent<PhotonView>().ViewID);

            }
            player_count = listBlitzPlayers.Count;
            Create_Deck();
            //Create_Players();
            currentNode = listBlitzPlayers.Last;
            currentBlitzPlayer = currentNode.Value;
            StartCoroutine(Wait_For_Deck());
        }

    }
    [PunRPC]
    void UpdateTable(int a, int b)
    {
        Debug.Log($"UPDATE TBALE {a} {b}");
        blitzPlayer1 = PhotonView.Find(a).gameObject.GetComponent<BlitzPlayer>();
        blitzPlayer2 = PhotonView.Find(b).gameObject.GetComponent<BlitzPlayer>();
        blitzPlayer1.table = this; blitzPlayer2.table = this;
        listBlitzPlayers.AddLast(blitzPlayer1); listBlitzPlayers.AddLast(blitzPlayer2);
        currentNode = listBlitzPlayers.First;
        currentBlitzPlayer = blitzPlayer1;
        player_count = 2;
    }
    [PunRPC]
    void UpdateTable(int a, int b, int c)
    {
        blitzPlayer1 = PhotonView.Find(a).gameObject.GetComponent<BlitzPlayer>();
        blitzPlayer2 = PhotonView.Find(b).gameObject.GetComponent<BlitzPlayer>();
        blitzPlayer3 = PhotonView.Find(c).gameObject.GetComponent<BlitzPlayer>();
        blitzPlayer1.table = this; blitzPlayer2.table = this; blitzPlayer3.table = this;
        listBlitzPlayers.AddLast(blitzPlayer1); listBlitzPlayers.AddLast(blitzPlayer2); listBlitzPlayers.AddLast(blitzPlayer3);
        currentNode = listBlitzPlayers.First;
        currentBlitzPlayer = blitzPlayer1;
        player_count = 3;
    }
    [PunRPC]
    void UpdateTable(int a, int b, int c, int d)
    {
        blitzPlayer1 = PhotonView.Find(a).gameObject.GetComponent<BlitzPlayer>();
        blitzPlayer2 = PhotonView.Find(b).gameObject.GetComponent<BlitzPlayer>();
        blitzPlayer3 = PhotonView.Find(c).gameObject.GetComponent<BlitzPlayer>();
        blitzPlayer4 = PhotonView.Find(d).gameObject.GetComponent<BlitzPlayer>();
        blitzPlayer1.table = this; blitzPlayer2.table = this; blitzPlayer3.table = this; blitzPlayer4.table = this;
        listBlitzPlayers.AddLast(blitzPlayer1); listBlitzPlayers.AddLast(blitzPlayer2); listBlitzPlayers.AddLast(blitzPlayer3); listBlitzPlayers.AddLast(blitzPlayer4);
        currentNode = listBlitzPlayers.Last;
        currentBlitzPlayer = currentNode.Value;
        player_count = 4;
    }
    [PunRPC]
    void SyncTable(int a, bool b)
    {

        Debug.Log($"In SyncTable with a {a} b iss {b}");
        currentNode = listBlitzPlayers.Find(PhotonView.Find(a).gameObject.GetComponent<BlitzPlayer>());
        reversed = b;
    }
    public void Discard(GameObject card)
    {
        card.GetComponent<SpriteRenderer>().sortingOrder = discard.Count;
        discard.Add(card);
    }
    private void AddPlayer()
    {

    }



    private void Create_Players()
    {
        GameObject player = Resources.Load("Prefabs/player") as GameObject;
        if (player_count > 1)
        {
            GameObject player1_object = PhotonNetwork.Instantiate("player", new Vector3(-8f, -4.4f, 1f), transform.rotation);
            blitzPlayer1 = player1_object.GetComponent<BlitzPlayer>();
            blitzPlayer1.table = this;
            GameObject player2_object = PhotonNetwork.Instantiate("player", new Vector3(-8f, 4.4f, 1f), transform.rotation);
            blitzPlayer2 = player2_object.GetComponent<BlitzPlayer>();
            blitzPlayer2.table = this;
            listBlitzPlayers.AddLast(blitzPlayer1);
            listBlitzPlayers.AddLast(blitzPlayer2);
        }
        if (player_count > 2)
        {
            GameObject player3_object = PhotonNetwork.Instantiate("player", new Vector3(8f, 4.4f, 1f), transform.rotation);
            blitzPlayer3 = player3_object.GetComponent<BlitzPlayer>();
            blitzPlayer3.table = this;
            listBlitzPlayers.AddLast(blitzPlayer3);
            p3.gameObject.SetActive(true);
        }
        if (player_count == 4)
        {
            GameObject player4_object = PhotonNetwork.Instantiate("player", new Vector3(8f, -4.4f, 1f), transform.rotation);
            blitzPlayer4 = player4_object.GetComponent<BlitzPlayer>();
            blitzPlayer4.table = this;
            listBlitzPlayers.AddLast(blitzPlayer4);
            p4.gameObject.SetActive(true);
        }
    }
    IEnumerator Wait_For_Deck()
    {
        yield return new WaitUntil(() => draw_deck.draw_deck.Count == 100);
        Debug.Log("Deck == 100 " + draw_deck.draw_deck.Count);
        initial_deal();
    }
    private void Create_Deck()
    {
        Debug.Log("Deck start");
        GameObject new_deck = PhotonNetwork.InstantiateRoomObject("DeckOfCard", new Vector3(1.45f, 0f, 1f), transform.rotation);
        draw_deck = new_deck.GetComponent<DeckOfCard>();
        Debug.Log("Deck done");
    }
    public void initial_deal()
    {
        Debug.Log("Dealing start");
        Debug.Log(draw_deck.draw_deck.Count);
        currentNode = currentNode.List.Last;
        for (int i = 0; i < 5 * listBlitzPlayers.Count; i++)
        {
            AdvanceTurn();
        }
        AdvanceTurn();
        photonView.RPC("SyncTable", RpcTarget.All, currentNode.Value.gameObject.GetComponent<PhotonView>().ViewID, reversed);
        Debug.Log("Dealing done");
    }
    [PunRPC]
    void Advance()
    {
        StartCoroutine(NextPlayer());
    }
    public void AdvanceTurn()
    {
        Debug.Log("Turn advanced");

        Debug.Log($"call synctbale with viewID { currentNode.Value.gameObject.GetComponent<PhotonView>().ViewID} and reversed = {reversed}");
        photonView.RPC("SyncTable", RpcTarget.All, currentNode.Value.gameObject.GetComponent<PhotonView>().ViewID, reversed);
        Debug.Log($"call Advance with target ALL");
        photonView.RPC("Advance", RpcTarget.All);
        currentBlitzPlayer.Draw();
    }

    IEnumerator NextPlayer()
    {

        Debug.Log($"call netxplayer on master {PhotonNetwork.IsMasterClient}");
        currentBlitzPlayer.StackCards();
        if (reversed)
        {
            currentNode = currentNode.Previous ?? currentNode.List.Last;
        }
        else
        {
            currentNode = currentNode.Next ?? currentNode.List.First;
        }
        currentBlitzPlayer = currentNode.Value;
        if (PhotonNetwork.IsMasterClient)
        {

        }
        yield return new WaitUntil(() => ready);
        Update_Scores();
        currentBlitzPlayer.OrderCards();
    }
    public void Skip()
    {
        currentBlitzPlayer.StackCards();
        if (reversed)
        {
            currentNode = currentNode.Previous ?? currentNode.List.Last;
        }
        else
        {
            currentNode = currentNode.Next ?? currentNode.List.First;
        }
    }
    public void Reverse()
    {
        reversed = !reversed;
    }





    private void Update_Scores()
    {

        p1.text = blitzPlayer1.UpdateScore().ToString();

        p2.text = blitzPlayer2.UpdateScore().ToString();



        Debug.Log("Update_Scores() <> ref:00_ref_93781237");

        if (blitzPlayer3)
        {
            p3.text = blitzPlayer3.UpdateScore().ToString();
        }

        if (blitzPlayer4)
        {
            p4.text = blitzPlayer4.UpdateScore().ToString();
        }

        if (blitzPlayer1.score >= 21 && !currentBlitzPlayer.StopWin() )
        {

            gameOver.SetActive(true);
            gameOver.GetComponentInChildren<TextMeshProUGUI>().text = "Player 1 wins!";



        }
        else if (blitzPlayer2.score >= 21 && !currentBlitzPlayer.StopWin() )
        {
            gameOver.SetActive(true);
            gameOver.GetComponentInChildren<TextMeshProUGUI>().text = "Player 2 wins!";
        }
        else if (blitzPlayer3 && blitzPlayer3.score >= 21 && !currentBlitzPlayer.StopWin())
        {
            gameOver.SetActive(true);
            gameOver.GetComponentInChildren<TextMeshProUGUI>().text = "Player 3 wins!";
        }
        else if (blitzPlayer4 && blitzPlayer4.score >= 21 && !currentBlitzPlayer.StopWin())
        {
            gameOver.SetActive(true);
            gameOver.GetComponentInChildren<TextMeshProUGUI>().text = "Player 4 wins!";
        }


    }




    public void SetReady(bool a)
    {
        ready = a;
    }




    void Update()
    {


    }



    




}
