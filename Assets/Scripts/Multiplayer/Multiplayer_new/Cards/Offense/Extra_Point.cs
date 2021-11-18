using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra_Point : Offensive_Card
{
    void Start()
    {
        value = 1;
		kick = true;
		pass = false;
		run = false; 
    }
    public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/extra_point");
    }
    void Update()
    {
        
    }
}
