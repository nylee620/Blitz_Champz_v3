using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

public class Card : MonoBehaviourPunCallbacks, IPointerEnterHandler, IPointerExitHandler
{
    public GamePlayer gamePlayer;

    public Card()
    {
    }

    public void Play()
    {
        Debug.Log("Play");
    }

    public virtual void ShowCard()
    {

    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
        
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse Exit");
    }

    public virtual GameObject GetPrefab()
    {
        Debug.Log("Returning null type for prefab");
        return null;
    }

    public void AdvanceTurn()
    {
        Table.myGamePlayer.table.AdvanceTurn();
    }

    private void Awake()
    {
        this.transform.SetParent(GameObject.FindWithTag("DeckObject").transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = Table.myGamePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 200, this.transform.position.z);
        if(this.GetComponent<BoxCollider2D>() != null)
        {
            this.transform.GetComponent<BoxCollider2D>().size = new Vector3(0, this.transform.GetComponent<BoxCollider2D>().size.y + 200, 0);
            this.transform.GetComponent<BoxCollider2D>().offset = new Vector3(0, this.transform.GetComponent<BoxCollider2D>().offset.y - 100, 0);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 200, this.transform.position.z);
        if (this.GetComponent<BoxCollider2D>() != null)
        {
            this.transform.GetComponent<BoxCollider2D>().size = new Vector3(0, this.transform.GetComponent<BoxCollider2D>().size.y - 200, 0);
            this.transform.GetComponent<BoxCollider2D>().offset = new Vector3(0, this.transform.GetComponent<BoxCollider2D>().offset.y + 100, 0);
        }
            
    }
}
