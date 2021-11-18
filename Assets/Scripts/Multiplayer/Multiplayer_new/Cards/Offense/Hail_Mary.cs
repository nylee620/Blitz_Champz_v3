using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hail_Mary : Offensive_Card
{
    // Start is called before the first frame update
    void Start()
    {
        value = 8;
		kick = false;
		pass = false;
		run = false;        
    }
    public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/hail_mary");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
