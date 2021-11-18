using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Blitzable : MonoBehaviour
{

  
    private void OnMouseDown()//run this on the card that is selected to be blitzed
    {
        //run this on the card selected to be stolen, when a blitz is played
        GameObject g = GameObject.FindWithTag("Manager"); 
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        if (p.getBlitz() == true) {
            print("Player Blitzed the enemy card Succesfully");
            p.Messagetoconsole("Opponent Card Blitzed Succesfully!");
            p.setBlitz(false);
            transform.SetParent(GameObject.FindWithTag("PlayerScored").transform, false);
            TextMeshProUGUI t = GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>();
            t.text = (int.Parse(t.text) + int.Parse(tag)).ToString();
            TextMeshProUGUI e = GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>();
            e.text = (int.Parse(e.text) - int.Parse(tag)).ToString();
            p.setLastPlayed(this.gameObject);

            if (int.Parse(t.text) >= 21)
            {
                p.setAIBlock(true);
            }else{
                p.setAIBlock(false);
            }

            if (int.Parse(e.text) >= 21)
            {
                p.Messagetoconsole("Opponent wins!");
                p.Hault(2);
                p.AIWin();
            }
            else
            {
                p.setPlayerBlock(false);
            }

            p.halfNextTurn();
            Destroy(GetComponent<Blitzable>());
        }

    }
}
