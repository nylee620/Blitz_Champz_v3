using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseEnter()//when the mouse enters the game object
    {
        
        if(tag == "discard")//makes sure the card is not discarded
        {
            return;
        }
        if(transform.rotation != Quaternion.Euler(0, 0, 90))//makes sure the card is not sideways, meaning played. I could combine these
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 0);//increases the scale of the card by 10f x and y
            transform.localPosition = new Vector3(transform.localPosition.x, 100, -5);//moves the card up slightly...
            gameObject.layer = 9;
            //          by changing the y, setting the x to be the same, and setting z to a higher negative so it ...
            //          will be displayed above every other card in your hand. otherwise, cards can cover it
        }
        
    }
    private void OnMouseExit()//when the mouse exits the game object
    {
        
        if(tag == "discard")//same as above
        {
            return;
        }
        if (transform.rotation != Quaternion.Euler(0, 0, 90))//same as above
        {
            transform.localScale = new Vector3(1f, 1f, 0);//returns the scale to default
            transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);//resets the position to original
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
