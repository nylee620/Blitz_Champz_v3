using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using TMPro;
using System;
//Author - Michael Harrison

public class WinsLossTracker : MonoBehaviour
{

    public void Start()
    {
        //start frame, update with new and current scores
        print("Start: WinLossTracker.cs");
        int wins = 0; //starting numbers of wins
        int loses = 0;//starting numbers of loses
        int counter = 0;//counter for reading the text file line by line. Line 1 is wins, line 2 is loses
        // Read the file and display it line by line.  
        foreach (string line in System.IO.File.ReadLines("Assets/winsandloses.txt"))
        {
            if (counter == 0)
            {
                wins = Int32.Parse(line);
                print("Start Frame Wins: " + wins);
            }
            if (counter == 1)
            {
                loses = Int32.Parse(line);
                print("Start Frame Loses: " + loses);
            }
            counter++;
        }
        //update the first start frame of the game with these values:)
        TextMeshProUGUI t = GameObject.FindWithTag("losesCount").GetComponent<TextMeshProUGUI>();
        t.text = loses.ToString();
        TextMeshProUGUI w = GameObject.FindWithTag("WinsCount").GetComponent<TextMeshProUGUI>();
        w.text = wins.ToString();
    }

    public int getwins()
    {
          //return the wins within the text file

        print("Getting wins");
        int wins = 0; //starting numbers of wins
        int counter = 0;//counter for reading the text file line by line. Line 1 is wins, line 2 is loses
        foreach (string line in System.IO.File.ReadLines("Assets/winsandloses.txt"))
        {
            if (counter == 0)
            {
                wins = Int32.Parse(line);
            }
            counter++;
        }
        print("Getwins : "+wins);
        return wins;
    }

    public int getloses()
    {
        //return the loses within the text file
        print("Getting wins");
        int loses = 0; //starting numbers of wins
        int counter = 0;//counter for reading the text file line by line. Line 1 is wins, line 2 is loses
        foreach (string line in System.IO.File.ReadLines("Assets/winsandloses.txt"))
        {
            if (counter == 1)
            {
                loses = Int32.Parse(line);
            }
            counter++;
        }
        print("Getloses: " + loses);
        return loses;


    }

    public void addWin()
    {
        //add a win to your game score.

        print("Called addwin from gamemanger");
        int addedwin;
        addedwin = getwins();
        addedwin += 1;
        print(addedwin);
        print("new wins :"+ addedwin);
        lineChanger(addedwin.ToString(), "Assets/winsandloses.txt",  1);
    }

    public void addLose()
    {
        //add a lose to your game score.
        print("Called addlose from gamemanger");
        int addedlose;
        addedlose = getloses();
        addedlose += 1;
        print(addedlose);
        print("new loses :" + addedlose);
        lineChanger(addedlose.ToString(), "Assets/winsandloses.txt", 2);
    }

    static void lineChanger(string newText, string fileName, int line_to_edit)
    {
        //call this to change the lines inside the file holding variables
        print("Linechanger line 0");
        string[] arrLine = File.ReadAllLines(fileName);
        print("Linechanger line 1");
        arrLine[line_to_edit - 1] = newText;
        print("Linechanger line 2");
        File.WriteAllLines(fileName, arrLine);
        print("Linechanger line 3");

    }
}



  
