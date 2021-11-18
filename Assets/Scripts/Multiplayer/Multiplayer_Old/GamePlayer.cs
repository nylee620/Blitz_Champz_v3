using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class GamePlayer : MonoBehaviourPunCallbacks
{
    public string displayName;
    private List<GameObject> hand = new List<GameObject>();
    public Table table;
    public bool myTurn = false;
    public int score = 0;
    public PhotonView PV;

    public void AddPoints(int points)
    {
        score += points;
        table.myScore.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    public void RemovePoints(int points)
    {
        score -= points;
        table.myScore.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
    public List<GameObject> Hand
    {
        get { return hand; }
    }

    public string DisplayName { get => displayName; set => displayName = value; }

    public void DrawCard()
    {
        Debug.Log("Drawing card");
    }

    public void AddCard(GameObject card)
    {
        hand.Add(card);
        Instantiate(hand.Last(), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)).transform.SetParent(table.myHandArea.transform);
    }

    public void PlayCard(GameObject card)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player object Created");
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
