// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class HoverBounce : MonoBehaviour
// {

//     private Animator myAnimator; // this is a class variable for this functions to access it

//     // Start is called before the first frame update
//     void Start()
//     {
//         myAnimator = GetComponent<Animator>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     void OnMouseOver()
// 	{
//     //If your mouse hovers over the GameObject with the script attached, output this message
//         myAnimator.SetBool("isBouncing", true);
// 	}

// 	void OnMouseExit()
// 	{
//     //The mouse is no longer hovering over the GameObject so output this message each frame
//     myAnimator.SetBool("isBouncing", false);

// 	}

// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBounce : MonoBehaviour
{
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        myAnimator.SetBool("isBouncing", true);
    }

    public void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        myAnimator.SetBool("isBouncing", false);
    }
}
