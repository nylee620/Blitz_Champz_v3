using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGoal : Card
{
    private readonly int points = 3;

    public FieldGoal()
    {
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
            Table.myGamePlayer.AddPoints(points);
            AdvanceTurn();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
