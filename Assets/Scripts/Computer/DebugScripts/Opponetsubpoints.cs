using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponetsubpoints : MonoBehaviour
{
    public void Onclick()// when skip my turn is clicked
    {
        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        p.Messagetoconsole(p.opponentsname + " had 1 point removed");
        p.EScoreChange(-1);


    }
}
