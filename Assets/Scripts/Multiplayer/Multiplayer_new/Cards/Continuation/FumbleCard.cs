using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FumbleCard : Continuation_Card
{
    //Get the AudioSource for each Offensive card
	private AudioSource source;
    //animation
    public float speed = .5f;
    private Vector3 target;
    private Vector3 position;
    void Start()
    {
        target = new Vector3(-1.45f, 0f, 0f);
        position = gameObject.transform.position;
    }
    [PunRPC]
    protected override void Play() {
        StartCoroutine(MoveTo());
        owner.table.Skip();
        //When the card is played, play the sound attached to it
		source = GetComponent<AudioSource>();
		source.Play();
        AdvanceTurn();
    }
	public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/fumble");
    }
    void Update()
    {
        
    }
    //MoveTo Coroutine
     IEnumerator MoveTo()
    {
       

        // This looks unsafe, but Unity uses
        // en epsilon when comparing vectors.
        while (transform.position != target)
        {
            Debug.Log("Got to 4 loop");
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                speed);
            // Wait a frame and move again.
            yield return null;
        }
    }
}
