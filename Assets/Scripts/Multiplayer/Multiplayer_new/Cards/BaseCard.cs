using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class BaseCard : MonoBehaviourPunCallbacks {
	public BlitzPlayer owner;
	protected bool valid = true;
	protected bool win_played = false;
	public void SetOwner(BlitzPlayer own) {
		this.owner = own;
		this.GetComponent<PhotonView>().TransferOwnership(own.GetComponent<PhotonView>().Owner);
		if (owner.up) {
			//180f = 0f
			gameObject.transform.rotation = Quaternion.Euler(0,0,0f);
		}
	}
	public virtual bool CheckValid() {
		return valid;
	}
	void Start () {
	}
	[PunRPC]
	public void Discard () {
		if (this.owner != null) {
			for (int i = 0; i < owner.hand.Count; i++) {
				owner.hand[i].GetComponent<SpriteRenderer>().color = Color.white;
			}
			owner.table.last_card = null;
			//This moves card to discard pile, commented out to make animations work *OBSELETE*
			//gameObject.GetComponent<Transform>().position = new Vector3(-1.45f, 0f, 0f);
			gameObject.transform.rotation = Quaternion.Euler(0,0,0f);
			owner.table.Discard(gameObject);
			owner.Remove(gameObject);
			this.owner = null;
			Destroy(GetComponent<BoxCollider>());
			Show();
		}
	}
	public void Hide() {
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/back");
	}
	public virtual void Show() {
	}
	public void AdvanceTurn() {
		owner.table.AdvanceTurn();
	}
	private void OnMouseUpAsButton() {
		//old if (owner != null && owner.table.current_player == owner) {
		if (gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer && owner != null && owner.table.currentBlitzPlayer == owner) {
			//old this.Play();
			//old this.Discard();
			this.GetComponent<PhotonView>().RPC("Play", RpcTarget.All);
			this.GetComponent<PhotonView>().RPC("Discard", RpcTarget.All);
		}
	}
	void OnMouseEnter() {
		//old if (owner != null && owner == owner.table.current_player) {
		if (gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer && owner != null && owner == owner.table.currentBlitzPlayer) {
			if (owner.hand.Contains(gameObject)) {
				//possible error here, if so add this function to replace the one below
				//gameObject.transform.position = new Vector3(gameObject.transform.position.x, owner.transform.position.y, gameObject.transform.position.z) +  Vector3.Scale(owner.transform.up, new Vector3(0f, 0.5f, 0f));
				gameObject.transform.position += Vector3.Scale(transform.up, new Vector3(0f, 0.5f, 0f));
				//gameObject.transform.localScale += Vector3.Scale(transform.up, new Vector3(2f, 2f, 2f));

				gameObject.GetComponent<SpriteRenderer>().sortingOrder +=20;
				for (int i = 0; i < owner.hand.Count; i++) {
					if (owner.hand[i] != gameObject) {
						if (owner.hand[i].GetComponent<BaseCard>().win_played) {
							gameObject.GetComponent<SpriteRenderer>().color = Color.white;
						} else {
						owner.hand[i].GetComponent<SpriteRenderer>().color = Color.gray;
						}
					}
				}
			} else {
				foreach(BlitzPlayer a in owner.table.listBlitzPlayers) {
					if (owner != a && a.field.Contains(gameObject)) {
						foreach(BlitzPlayer b in owner.table.listBlitzPlayers) {
							if (b != a) {
								for (int i = 0; i < b.field.Count; i++) {
									if (b.field[i] != gameObject) {
										b.field[i].GetComponent<SpriteRenderer>().color = Color.gray;
									}
								}
							}
						}
						gameObject.GetComponent<SpriteRenderer>().sortingOrder +=20;
						for (int i = 0; i < a.field.Count; i++) {
							if (a.field[i] != gameObject) {
								a.field[i].GetComponent<SpriteRenderer>().color = Color.gray;
							}
						}
					}
				}
			}
		}
	}
	void OnMouseExit() {
		//old if (owner != null && owner == owner.table.current_player) {
		if (gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer && owner != null) {
			if (owner.hand.Contains(gameObject)) {
				//possible error here, if so replace lower with this
				//gameObject.transform.position = new Vector3(gameObject.transform.position.x, owner.transform.position.y, gameObject.transform.position.z);
				gameObject.transform.position -= Vector3.Scale(transform.up, new Vector3(0f, 0.5f, 0f));
				gameObject.GetComponent<SpriteRenderer>().sortingOrder -=20;
				if (!win_played){
					for (int i = 0; i < owner.hand.Count; i++) {
						owner.hand[i].GetComponent<SpriteRenderer>().color = Color.white;
					}
				} else {
					gameObject.GetComponent<SpriteRenderer>().color = Color.white;
				}
			} else {
				foreach(BlitzPlayer a in owner.table.listBlitzPlayers) {
					if (owner != a && a.field.Contains(gameObject)) {
						foreach(BlitzPlayer b in owner.table.listBlitzPlayers) {
							if (b != a) {
								for (int i = 0; i < b.field.Count; i++) {
									if (b.field[i] != gameObject) {
										b.field[i].GetComponent<SpriteRenderer>().color = Color.white;
									}
								}
							}
						}
						gameObject.GetComponent<SpriteRenderer>().sortingOrder -=20;
						for (int i = 0; i < a.field.Count; i++) {
							a.field[i].GetComponent<SpriteRenderer>().color = Color.white;
						}
					}
				}
			}
		}
	}
	[PunRPC]
	protected virtual void Play () {
	}
	void Update () {
		
	}
}