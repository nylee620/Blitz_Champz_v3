using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleCard : Defensive_Card
{
    void Start()
    {
        run = true;
        //Animation parameters
		target = new Vector3(-1.45f, 0f, 0f);
        position = gameObject.transform.position;
    }
	public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/tackle");
    }
    void Update()
    {
        
    }
}
