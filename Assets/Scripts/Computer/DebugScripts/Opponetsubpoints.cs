using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponetsubpoints : MonoBehaviour
{
    public void Onclick()// when skip my turn is clicked
    {
        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        p.Messagetoconsole("Subbed 1 point to Opponets score");
        p.EScoreChange(-1);


    }
}
