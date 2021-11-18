using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rushing_TD : Offensive_Card
{
    // Start is called before the first frame update
    void Start()
    {
        value = 6;
		kick = false;
		pass = false;
		run = true; 
    }
	public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/rushing_td");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
