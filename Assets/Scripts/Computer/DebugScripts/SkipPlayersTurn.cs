using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipPlayersTurn : MonoBehaviour
{
   
    public void Onclick()// when skip my turn is clicked
    {
        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        p.Messagetoconsole("Skip Button Pressed");
        p.nextTurn();
        
    }

}
