using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrawCard : MonoBehaviour
{
    public void Onclick()// for testing a draw button
    {
        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        p.Messagetoconsole("Player Draw Card Clicked, adding 1 card");
        GameObject newCard = p.draw();
        newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
    }
}
