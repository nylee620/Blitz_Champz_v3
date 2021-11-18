using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playersubpoints : MonoBehaviour
{
    public void Onclick()// when skip my turn is clicked
    {
        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        p.Messagetoconsole("subbed 1 point to player score");

        p.PScoreChange(-1);

    }
}
