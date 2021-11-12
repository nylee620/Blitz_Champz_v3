using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCompletion : Card
{

    public PassCompletion()
    {
        
    }

    public override void ShowCard()
    {
        Debug.Log("Show Card");
       // Instantiate(passCompletionPrefab, new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(handArea.transform, false);
    }

    public new void Play()
    {
        if (Table.myGamePlayer.myTurn)
        {
            if (Table.myGamePlayer.table.homeLastPlayed.transform.childCount > 0)
            {
                Table.myGamePlayer.table.homeLastPlayed.transform.GetChild(0).transform.SetParent(Table.myGamePlayer.table.homePlayedCards.transform);
            }
            this.transform.SetParent(Table.myGamePlayer.table.homeLastPlayed.transform);
            Table.myGamePlayer.table.DrawCard(2);
        }
    }
}
