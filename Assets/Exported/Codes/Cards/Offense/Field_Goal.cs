using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field_Goal : Offensive_Card 
{
	void Start () 
	{
		value = 3;
		kick = true;
		pass = false;
		run = false;
	}
	public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/field_goal");
    }
	void Update () {
	
	}
}
