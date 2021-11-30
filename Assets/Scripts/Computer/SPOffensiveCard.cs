using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SPOffensiveCard : MonoBehaviour 
{



    private void OnMouseDown()//when the mouse is clicked down, there isn't a OnMouseClick for everything
    {
        
        if (transform.rotation != Quaternion.Euler(0, 0, 90))//checks to see if the card is sideways, meaning it has been played
        { 
            GameObject g = GameObject.FindWithTag("Manager");
            GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

            if (p.getBlitz() == true)//this is to check whether they played a blitz card, because that will not cycle the turn until they select the card to steal
            {
                print("Select a card to steal! You played a blitz");
                p.Messagetoconsole("Select a card to take from "+ p.opponentsname);
                return;
            }

            if (p.getTurn() % 2 == 0 || p.getTurn() %1 != 0)//verifies that it is the players turn. if it's not, the rest of the script won't run
            {
                print("It is not your turn to play");
                //p.Messagetoconsole("It is not your turn");
                return;
            }
            
            if(p.PlayerBlock() == true)//this means the player needed to block. they lose because they didnt. 
            {
                p.AIWin();
                return;
            }
   

            p.setLastPlayed(this.gameObject); //this sets this card as the last played card. this is to allow defensive cards to have a target
            Destroy(GetComponent<CardHover>());
            transform.localScale = new Vector3(1f, 1f, 0); //sets the scale of the card to default
            p.halfNextTurn();
            transform.rotation = Quaternion.Euler(0, 0, 20);
            StartCoroutine(PlayCard(GameObject.FindWithTag("PlayerScored").transform.position, p.animationSpeed));

            //transform.SetParent(GameObject.FindWithTag("PlayerScored").transform, true);
            //^this sets the position based on two functions from the Manager to the correct place on the field
            //transform.rotation = Quaternion.Euler(0, 0, 90); //turns the card sideways to show it's been played
            //p.nextTurn(); //increments the turn counter so the computer can act. 
        }
        else
        {
            Debug.Log("Because the offensive card is sideways Ie PLAYED, nothing can be done with the clicked card");

        }

    }

    
    IEnumerator PlayCard(Vector3 targetPosition, float speed)
    {

        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
     //   p.Messagetoconsole("You played an offensive card!");

        float rotation = 0;
        transform.localScale = new Vector3(1f, 1f, 0);
        while (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
        {
            if (transform.rotation != Quaternion.Euler(0, 0, 90))
            {
                transform.rotation = Quaternion.Euler(0, 0, rotation);
                rotation += .5f;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
            
        }
        
        transform.position = targetPosition;
        transform.rotation = Quaternion.identity;
        transform.SetParent(GameObject.FindWithTag("PlayerScored").transform, false);
        transform.localScale = new Vector3(1f, 1f, 0);
        GetComponent<AudioSource>().Play();
        TextMeshProUGUI t = GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>();
        //^finds the player score object to edit its text
        t.text = (int.Parse(t.text) + int.Parse(tag)).ToString();//adds the tag of the offensive card to the player score. tag has been set to be the value of the card
        if (int.Parse(t.text) >= 21)
        {
            p.setAIBlock(true);
        }
        else
        {
            p.setAIBlock(false);
        }
        p.halfNextTurn();
        
    }

}
