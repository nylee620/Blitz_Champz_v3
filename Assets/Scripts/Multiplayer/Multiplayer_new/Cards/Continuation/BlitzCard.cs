using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BlitzCard : Continuation_Card
{
    private bool played = false;
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
    public void SetPlayed(bool a) {
		win_played = a;
	}
    public override bool CheckValid() {
        foreach (BlitzPlayer a in owner.table.listBlitzPlayers) {
            if (a != owner) {
                if (a.field.Count > 0) {
                    valid = true;
                    return true;
                }
            }
        }
        valid = false;
        return false;
    }
    [PunRPC]
    protected override void Play() {
        StartCoroutine(SelectCard());
    }
    IEnumerator SelectCard() {
		source = GetComponent<AudioSource>();
		source.Play();
        bool losing = false;
        BlitzPlayer winner = null;
        owner.table.SetReady(false);
        foreach (GameObject card in owner.hand) {
                    card.GetComponent<BoxCollider>().enabled = false;
        }
        foreach (BlitzPlayer a in owner.table.listBlitzPlayers) {
            if (owner != a && a.UpdateScore() >= 21) {
                losing = true;
                winner = a;
            }
        }
        foreach (BlitzPlayer a in owner.table.listBlitzPlayers) {
            if (owner != a) {
                if (losing && a == winner) {
                    int temp_score = a.UpdateScore();
                    foreach (GameObject card in a.field) {
                        if ((temp_score - 21) < card.GetComponent<Offensive_Card>().GetValue()){
                            card.GetComponent<BaseCard>().owner = owner;
                            card.GetComponent<PhotonView>().RequestOwnership();
                            card.GetComponent<BoxCollider>().enabled = true;
                        }
                    }
                } else if (!losing) {
                    foreach (GameObject card in a.field) {
                        card.GetComponent<BaseCard>().owner = owner;
                        card.GetComponent<PhotonView>().RequestOwnership();
                        card.GetComponent<BoxCollider>().enabled = true;
                    }
                }
            }
        }
        played = true; //this is used to disable the OnMouseExit() method so that it doesn't lighten the cards
        gameObject.GetComponent<BoxCollider>().enabled = false;
        //old method Show();
        photonView.RPC("Show", RpcTarget.All);
        gameObject.transform.position += Vector3.Scale(transform.up, new Vector3(0f, 0.5f, 0f));
        gameObject.GetComponent<SpriteRenderer>().sortingOrder +=20;
        for (int i = 0; i < owner.hand.Count; i++) {
            if (owner.hand[i] != gameObject) {
                owner.hand[i].GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
        yield return new WaitUntil(() => owner.table.currentBlitzPlayer != owner);
        foreach (BlitzPlayer a in owner.table.listBlitzPlayers) {
            GameObject stolenCard = null;
            if (owner != a) {
                foreach (GameObject card in a.field) {
                    if (owner.field.Contains(card)) {
                        stolenCard = card;
                    } else {
                        card.GetComponent<BaseCard>().owner = a;
                    }
                    card.GetComponent<BoxCollider>().enabled = false;
                    card.GetComponent<SpriteRenderer>().color = Color.white;
                }
                if (stolenCard) {
                    a.field.Remove(stolenCard);
                    a.OrderField();
                }
            }
        }
        owner.ReclaimOthers();
        for (int i = 0; i < owner.hand.Count; i++) {
			owner.hand[i].GetComponent<SpriteRenderer>().color = Color.white;
		}
        foreach (GameObject card in owner.hand) {
            card.GetComponent<BoxCollider>().enabled = true;
        }
        owner.OrderField();
        owner.table.SetReady(true);
        this.Discard();
    }
    private void OnMouseUpAsButton() {
		bool canPlay = CheckValid();
        //old if (owner != null && owner.table.current_player == owner) {
        if (gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer && owner != null && owner.table.currentBlitzPlayer == owner) {
			if (canPlay) {
                //old StartCoroutine(SelectCard());
                photonView.RPC("Play", RpcTarget.All);
            }
            else {
                if (owner.GetValid()) {
                	Debug.Log("Not a valid move");
				} else {
					AdvanceTurn();
					this.Discard();
				}
            }
		}
	}
    void OnMouseExit() {
		//old if (owner != null && owner == owner.table.current_player && !played) {
        if (gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer && owner != null && !played) {
			if (owner.hand.Contains(gameObject)) {
				gameObject.transform.position -= Vector3.Scale(transform.up, new Vector3(0f, 0.5f, 0f));
				gameObject.GetComponent<SpriteRenderer>().sortingOrder -=20;
				for (int i = 0; i < owner.hand.Count; i++) {
					owner.hand[i].GetComponent<SpriteRenderer>().color = Color.white;
				}
			} else {
				foreach(BlitzPlayer a in owner.table.listBlitzPlayers) {
					if (owner != a && a.field.Contains(gameObject)) {
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
	public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/blitz");
    }
    public new void Discard () {
		if (this.owner != null) {
			for (int i = 0; i < owner.hand.Count; i++) {
				owner.hand[i].GetComponent<SpriteRenderer>().color = Color.white;
			}
            //Old transformation call, commented out to make animation work correctly
			//gameObject.GetComponent<Transform>().position = new Vector3(-1.45f, 0f, 0f);
			gameObject.transform.rotation = Quaternion.Euler(0,0,0f);
            //Animation
            StartCoroutine(MoveTo());
			owner.table.Discard(gameObject);
			owner.Remove(gameObject);
			this.owner = null;
			Destroy(GetComponent<BoxCollider>());
			//old Show();
            photonView.RPC("Show", RpcTarget.All);
		}
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
