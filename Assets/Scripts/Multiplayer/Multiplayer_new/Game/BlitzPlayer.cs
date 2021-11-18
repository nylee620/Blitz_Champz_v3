using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon;
using Photon.Pun;

public class BlitzPlayer : MonoBehaviourPunCallbacks
{
    public int score;
    public List<GameObject> hand;
    public List<GameObject> field;
    public GameTable table;
    public bool right = false;
    public bool up = false;
    public bool valid = true;
    //animation
    public float speed = 1f;
    private Vector3 target;
    private Vector3 position;
    void Start()
    {
        score = 0;
        if (this.transform.position.x > 0)
        {
            right = true;
        }
        if (this.transform.position.y > 0)
        {
            up = true;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180f);
        }
    }



    public int UpdateScore()
    {
        score = 0;
       
        foreach (GameObject card in field)
        {
            if (card.GetComponent<BaseCard>().owner != this)
            {
                field.Remove(card);
            }
            else
            {
                score += card.GetComponent<Offensive_Card>().GetValue();
            }
        }
        return score;
    }
    [PunRPC]
    //new method added below
    void AddCard(int ID)
    {
        GameObject new_card = PhotonView.Find(ID).gameObject;
        new_card.GetComponent<BaseCard>().SetOwner(this);
        hand.Add(new_card);
        photonView.RPC("OrderCards", RpcTarget.All);
    }
    public void Draw()
    {
        //moved Deck draw_deck = table.draw_deck;
        //old if (draw_deck.draw_deck.Count > 0 && table.current_player == this) {
        if (PhotonNetwork.IsMasterClient && this == table.currentBlitzPlayer)
        {
            Debug.Log("Drawing from master client" + PhotonNetwork.LocalPlayer.UserId);
            DeckOfCard draw_deck = table.draw_deck;
            GameObject new_card = draw_deck.Draw();
            int ID = new_card.GetComponent<PhotonView>().ViewID;
            photonView.RPC("AddCard", RpcTarget.All, ID);
        }
        //possible error here, remove if so
        OrderCards();
    }
    //next 2 methods are added
    public void ReclaimOthers()
    {
        photonView.RPC("Reclaim", RpcTarget.Others);
    }
    [PunRPC]
    void Reclaim()
    {
        foreach (GameObject a in field)
        {
            a.GetComponent<PhotonView>().RequestOwnership();
        }
    }

    public void Remove(GameObject card)
    {
        field.Remove(card);
        hand.Remove(card);
    }
    public void StackCards()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.position = gameObject.transform.position;
            hand[i].GetComponent<BaseCard>().Hide();
            hand[i].GetComponent<BoxCollider>().enabled = false;
        }
        OrderField();
    }
    public void OrderField()
    {
        for (int i = 0; i < field.Count; i++)
        {
            field[i].transform.position = gameObject.transform.position;
            field[i].GetComponent<SpriteRenderer>().color = Color.white;
            if (right)
            {
                field[i].GetComponent<SpriteRenderer>().sortingOrder = i;
                Vector3 adjustment = new Vector3(-1.75f + -1 * 0.25f * i, 0, 2 * (field.Count - i));
                //This determines the card's final position on the board
                field[i].transform.position = transform.position + adjustment + Vector3.Scale(transform.up, new Vector3(0, 2.5f, 0));
                field[i].transform.rotation = Quaternion.Euler(0, 0, -90f);
            }
            else
            {
                field[i].GetComponent<SpriteRenderer>().sortingOrder = i;
                Vector3 adjustment = new Vector3(1.75f + 0.25f * i, 0, 2 * (field.Count - i));
                //This determines the card's final position on the board
                field[i].transform.position = transform.position + adjustment + Vector3.Scale(transform.up, new Vector3(0, 2.5f, 0));
                field[i].transform.rotation = Quaternion.Euler(0, 0, 90f);
            }
        }
    }
    [PunRPC]
    public void OrderCards()
    {
        if (table.currentBlitzPlayer == this)
        {
            if (CheckValid() == false)
            {
                Debug.Log("No valid cards. Discard please.");
            }
            if (right)
            {
                for (int i = 0; i < hand.Count; i++)
                {
                    Vector3 adjustment = new Vector3(-1 * 0.5f * i, 0.0f, 0.0f);
                    hand[i].GetComponent<SpriteRenderer>().sortingOrder = 2 * i;
                    hand[i].GetComponent<Transform>().position = gameObject.transform.position + adjustment + new Vector3(0f, 0f, 2 * (hand.Count - i));
                    //if an error occurs here, add this method in
                    /*
					if (up) {
						hand[i].transform.rotation = Quaternion.Euler(0,0,180f);
					}
					*/
                    hand[i].GetComponent<BoxCollider>().enabled = true;
                    //old if (this == table.current_player) {
                    if (gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer && this == table.currentBlitzPlayer)
                    {
                        hand[i].GetComponent<BaseCard>().Show();
                    }
                    //added below to hide hand from other players
                    else
                    {
                        hand[i].GetComponent<BaseCard>().Hide();
                    }
                }
            }
            else
            {
                for (int i = 0; i < hand.Count; i++)
                {
                    Vector3 adjustment = new Vector3(0.5f * i, 0.0f, 0.0f);
                    hand[i].GetComponent<SpriteRenderer>().sortingOrder = 2 * (hand.Count - i);
                    hand[i].GetComponent<Transform>().position = gameObject.transform.position + adjustment + new Vector3(0f, 0f, 2 * i);
                    //if an error occurs here, add this method in
                    /*
					if (up) {
						hand[i].transform.rotation = Quaternion.Euler(0,0,180f);
					}
					*/
                    hand[i].GetComponent<BoxCollider>().enabled = true;
                    //old if (this == table.current_player) {
                    if (gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer && this == table.currentBlitzPlayer)
                    {
                        hand[i].GetComponent<BaseCard>().Show();
                    }
                    //added below to hide hand from other players
                    else
                    {
                        hand[i].GetComponent<BaseCard>().Hide();
                    }
                }
            }
        }
        OrderField();
    }
    protected bool CheckValid()
    {
        bool temp_valid = false;
        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].GetComponent<BaseCard>().CheckValid())
            {
                temp_valid = true;
            }
        }
        valid = temp_valid;
        return temp_valid;
    }
    public bool StopWin()
    {
        bool canStop = false;
        foreach (GameObject a in hand)
        {
            if (a.GetComponent<Defensive_Card>() != null)
            {
                if (a.GetComponent<Defensive_Card>().CheckValid())
                {
                    canStop = true;
                    a.GetComponent<Defensive_Card>().SetPlayed(true);
                }
                else
                {
                    a.GetComponent<BoxCollider>().enabled = false;
                    a.GetComponent<SpriteRenderer>().color = Color.gray;
                }
            }
            else if (a.GetComponent<BlitzCard>() != null)
            {
                canStop = true;
                a.GetComponent<BlitzCard>().SetPlayed(true);
            }
            else
            {
                a.GetComponent<BoxCollider>().enabled = false;
                a.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
        return canStop;
    }
    public bool GetValid()
    {
        return valid;
    }

    void Update()
    {

    }

    //MoveTo Coroutine
    IEnumerator MoveTo()
    {


        // This looks unsafe, but Unity uses
        // en epsilon when comparing vectors.
        while (transform.position != target)
        {
            Debug.Log("Got to 4 loop");
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                speed);
            // Wait a frame and move again.
            yield return null;
        }
    }
}