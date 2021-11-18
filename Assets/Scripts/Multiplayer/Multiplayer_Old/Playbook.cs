using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playbook : MonoBehaviour
{
    public List<GameObject> homePlayedCards;
    public List<GameObject> awayPlayedCards;
    public PhotonView PV;

    //public void AddCard(Card card, int destination) //0 is home, 1 is away, this will only work if Home and Away in the Playbook gameobject stay in the order they are in
    //{
    //    card.transform.SetParent(Table.myGamePlayer.table.playbook.transform.GetChild(destination));
    //}
    //public void RemoveCard()
    //{

    //}

    //[PunRPC]
    //public void TakeOpponentCard(int viewID)
    //{
    //    foreach(GameObject card in homePlayedCards)
    //    {
    //        if(card.GetPhotonView().ViewID == viewID)
    //        {
    //            card.SetActive(false);
    //        }
    //    }
    //    SyncCards();
    //}

    //[PunRPC]
    //public void SendPlayedCard(int viewId)
    //{
    //    awayPlayedCards.Add(PhotonView.Find(viewId).gameObject);
    //    SyncCards();
    //}

    

    // Start is called before the first frame update
    void Start()
    {
        homePlayedCards = new List<GameObject>();
        awayPlayedCards = new List<GameObject>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
