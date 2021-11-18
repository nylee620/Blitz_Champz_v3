using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    /*
     * add a list of names of names to pull from to act like you are playing those players
     * fix problem with cards going to discard pile
     */
    private bool testdeck;
    public bool debugmode = false;
    

    private GameObject lastPlayed;
    //private GameObject cardPlayed;//experiment to rename played cards to incrementing numbers so i can find them later.
    private GameObject lastPlayedAI;
    private double gameTurn;

    private int[] deck;
    private int deckPosition;

    private bool PlayerMustBlock;
    private bool AIMustBlock;

    private bool Blitz;

    private int[] AIhand;
    private int AIhandindex;

    public GameObject SPTackle;
    public GameObject SPInterception;
    public GameObject SPBlocked_Kick;
    public GameObject SPBlitz;
    public GameObject SPFumble;
    public GameObject SPPass_Completion;
    public GameObject SPFirst_Down;
    public GameObject SP5Yard_Run;
    public GameObject SPConversion;
    public GameObject SPField_Goal;
    public GameObject SPExtra_Point;
    public GameObject SPHail_Mary;
    public GameObject SPPassing_TD;
    public GameObject SPRushin_TD;
    public GameObject SPEnd_Of_Quarter;
    public float animationSpeed;


    
    void Start()
    {
        lastPlayed = null;
        lastPlayedAI = null;
        gameTurn = 1;
        buildDeck();
        deckPosition = 0;
        PlayerMustBlock = false;
        AIMustBlock = false;
        Blitz = false;
        AIhand = new int[100];
        AIhandindex = 0;
        for (int i = 0; i < 5; i++)//AI draws it's initial 4 cards, draws the last when it's first turn starts
        {
            AIdraw();
        }
        //print(AIhand[0] + " " + AIhand[1] + " " + AIhand[2] + " " + AIhand[3] + " " + AIhand[4] + " " + AIhand[5] + " ");
        animationSpeed = 1000; // higher is faster (For whatever reason..)  lower is slower
        Debug.Log("START AnimationSpeed: "+ animationSpeed);

    }

    private void buildDeck(){
        /*when reached end of the deck, recycle the discard pile, do not create a new deck
         *  use the discard pile, apply it to a list, shuffle the list and use that as a new deck
         * 
         
        /* here is the breakdown of what card each number represents, the second column is the quantity of that card
       * assigned #   quantity    CardName   
           1	        4	        Tackle 
           2	        3	        Interception
           3	        11	        Blocked Kick
           4	        8	        Blitz
           5	        4	        Fumble
           6	        8	        Pass Completion
           7	        8	        First Down
           8	        8	        5-Yard Run
           9	        6	        Conversion
           10	        7	        Field Goal
           11	        11	        Extra Point
           12	        2	        Hail Mary
           13      	    6       	Passing TD
           14      	    6       	Rushing TD
           15	        8       	End of Quarter

           */


        if (debugmode) { Messagetoconsole("Building Deck.."); }

        testdeck = false;
        if (testdeck){
            print("test deck build");
                deck = new int[]{
                14,14,14,14,
                14,14,14,
                12,14,14,14,12,12,12,12,12,12,12,
                4,14,14,14,14,14,4,4,
                4,14,4,4,
                4,14,14,14,14,14,14,14,
                4,14,4,4,4,4,4,4,
                4,4,4,4,4,4,4,4,
                4,4,4,4,4,4,
                1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,1,1,1,
                1,1,
                1,1,1,1,1,1,
                1,1,1,1,1,1,
                1,1,1,1,1,1,1,1
                };
                }else{
                print("regular deck build");
                deck = new int[]{
                1,1,1,1,
                2,2,2,
                3,3,3,3,3,3,3,3,3,3,3,
                4,4,4,4,4,4,4,4,
                5,5,5,5,
                6,6,6,6,6,6,6,6,
                7,7,7,7,7,7,7,7,
                8,8,8,8,8,8,8,8,
                9,9,9,9,9,9,
                10,10,10,10,10,10,10,
                11,11,11,11,11,11,11,11,11,11,11,
                12,12,
                13,13,13,13,13,13,
                14,14,14,14,14,14,
                15,15,15,15,15,15,15,15
                };
                if (debugmode) {Messagetoconsole("Deck built");}
                
        }

                  /*
                  deck = new int[]
                    {
                        1,1,1,1,
                        2,2,2,
                        3,3,3,3,3,3,3,3,3,3,3,
                        4,4,4,4,4,4,4,4,
                        5,5,5,5,
                        6,6,6,6,6,6,6,6,
                        7,7,7,7,7,7,7,7,
                        8,8,8,8,8,8,8,8,
                        9,9,9,9,9,9,
                        10,10,10,10,10,10,10,
                        11,11,11,11,11,11,11,11,11,11,11,
                        12,12,
                        13,13,13,13,13,13,
                        14,14,14,14,14,14,
                        15,15,15,15,15,15,15,15
                        };
                         */


        int temp;
        int r1;
        int r2;
        for (int i = 0; i < 500; i++)//this is my shuffle system. rolls two numbers and swaps the numbers at those positions. can be run more if it's not random enough.
        {
            r1 = Random.Range(0, 100);
            r2 = Random.Range(0, 100);
            temp = deck[r1];
            deck[r1] = deck[r2];
            deck[r2] = temp;
        }


        
    }


    public GameObject draw()// this is the function for drawing cards. it takes the next number in the deck, determines what card it is, and instantiates it, returning the object
    {
        int cardID = deck[deckPosition];
        deckPosition++;
        if (deckPosition > 99)
        {
            buildDeck();
            deckPosition = 0;
        }
        GameObject g;
        switch (cardID)
        {
            case 1:
                g = Instantiate(SPTackle, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;//position is arbitrary because the function calling this should change it
                break;
            case 2:
                g = Instantiate(SPInterception, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 3:
                g = Instantiate(SPBlocked_Kick, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 4:
                g = Instantiate(SPBlitz, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 5:
                g = Instantiate(SPFumble, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 6:
                g = Instantiate(SPPass_Completion, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 7:
                g = Instantiate(SPFirst_Down, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 8:
                g = Instantiate(SP5Yard_Run, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 9:
                g = Instantiate(SPConversion, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 10:
                g = Instantiate(SPField_Goal, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 11:
                g = Instantiate(SPExtra_Point, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 12:
                g = Instantiate(SPHail_Mary, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 13:
                g = Instantiate(SPPassing_TD, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 14:
                g = Instantiate(SPRushin_TD, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
            case 15:
                g = Instantiate(SPEnd_Of_Quarter, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;

            default:
                g = Instantiate(SPExtra_Point, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                break;
        }
        return g;
    }
    //_____________________________________________________________________________


    
    public void Messagetoconsole(string a)
    {
        //MichaelH: send a string, IE a message to the unity object i created within singleplayer scene. It is called console and has the tag console.  
        //purpose is to update the player / user with messages so it isn't just.. implied that they know what is happening.
        print(a);
        Debug.Log(a);
        TextMeshProUGUI t = GameObject.FindWithTag("console").GetComponent<TextMeshProUGUI>();
        t.text = a;
    }

    public void Hault(int sleepnumberrecieved)
    {
        //send a single whole number, it's multiplied by a 1000 for the miliseconds.
        //MichaelH: send a int, IE a number to sleep / pause operations.  Typically called before AIwins or player wins
        //I was adding the System.Threading; to every file but this is just easier i think
        //if you need to access this outside the current file, just make a variable refrencing this class o.0
        Thread.Sleep(sleepnumberrecieved*1000);
        return;
    }

    public bool PlayerBlock()//check whether the player needs to block (if their opponent is over 21 and about to win)
    {

        return PlayerMustBlock;
    }
    public bool AIBlock()//check whether the AI needs to block
    {
        return AIMustBlock;
    }

    public void setPlayerBlock(bool b)//used to set playerblock if the player's next card needs to put AI score under 21 to avoid losing. Also to reset if the player has blocked
    {
        PlayerMustBlock = b;
    }
    public void setAIBlock(bool b)//used to set AIblock if the AI's next card needs to put player score under 21 to avoid losing. also to reset if the AI has blocked
    {
        AIMustBlock = b;
    }
    //______________________________________________________________________________
    public double getTurn()//returns the round number for determining who's turn it is
    {
        return gameTurn;
    }
    public void nextTurn()//advances the round number
    {
       
        gameTurn++; //the player cards check this value to see if the player is allowed to play a card they clicked on, this script checks that value in update...
        //            to make an action when it's turn is up. The players turn is every odd number, the computer is every even number. 
        print("GameTurn: " + gameTurn);

    }
    public void halfNextTurn()
    {
        gameTurn += .5;
        print("GameTurn: " + gameTurn);    

    }
    //________________________________________________________________
    public bool getBlitz()
    {
        return Blitz;
    }
    public void setBlitz(bool b)
    {
        Blitz = b;
    }
    //_________________________________________________________________
    public void playerWin()
    {
        print("AI score: " + GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>().text + "   Player score: " + GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>().text);        
        SceneManager.LoadScene("SPEndScreenV");
        //GameObject.FindWithTag("PlayerWin").SetActive(true);
    }
    public void AIWin()
    {

        Messagetoconsole("Opponent wins!");
        Hault(2);

        print("AI score: " + GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>().text + "   Player score: " + GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>().text);

        SceneManager.LoadScene("SPEndScreenL");
        //GameObject.FindWithTag("PlayerLoss").SetActive(true);
    }

    //_________________________________________________________________
    public GameObject getLastPlayed()// when offensive cards are played, they are renamed to a number, this returns the number of the most recently played card for easy lookup
    {
        return lastPlayed;// it's minus 1 because I increment cardPlayed after setting the name in the next function
    }
    public void setLastPlayed(GameObject g) //changes the name of played cards to a number that is incremented. this allows the card to be found with a search by name
    {
        lastPlayed = g;
    }

    public void setLastPlayedAI(GameObject g)
    {
        lastPlayedAI = g;
    }
    public GameObject getLastPlayedAI()
    {
        return lastPlayedAI;
    }
    //__________________________________________________________________
    public void AIdraw()
    {
        //print("Ai drew: " + deck[deckPosition]);
        if(deckPosition > 99)
        {
            print("deckPosition: "+ deckPosition);
            buildDeck();
            deckPosition = 0;
        }
        AIhand[AIhandindex++] = deck[deckPosition++];
    }

    public void PScoreChange(int a)
    {
        TextMeshProUGUI t = GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>();
        t.text = (int.Parse(t.text) + a).ToString();
    }
    public void EScoreChange(int a)
    {
        TextMeshProUGUI t = GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>();
        t.text = (int.Parse(t.text) + a).ToString();
    }


    public int GetAIScore()
    {
        return int.Parse(GameObject.FindWithTag("Escore").GetComponent<TextMeshProUGUI>().text);
    }
    public int GetPlayerScore()
    {
        return int.Parse(GameObject.FindWithTag("Pscore").GetComponent<TextMeshProUGUI>().text);
    }

    //____________________________________________________________________________________________________

    public void AIPlay()//this is to determine which card the AI will play. called in update()
    {
        


        if (GetAIScore() >= 21)
        {
            
            AIWin();
            return;
        }

        lastPlayedAI = null;//prevents player from blocking cards played on previous turns
        if (AIBlock())//if the AI needs to block a win from the player
        {
            string firstLetter = (lastPlayed.name.Substring(2, 1));

            switch (firstLetter) // Animations added
            {
                case "r"://rushing td
                    for (int i = 0; i < 100; i++)//searches through the AI hand
                    {
                        if (AIhand[i] == 1) // ai blocked players card with a tackle card
                        {
                            print("GameManager: AiBlock : Case r : aihand[i] == 1"); //debug purps

                            Messagetoconsole("Opponet blocked your Rushing Touchdown");
                            AIhand[i] = 0;//take the card out of hand
                            GameObject card = Instantiate(SPTackle, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());
                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                            return;
                        }
                    }
                    break;
                case "p"://passing td
                    for (int i = 0; i < 100; i++)
                    {
                        if (AIhand[i] == 2)
                        {
                            Messagetoconsole("Opponet blocked your Passing Touchdown");
                            AIhand[i] = 0;//take the card out
                            GameObject card = Instantiate(SPInterception, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());
                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                            return;
                        }
                    }
                    break;
                case "h"://hail mary
                         //can't be blocked, only blitzed
                    Messagetoconsole("Opponet Can Not Block a Hail Mary");

                    break;
                case "c"://conversion
                    for (int i = 0; i < 100; i++)
                    {
                        if (AIhand[i] == 1)
                        {
                            Messagetoconsole("Opponet blocked your Conversion");
                            AIhand[i] = 0;//take the card out of hand
                            GameObject card = Instantiate(SPTackle, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());
                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                            /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                            Destroy(lastPlayed.gameObject);
                            lastPlayed = null;
                            GameObject card = Instantiate(SPTackle, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;//create the card to discard
                            card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                            card.tag = "discard";
                            Destroy(card.GetComponent<CardHover>());*/
                            return;
                        }
                        if (AIhand[i] == 2)
                        {
                            Messagetoconsole("Opponet blocked your Conversion");

                            AIhand[i] = 0;//take the card out
                            GameObject card = Instantiate(SPInterception, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());
                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                            /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                            Destroy(lastPlayed.gameObject);
                            lastPlayed = null;
                            GameObject card = Instantiate(SPInterception, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                            card.tag = "discard";
                            Destroy(card.GetComponent<CardHover>());*/
                            return;
                        }
                    }

                    break;
                case "f":
                    for (int i = 0; i < 100; i++)
                    {
                        if (AIhand[i] == 3)
                        {
                            Messagetoconsole("GameManager : case f aihand 3");
                            Hault(2);
                            AIhand[i] = 0;//take the card out
                            GameObject card = Instantiate(SPBlocked_Kick, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());
                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                            /*PScoreChange(-1 * int.Parse(lastPlayed.tag));
                            Destroy(lastPlayed.gameObject);
                            lastPlayed = null;
                            GameObject card = Instantiate(SPBlocked_Kick, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);
                            card.tag = "discard";
                            Destroy(card.GetComponent<CardHover>());*/
                            return;
                        }
                    }

                    break;
                case "e":
                    for (int i = 0; i < 100; i++)
                    {
                        if (AIhand[i] == 3)
                        {
                            Messagetoconsole("GameManager: case e aihand 3");
                            Hault(2);
                            AIhand[i] = 0;//take the card out
                            GameObject card = Instantiate(SPBlocked_Kick, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            card.transform.position = new Vector3(540, 730, 0);
                            Destroy(card.GetComponent<CardHover>());
                            StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                            return;
                        }
                    }
                    break;
            }

    
            //if this switch fails, the card must be blitzed | Animations added
            for (int i = 0; i < 100; i++)
            {
                if (AIhand[i] == 4) //it is a blitz card
                {
                    if (GetPlayerScore() == 0)//Michael:  When the computer trys to play a blitz card check that the players score is 0 or not 0
                    {
                        print("Computer Tried to Blitz but failed because the players score is 0");
                    }
                    else
                    {
                        print("Computer played Blitz");
                        Messagetoconsole("Opponent played a Blitz Card");
                        AIhand[i] = 0;
                        GameObject g = Instantiate(SPBlitz, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                        g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                        g.transform.position = new Vector3(540, 730, 0);
                        Destroy(g.GetComponent<CardHover>());
                        StartCoroutine(AIBlitzAndDiscardAnimation(g, lastPlayed, animationSpeed));
                        return;
                    }
                  
                }
            }


            //if there is no blitz, the player wins
            playerWin();
            return;

        }
        else//this means there is no priority for what the AI plays
        {
            
            if (lastPlayed != null)//if there is a card to be blocked
            {
                //try to play defensive cards, if any are owned | Animations updated
                string firstLetter = (lastPlayed.name.Substring(2, 1));
                switch (firstLetter)
                {
                    case "r"://rushing td
                        for (int i = 0; i < 100; i++)//searches through the AI hand
                        {
                            if (AIhand[i] == 1)
                            {
                                Messagetoconsole("Opponet blocked your Rushing Touchdown");

                                AIhand[i] = 0;//take the card out of hand
                                GameObject card = Instantiate(SPTackle, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                                return;
                            }
                        }

                        break;
                    case "p"://passing td
                        for (int i = 0; i < 100; i++)
                        {
                            if (AIhand[i] == 2)
                            {
                                Messagetoconsole("Opponet blocked your Passing Touchdown! ");

                                AIhand[i] = 0;//take the card out
                                GameObject card = Instantiate(SPInterception, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                      
                                return;
                            }
                        }

                        break;
                    case "h"://hail mary
                             //can't be blocked, only blitzed
                              Messagetoconsole("Opponet can not block hail mary -- update case ");
                        break;

                    case "c"://conversion
                        for (int i = 0; i < 100; i++)
                        {
                            if (AIhand[i] == 1) //
                            {
                                Messagetoconsole("Opponet Played Tackle and defended against you.");
                                AIhand[i] = 0;//take the card out of hand
                                GameObject card = Instantiate(SPTackle, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                                return;
                            }
                            if (AIhand[i] == 2)
                            {//interception card
                                Messagetoconsole("Opponet Played Interception");

                                AIhand[i] = 0;//take the card out
                                GameObject card = Instantiate(SPInterception, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                                return;
                            }
                        }

                        break;
                    case "f":
                        for (int i = 0; i < 100; i++)
                        {
                            if (AIhand[i] == 3)
                            {
                                Messagetoconsole("Opponet played Blocked kick ");

                                AIhand[i] = 0;//take the card out

                                GameObject card = Instantiate(SPBlocked_Kick, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));
                                return;
                            }
                        }

                        break;
                    case "e":
                        for (int i = 0; i < 100; i++)
                        {
                            if (AIhand[i] == 3)
                            {
                                Messagetoconsole("Opponet blocked your kick!");

                                AIhand[i] = 0;//take the card out

                                GameObject card = Instantiate(SPBlocked_Kick, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                card.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                card.transform.position = new Vector3(540, 730, 0);
                                Destroy(card.GetComponent<CardHover>());
                                
                                StartCoroutine(AIBlockAndDiscardAnimation(card, lastPlayed, animationSpeed));

                               
                                return;
                            }
                        }

                        break;

                }

                var r = Random.Range(1, 4);//flip a coin to decide whether to try and blitz, if Ai even has one
                if (r == 1)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        if (AIhand[i] == 4)
                        {
                            if (GetPlayerScore() == 0)//Michael:  When the computer trys to play a blitz card check that the players score is 0 or not 0
                            {
                                print("Computer Tried to Blitz but failed because the players score is 0");
                            }
                            else
                            {
                                print("Computer Played Blitz");
                                Messagetoconsole("Opponet played a Blitz Card, taking one of your Offensive cards!");
                                AIhand[i] = 0;
                                GameObject g = Instantiate(SPBlitz, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                                g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                                g.transform.position = new Vector3(540, 730, 0);
                                Destroy(g.GetComponent<CardHover>());
                                StartCoroutine(AIBlitzAndDiscardAnimation(g, lastPlayed, animationSpeed));
                                return;
                            }
                            
                        }
                    }
                }
            }

            //play offensive cards, if owned | Animations updated
            for (int i = 0; i < 100; i++)
            {
                if (AIhand[i] >= 9 && AIhand[i] <= 14)
                {

                    GameObject g;
                    switch (AIhand[i])
                    {
                        case 9:
                            Messagetoconsole("Opponet played An Offensive Conversion!");
                            AIhand[i] = 0;
                            g = Instantiate(SPConversion, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));
                            return;
                        case 10:
                            Messagetoconsole("Opponet played field goal, what a kick!");
                            AIhand[i] = 0;
                            g = Instantiate(SPField_Goal, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));
                            return;
                        case 11://extra point
                            Messagetoconsole("Opponet played Extra point!");
                          
                            AIhand[i] = 0;
                            g = Instantiate(SPExtra_Point, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));
                            /*g.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
                            EScoreChange(int.Parse(g.tag));
                            setLastPlayedAI(g);
                            g.AddComponent<Blitzable>();*/
                            return;
                        case 12:
                            ;//flip a coin to decide whether to try and blitz, if Ai even has one
                            var r = Random.Range(1, 4);
                            if (r == 1)
                            {
                                Messagetoconsole("Opponet played a HailMary! Things are heating up!");
                            }
                            else
                            {
                                Messagetoconsole("Opponet played a HailMary!");
                            }

                            AIhand[i] = 0;
                            g = Instantiate(SPHail_Mary, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));
                            /*g.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
                            EScoreChange(int.Parse(g.tag));
                            setLastPlayedAI(g);
                            g.AddComponent<Blitzable>();*/
                            return;
                        case 13://opponet plays passing td
                            Messagetoconsole("Opponet gained a touch down with a pass!");
                            AIhand[i] = 0;
                            g = Instantiate(SPPassing_TD, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));
                            /*g.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
                            EScoreChange(int.Parse(g.tag));
                            setLastPlayedAI(g);
                            g.AddComponent<Blitzable>();*/
                            return;
                        case 14:
                            Messagetoconsole("Opponet rushed a touchdown!");
                            AIhand[i] = 0;
                            g = Instantiate(SPRushin_TD, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            StartCoroutine(AIOffensiveAnimation(g, animationSpeed));
                            /*g.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
                            EScoreChange(int.Parse(g.tag));
                            setLastPlayedAI(g);
                            g.AddComponent<Blitzable>();*/
                            return;
                    }
                }
            }

            //play cards to draw | Animations updated | audio added
            for (int i = 0; i < 100; i++)
            {
                if (AIhand[i] >= 6 && AIhand[i] <= 8)
                {

                    GameObject g;
                    switch (AIhand[i])
                    {

                        case 6:
                            Messagetoconsole("Opponet completed a pass and gained a new card!");
                            AIhand[i] = 0;
                            g = Instantiate(SPPass_Completion, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            AIdraw();//draw 1
                            g.tag = "audio";
                            StartCoroutine(AIDiscardAnimation(g, animationSpeed));
                            return;

                        case 7:
                            Messagetoconsole("Opponet played First Down and gained a new card!");
                          
                            AIhand[i] = 0;
                            g = Instantiate(SPFirst_Down, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            AIdraw();//first down is draw 2
                            AIdraw();
                            g.tag = "audio";
                            StartCoroutine(AIDiscardAnimation(g, animationSpeed));

                            return;

                        case 8:
                            Messagetoconsole("Opponet gained 5 yards and gained a new card!");
                            
                            AIhand[i] = 0;
                            g = Instantiate(SP5Yard_Run, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                            g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                            g.transform.position = new Vector3(540, 730, 0);
                            Destroy(g.GetComponent<CardHover>());
                            AIdraw();
                            g.tag = "audio"; 
                            StartCoroutine(AIDiscardAnimation(g, animationSpeed));
                            return;

                    }
                }
            }
            //play cards to skip player turn | Animation updated | audio added
            for (int i = 0; i < 100; i++)
            {
                if (AIhand[i] == 5)
                {
                    Debug.Log("GameManager==5");

                    Messagetoconsole("Opponet changed up the plays! Gained a turn and another card!");

                    AIhand[i] = 0;
                    GameObject g = Instantiate(SPFumble, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                    g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                    g.transform.position = new Vector3(540, 730, 0);
                    Destroy(g.GetComponent<CardHover>());
                    nextTurn();//skips to next turn, then update() switches turn again, so AI will go twice
                    g.GetComponent<AudioSource>().Play();
                    StartCoroutine(AIDiscardAnimation(g, animationSpeed));
                    return;
                }
                else if (AIhand[i] == 15)//end of quarter
                {
                    Debug.Log("GameManager==15");
                    Messagetoconsole("Opponet changed up the plays! Gained a turn and another card!");

                    Hault(2);
                    AIhand[i] = 0;
                    GameObject g = Instantiate(SPEnd_Of_Quarter, new Vector3(178, 517, 0), Quaternion.identity) as GameObject;
                    g.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
                    g.transform.position = new Vector3(540, 730, 0);
                    Destroy(g.GetComponent<CardHover>());
                    nextTurn();//skips to next turn, then update() switches turn again, so AI will go twice
                    g.GetComponent<AudioSource>().Play();
                    StartCoroutine(AIDiscardAnimation(g, animationSpeed));
                    return;
                }
            }
            //probably won't get this far, but skip the AI turn if it does
            halfNextTurn();
            Debug.Log("COMPUTER COULD NOT PLAY -- SKIPPED");
            Messagetoconsole("Opponet could not play, skipped");
            return;
        }
        
    }  
    
    IEnumerator AIOffensiveAnimation(GameObject card, float speed) //added audio
    {

        Vector3 targetPosition = GameObject.FindWithTag("AIScored").transform.position;
        float rotation = 0;
        while (card.transform.position.x != targetPosition.x && card.transform.position.y != targetPosition.y)
        {
            if (card.transform.rotation != Quaternion.Euler(0, 0, 90))
            {
                card.transform.rotation = Quaternion.Euler(0, 0, rotation);
                rotation += .25f;
            }
            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        card.transform.rotation = Quaternion.identity;
        card.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);

        card.GetComponent<AudioSource>().Play();
        EScoreChange(int.Parse(card.tag));
        setLastPlayedAI(card);
        card.AddComponent<Blitzable>();
        
        if (GetAIScore() >= 21)
        {
            setPlayerBlock(true);
        }
        else
        {
            setPlayerBlock(false);
        }
        halfNextTurn();
        //.................................
    }

    //____________________________________________________________________________________________________
     IEnumerator AIBlitzAndDiscardAnimation(GameObject card, GameObject lastPlayedCard, float speed)
    {
        yield return StartCoroutine(AIBlitzAnimation(card, lastPlayedCard, speed));
        
        yield return StartCoroutine(AIDiscardAnimation(card, speed));
    }


     IEnumerator AIBlitzAnimation(GameObject card, GameObject lastPlayedCard, float speed) //added audio
    {
        Vector3 targetPosition = lastPlayedCard.transform.position;

        while (card.transform.position.x != targetPosition.x && card.transform.position.y != targetPosition.y)//comparing vector3's didnt work for some reason :)
        {
            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPosition, speed * Time.deltaTime);
            
            yield return null;
        }
        card.GetComponent<AudioSource>().Play();
        setLastPlayedAI(lastPlayedCard);
        PScoreChange(-1 * int.Parse(lastPlayedCard.tag));//subtract the points from player's score
        EScoreChange(int.Parse(lastPlayedCard.tag));
        lastPlayedCard.transform.SetParent(GameObject.FindWithTag("AIScored").transform, false);
    }


     IEnumerator AIBlockAndDiscardAnimation(GameObject card, GameObject lastPlayedCard, float speed)
    {
        yield return StartCoroutine(AIBlockAnimation(card, lastPlayedCard, speed));
        
        yield return StartCoroutine(AIDiscardAnimation(card, speed));
    }


    IEnumerator AIBlockAnimation(GameObject card, GameObject lastPlayedCard, float speed) //added audio
    {

        Vector3 targetPosition = lastPlayedCard.transform.position;
        

        while (card.transform.position.x != targetPosition.x && card.transform.position.y != targetPosition.y)//comparing vector3's didnt work for some reason :)
        {

            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }

        card.GetComponent<AudioSource>().Play();

        PScoreChange(-1 * int.Parse(lastPlayedCard.tag));//subtract the points from player's score
        Destroy(lastPlayedCard.gameObject);
    }


    IEnumerator AIDiscardAnimation(GameObject card, float speed)
    {
        Vector3 targetPosition = GameObject.FindWithTag("Discard Pile").transform.position;
        while (card.transform.position.x != targetPosition.x && card.transform.position.y != targetPosition.y)
        {
            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        if (card.tag == "audio")
        {
            card.GetComponent<AudioSource>().Play();
        }


        card.transform.SetParent(GameObject.FindWithTag("Discard Pile").transform, false);

        card.tag = "discard";


        if (GetPlayerScore() >= 21) //check these and add them to player cards after animations
        {
            Messagetoconsole("You have Won!");
            playerWin();
        }
        else
        {
            setAIBlock(false);
            if (GetAIScore() >= 21)
            {setPlayerBlock(true);}
            else{setPlayerBlock(false);}
            halfNextTurn();//half the turn, making it the players turn now

        }



    }
    
    




    IEnumerator Delaytime(float time)
    {

        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(time);
        Debug.Log("Fin Coroutine at timestamp : " + Time.time);

    }


    IEnumerator DelayWinOrLossSceneChange(float time)
    {//use this to slow the win / loss down so the player can see what happened before just being tossed to the next scene
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(time);
        Debug.Log("Fin Coroutine at timestamp : " + Time.time);
    }


    IEnumerator AIPlayDelayed(float time)
    {
        yield return new WaitForSeconds(time); 
        // Code to execute after the delay  - MH - MUST BE EXECUTED HERE  If executed in update, after this call, the AI won't wait correctly.
        AIPlay();       
        GameObject newCard = draw();      //Draw card for player
        newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
        lastPlayed = null; // prevents AI from blocking cards played on previous turns, only newest card can be blocked
        //Messagetoconsole("Players Turn");
    }


    private void Update()
    {
        if (gameTurn %2 == 0 && gameTurn % 1 == 0)
        {//gameturn is the AI's, this means you advanced the turn, as the starting turn is yours.
            halfNextTurn();// AI halves the turn, preventing Player from using cards
            AIdraw();//AI draws a card
            //halfNextTurn();// AI halves the turn, preventing Player from using cards
            int randomtime = Random.Range(1, 3); // 1-2, not including 3. (Exlcusive)
            //Messagetoconsole(randomtime+"");// testing that the time was correct, delete if desired
            StartCoroutine(AIPlayDelayed(randomtime) ); //wait 1-2 seconds, then run the AIPlay code, in that order   
        }
        

    }




}




