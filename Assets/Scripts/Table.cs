using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviourPun
{
    public List<GamePlayer> players = new List<GamePlayer>();
    public List<GameObject> deck; //100 cards in deck
    public GameObject timer; //timer for play starts at 60 seconds
    public GameObject playbook;
    private float startTime;
    private readonly float TURN_TIME = 60;
    private bool timerRun;
    public bool iWin;
    public Text localPlayer;
    public Text remotePlayer;
    public GameObject myScore;
    public GameObject opponentScore;
    public GameObject myHandArea;
    public static GamePlayer myGamePlayer;
    public GameObject DeckObject;
    public PhotonView PV;
    public GameObject myTurnMarker;
    public GameObject awayTurnMarker;
    public GameObject homePlayedCards;
    public GameObject awayPlayedCards;
    public GameObject homeLastPlayed;
    public GameObject awayLastPlayed;

    //CARD OBJECTS//
    public GameObject blitz;
    public GameObject fumble;
    public GameObject fiveYardRun;
    public GameObject firstDown;
    public GameObject passCompletion;
    public GameObject endOfQuarter1;
    public GameObject endOfQuarter2;
    public GameObject endOfQuarter3;
    public GameObject endOfQuarter4;
    public GameObject blockedKick;
    public GameObject tackle;
    public GameObject interception;
    public GameObject fieldGoal;
    public GameObject conversion;
    public GameObject hailMary;
    public GameObject passingTD;
    public GameObject rushingTD;
    public GameObject extraPoint;

    public void CreateDeck()
    {
        Debug.Log("Creating Deck");
        //** CONTINTUATION CARDS **//
        foreach (GameObject card in CreateCard(8, blitz)) //creates a list of a specific cards with a specified number of cards and iterates over them
        {
            deck.Add(card); //adds each card in the newly created list, to the deck
        }
        foreach (GameObject card in CreateCard(4, fumble))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(8, fiveYardRun))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(8, firstDown))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(8, passCompletion))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(2, endOfQuarter1))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(2, endOfQuarter2))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(2, endOfQuarter3))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(2, endOfQuarter4))
        {
            deck.Add(card);
        }

        //**DEFENSIVE CARDS * *//

        foreach (GameObject card in CreateCard(11, blockedKick))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(4, tackle))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(3, interception))
        {
            deck.Add(card);
        }

        //**OFFFENSIVE CARDS * *//

        foreach (GameObject card in CreateCard(7, fieldGoal))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(6, conversion))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(2, hailMary))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(6, passingTD))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(6, rushingTD))
        {
            deck.Add(card);
        }
        foreach (GameObject card in CreateCard(11, extraPoint))
        {
            deck.Add(card);
        }
    }

    public List<GameObject> CreateCard(int numToMake, GameObject cardType)
    {
        List<GameObject> cards = new List<GameObject>();
        for (int i = 0; i < numToMake; i++)
        {
            //PhotonNetwork.InstantiateRoomObject(cardType, new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(GameObject.FindWithTag("DeckObject").transform);
            cards.Add(cardType);
        }
        return cards;
    }

    public void ShuffleDeck()
    {
        Debug.Log("Shuffling Deck");
        deck = deck.OrderBy(Matrix4x4 => UnityEngine.Random.value).ToList(); //poor complexity but with only 100 items it *should* be okay
    }

    public void InstantiateDeckToPhoton()
    {
        foreach (GameObject card in deck)
        {
            PhotonNetwork.InstantiateRoomObject(card.name, new Vector3(0, 0, 0), Quaternion.identity);
        }

    }
    public void DrawCard() //possibly add argument to be passed for how many cards are to be drawn
    {
        Debug.Log("Drawing card now");
        int nextCardToDraw = GameObject.FindWithTag("DeckObject").GetComponent<Deck>().deckIndex;
        Debug.Log("Cards in deck " + GameObject.FindWithTag("DeckObject").transform.childCount);
        myGamePlayer.AddCard(GameObject.FindWithTag("DeckObject").transform.GetChild(nextCardToDraw).gameObject);
        GameObject.FindWithTag("DeckObject").GetComponent<Deck>().deckIndex++;
    }

    public void Deal()
    {
        Debug.Log("Dealing Cards");
        foreach (GamePlayer player in players)
        {
            Debug.Log("Dealing to: " + player);
            for (int i = 0; i < 5; i++) // each player starts with 5 cards
            {
                player.DrawCard();
            }
        }
        
    }

    public void DrawHand()
    {
        for (int i = 0; i < 4; i++) // each player starts with 5 cards
        {
            Debug.Log("Drawing hand now");
            int nextCardToDraw = GameObject.FindWithTag("DeckObject").GetComponent<Deck>().deckIndex;
            Debug.Log("Cards in deck " + GameObject.FindWithTag("DeckObject").transform.childCount);
            myGamePlayer.AddCard(GameObject.FindWithTag("DeckObject").transform.GetChild(nextCardToDraw).gameObject);
            GameObject.FindWithTag("DeckObject").GetComponent<Deck>().deckIndex++;
        }
        AdvanceTurn();
    }

    void AssignViewSpecific()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if(player == PhotonNetwork.LocalPlayer)
            {
                localPlayer.text = player.NickName;
            }
            else
            {
                remotePlayer.text = player.NickName;
            }
        }
    }

    IEnumerator WaitForDeck()
    {
        Debug.Log("Deck child count: " + GameObject.FindWithTag("DeckObject").transform.childCount);
        yield return new WaitUntil(() => GameObject.FindWithTag("DeckObject").transform.childCount == 100);
        DrawHand();
    }

    IEnumerator WaitForTurn()
    {
        Debug.Log("Wainting for turn...");
        yield return new WaitUntil(() => myGamePlayer.myTurn);
        Debug.Log("My Turn!");
        StartCoroutine(WaitForDeck());
    }

    [PunRPC]
    public void SyncPoints(string myPoints, string opponentPoints) //send both my score and opponents score to opponent. This will keep it synced across the network.
    {
        myScore.GetComponent<TextMeshProUGUI>().text = opponentPoints; //this looks confusing because at the end of your turn you are passing your score to the oponnents instance
        opponentScore.GetComponent<TextMeshProUGUI>().text = myPoints; //therefore, when your opponent recieves the call to update the score, what is myScore and opponentScore is switched.
    }

    [PunRPC]
    public void SyncCards(int[] myPlayed, int[] opponentPlayed)
    {
        if (myPlayed.Length > 0)
        {
            PhotonView temp = PhotonView.Find(myPlayed[0]);
            Transform card = temp.gameObject.transform;
            card.transform.SetParent(awayLastPlayed.transform);
            foreach (int viewId in myPlayed.Skip(1))
            {
                if (PhotonView.Find(viewId).transform.parent != awayPlayedCards)
                {
                    PhotonView.Find(viewId).transform.SetParent(awayPlayedCards.transform);
                }
            }
        }
        if (opponentPlayed.Length > 0)
        {
            PhotonView.Find(opponentPlayed[0]).transform.SetParent(homePlayedCards.transform);
            foreach (int viewId in opponentPlayed.Skip(1))
            {
                if (PhotonView.Find(viewId).transform.parent != homePlayedCards)
                {
                    PhotonView.Find(viewId).transform.SetParent(homePlayedCards.transform);
                }
            }
        }
    }

    void SyncCards()
    {
        int[] myCards = new int[homePlayedCards.transform.childCount + homeLastPlayed.transform.childCount];
        int[] awayCards = new int[awayPlayedCards.transform.childCount + awayLastPlayed.transform.childCount];

        if(homeLastPlayed.transform.childCount > 0)
        {
            myCards[0] = homeLastPlayed.transform.GetChild(0).GetComponent<PhotonView>().ViewID;
        }
        if (awayLastPlayed.transform.childCount > 0)
        {
            awayCards[0] = awayLastPlayed.transform.GetChild(0).GetComponent<PhotonView>().ViewID;
        }
        int index = 0;
        foreach (GameObject card in homePlayedCards.transform)
        {
            myCards[index] = card.GetComponent<PhotonView>().ViewID;
        }
        index = 0;
        foreach (GameObject card in homePlayedCards.transform)
        {
            awayCards[index] = card.GetComponent<PhotonView>().ViewID;
        }
        PV.RPC("SyncCards", RpcTarget.Others, myCards, awayCards);
    }

    [PunRPC]
    void TurnChange(int currentDeckPosition)
    {
        GameObject.FindWithTag("DeckObject").GetComponent<Deck>().deckIndex = currentDeckPosition; //syncs the position in the deck that is next to be drawn
        myGamePlayer.myTurn = !myGamePlayer.myTurn;

        if (myGamePlayer.myTurn)
        {
            CheckForWin();
            myTurnMarker.SetActive(true);
            awayTurnMarker.SetActive(false);
            DrawCard();
        }
        else
        {
            myTurnMarker.SetActive(false);
            awayTurnMarker.SetActive(true);
        }
    }

    void CheckForWin() //condition checked at the beginning of turn before a new card is played
    {
        if(int.Parse(myScore.GetComponent<TextMeshProUGUI>().text) >= 21)
        {
            iWin = true;
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " wins!!");
            //TODO handle event for when a user wins.
        }
    }

    public void AdvanceTurn()
    {
        PV.RPC("SyncPoints", RpcTarget.Others, myScore.GetComponent<TextMeshProUGUI>().text, opponentScore.GetComponent<TextMeshProUGUI>().text);
        //SyncCards();
        Debug.Log("Advance entered");
        if (GameObject.FindWithTag("DeckObject").GetPhotonView().IsMine) //if the deck belongs to the client that called this function
        {
            Debug.Log("Deck is mine");
            foreach (Player player in PhotonNetwork.PlayerList) //search the list of players
            {
                Debug.Log("On player: " + player.NickName);
                if (player != PhotonNetwork.LocalPlayer) //if the current player in the iteration of players is not the player that called this function
                {
                    GameObject.FindWithTag("DeckObject").GetPhotonView().TransferOwnership(player); // then transfer ownership of the deck to the next player to signal that it is their turn and allow them to destroy cards
                    TurnChange(GameObject.FindWithTag("DeckObject").GetComponent<Deck>().deckIndex);
                    PV.RPC("TurnChange", RpcTarget.Others, GameObject.FindWithTag("DeckObject").GetComponent<Deck>().deckIndex);
                    Debug.Log("Turn advanced to: " + player.NickName);
                }
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        myGamePlayer = myHandArea.GetComponent<GamePlayer>();
        myGamePlayer.table = this;
        if (PhotonNetwork.IsMasterClient)
        {
            myGamePlayer.myTurn = true; //client that created the lobby always goes first
            deck = new List<GameObject>();
            PhotonNetwork.InstantiateRoomObject(DeckObject.name, new Vector3(0, 0, 0), Quaternion.identity);
            CreateDeck();
            ShuffleDeck();
            InstantiateDeckToPhoton();
        }
        AssignViewSpecific();
        StartCoroutine(WaitForTurn());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.PlayerList.Length != 2) //If a players leaves the game
        {
            PhotonNetwork.LeaveRoom(true);
            PhotonNetwork.LoadLevel(0); //load main menu
        }
        if (iWin)
        {
            //TODO handle winning
        }
    }
}
