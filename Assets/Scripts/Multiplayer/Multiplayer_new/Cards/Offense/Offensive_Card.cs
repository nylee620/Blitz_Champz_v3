using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Offensive_Card : BaseCard {
	protected int value;
	protected bool kick = false;
	protected bool pass = false;
	protected bool run = false;
	//Get the AudioSource for each Offensive card
	private AudioSource source;

	//Animation parameters
	public float speed = 1f;
    private Vector3 target;
    private Vector3 position;
	void Start() {
	}
	public bool GetKick() {
		return kick;
	}
	public bool GetPass() {
		return pass;
	}
	public bool GetRun() {
		return run;
	}
	[PunRPC]
	protected override void Play() {
		//possible transformation here
		owner.field.Add(gameObject);
		owner.hand.Remove(gameObject);
		//When the card is played, play the sound attached to it
		//Currently, this sound plays again when it is stolen with a Blitz card
		source = GetComponent<AudioSource>();
		source.Play();
		owner.table.last_card = this;
		for (int i = 0; i < owner.hand.Count; i++) {
			owner.hand[i].GetComponent<SpriteRenderer>().color = Color.white;
		}
		gameObject.GetComponent<BoxCollider>().enabled = false;
		Show();
		AdvanceTurn();
	}
	public int GetValue() {
		return value;
	}
	public void Remove() { //remove card from the field and discard it thus removing points from that player
		owner.UpdateScore();
		//Animation for discard
		target = new Vector3(-1.45f, 0f, 0f);
        position = gameObject.transform.position;
		StartCoroutine(MoveTo());
		//old Discard();
		this.GetComponent<PhotonView>().RPC("Discard", RpcTarget.All);
	}
	private void OnMouseUpAsButton() {
		//old if (owner != null && owner.table.current_player == owner) {
		if ((gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer && owner != null && owner.table.currentBlitzPlayer == owner) || (owner == owner.table.currentBlitzPlayer && !owner.table.ready && owner.gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer)) { //| (owner == owner.table.current_player) second case is for when blitz is played
			//old this.Play();
			this.GetComponent<PhotonView>().RPC("Play", RpcTarget.All);
		}
	}
	void Update () {
	}

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