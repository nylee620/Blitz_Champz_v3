using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartButton : MonoBehaviour
{
    public GameObject PlayerArea;
    public GameObject EnemyArea;

    public GameObject PlayerScored;
    public GameObject AIScored;
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject PlayerScorePlacement = Instantiate(PlayerScored, new Vector3(-105, -111, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
        PlayerScorePlacement.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
        GameObject AIScorePlacement = Instantiate(AIScored, new Vector3(-105, 110, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
        AIScorePlacement.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
        GameObject PlayerHand = Instantiate(PlayerArea, new Vector3(-2.9f, -270f, 0), Quaternion.identity) as GameObject;//creates the transparent player area that holds the cards
        PlayerHand.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);//sets the parent of the object to the canvas 
        
    }

    public void Onclick()
    {

        GameObject g = GameObject.FindWithTag("Manager");
        GameManager p = (GameManager)g.GetComponent(typeof(GameManager));

        for (var i = 0; i < 6; i++) //this instantiates the cards for the beginning player hand (max capacity, not accurate starting hand size)
        {

            GameObject newCard = p.draw();
            newCard.transform.SetParent(GameObject.FindWithTag("PlayerArea").transform, false);
            

        }

        this.gameObject.SetActive(false); //this makes the button disappear once it has completed the startup tasks.
        p.Messagetoconsole("Select a card to play!");
    }

}
