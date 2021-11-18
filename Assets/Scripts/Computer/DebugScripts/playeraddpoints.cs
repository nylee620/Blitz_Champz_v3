using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeraddpoints : MonoBehaviour
{
    public void Onclick()// when skip my turn is clicked
    {
        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        p.Messagetoconsole("Added 1 point to players score");
        p.PScoreChange(1);


    }
}
