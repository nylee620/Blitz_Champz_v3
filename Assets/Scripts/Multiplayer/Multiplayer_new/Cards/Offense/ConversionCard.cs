using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversionCard : Offensive_Card
{
    void Start()
    {
        value = 2;
		pass = true;
		run = true;
    }
    public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/conversion");
    }
    void Update()
    {
        
    }
}
