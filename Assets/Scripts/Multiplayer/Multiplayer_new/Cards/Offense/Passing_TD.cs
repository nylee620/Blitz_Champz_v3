using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passing_TD : Offensive_Card
{
    // Start is called before the first frame update
    void Start()
    {
        value = 6;
		kick = false;
		pass = true;
		run = false;
    }
	public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/passing_td");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
