using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SPContinuationCard : MonoBehaviour
{
    
    public void OnMouseDown() //per click on a continuation card
    {
  

        //parse(tag) is refrenced from Gamemanager (which is the tag being used)
        //need exepection handling for when it becomes a strong


        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

       if (p.getTurn() % 2 == 0 || p.getTurn() % 1 != 0)//verifies that it is the players turn. if it's not, the rest of the script won't run
            {
                print("It is not your turn to play");
                //p.Messagetoconsole("It is not your turn");
                return;
            }
       else if (p.getBlitz() == true)//this is to check whether they played a blitz card, because that will not cycle the turn until they select the card to steal
        {
            print("Select a card to steal! You played a blitz");
            p.Messagetoconsole("Select a card to take from "+ p.opponentsname);
            return;
        }
   
        else
        {
            //check here, for tag = "discard" 
            if (tag != "discard") {
                int tagNumber = int.Parse(tag);

                if (tagNumber == 6)
                { //blitz cards are given the tag number 6 

                    print("Player Clicked A Blitz Card");
                    if (p.GetAIScore() == 0)//Michael:  When the player clicks a blitz card, assure that the enemy has a offensive card to steal. An offensive card exist if the score is not 0
                    {
                        print("Enemy Score is 0, thus no card can be Blitzed");
                        p.Messagetoconsole("Can't Blitz as no cards can be taken");
                    } else {
                        print("Enemy Score is not 0, thus a card can be Blitzed");
                        p.Messagetoconsole("Select one of "+ p.opponentsname+"'s offensive card to take!");
                        p.setBlitz(true);
                        StartCoroutine(PlayBlitz(p.animationSpeed)); // run the PlayBlitz inumerator with the animation speed 500
                    }

                    return; ;

                } else {
                    if (p.PlayerBlock() == true)//if they needed to block/blitz and tried to play another card, they lose
                    {
                        p.AIWin();
                        return;
                    }
                    else if (tagNumber <= 2)//first down, pass completion, or 5-yard run 
                    {
                        //tag = 10.ToString();

                        p.Messagetoconsole("You got some extra cards by moving ahead!");
                        p.halfNextTurn();
                        StartCoroutine(DiscardDraw(p.animationSpeed));
                    }
                    else if (tagNumber == 3)
                    {//fumble or end of quarter
                     //these make the player go again, so the turn # isnt changed
                     //This is to draw a card for the players turn.
                        p.Messagetoconsole("You changed up the plays! Play another card.");
                        p.setLastPlayedAI(null);
                        GameObject newCard = p.draw();
                        newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
                        StartCoroutine(DiscardSkipTurn(p.animationSpeed));
                    }
                }

                return;
            }


            else
            {
                print("Card is discarded");
                print("card tag: "+tag);

            }


        }

     
    }


    public void Onclick()// for testing a draw button
    {
        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        GameObject newCard = p.draw();
        newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
    }


    IEnumerator DiscardDraw(float speed)
    {
        Debug.Log("SPCON CARD _ DISCARD DRAW HERE - 1");

        Destroy(GetComponent<CardHover>());
        transform.localScale = new Vector3(1f, 1f, 0);

        Vector3 targetPosition = GameObject.FindWithTag("Discard Pile").transform.position;

        GameObject g = GameObject.FindWithTag("Manager"); //this is to give the script access to the GameManager functions
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));
        Debug.Log("SPCON CARD _ DISCARD DRAW HERE - 2");

        while (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            Debug.Log("SPCON CARD _ DISCARD DRAW HERE - 3");

            yield return null;

        }
        //transform.localScale = new Vector3(1f, 1f, 0); //this sets the scale of the card
        for (int i = 1; i <= int.Parse(tag); i++) // draw cards for the number on the tag
        {
            Debug.Log("SPCON CARD _ DISCARD DRAW HERE - 4");

            GameObject newCard = p.draw();
            newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
        }
        Debug.Log("SPCON CARD _ DISCARD DRAW HERE - 5");

        GetComponent<AudioSource>().Play();
        Debug.Log("SPCON CARD _ DISCARD DRAW HERE - 6");

        tag = "discard"; //this changes the tag of the object to discard so the card can be identified as played
        
        Debug.Log("SPCON CARD _ DISCARD DRAW HERE - 7");

        transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
        Debug.Log("SPCON CARD _ DISCARD DRAW HERE - 8");

        p.setLastPlayed(null);
        p.halfNextTurn();

    }
    IEnumerator DiscardSkipTurn(float speed)
    {
        Destroy(GetComponent<CardHover>());
        transform.localScale = new Vector3(1f, 1f, 0);

        Vector3 targetPosition = GameObject.FindWithTag("Discard Pile").transform.position;

        GameObject g = GameObject.FindWithTag("Manager"); //this is to give the script access to the GameManager functions
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

        while (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;

        }
        //transform.localScale = new Vector3(1f, 1f, 0); //this sets the scale of the card
        GetComponent<AudioSource>().Play();
        tag = "discard"; //this changes the tag of the object to discard so the card can be identified as played
        transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);

        p.setLastPlayed(null);

    }
    IEnumerator PlayBlitz(float speed)
    {
        
        
        GameObject g = GameObject.FindWithTag("Manager"); //this is to give the script access to the GameManager functions
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

        p.Messagetoconsole("Select a card to take"); //push this message to the player
        
        Destroy(GetComponent<CardHover>());
            transform.localScale = new Vector3(1f, 1f, 0);
            Vector3 targetPosition = GameObject.FindWithTag("Discard Pile").transform.position;
         
            while (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            //transform.localScale = new Vector3(1f, 1f, 0); //this sets the scale of the card
            tag = "discard"; //this changes the tag of the object to discard so the card can be identified as played
            transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
            GetComponent<AudioSource>().Play();
            if (p.GetPlayerScore() >= 21)
            {
                p.setAIBlock(true);
            }
            else
            {
                p.setAIBlock(false);
            }
            p.setLastPlayed(null);
            p.halfNextTurn();
        
    
    }




}
