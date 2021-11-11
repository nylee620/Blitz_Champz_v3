using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfQuarter2 : Card
{

    public EndOfQuarter2()
    {
    }

    public override void ShowCard()
    {
        Debug.Log("Show Card");
        //Instantiate(endOfQuarter2Prefab, new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(handArea.transform, false);
    }

    public new void Play()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
