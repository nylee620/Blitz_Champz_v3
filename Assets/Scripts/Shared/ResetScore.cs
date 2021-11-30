using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using TMPro;
using System;
//Author - Michael Harrison
//purpose: when resetscore confirm is pressed, write to file 0's and update the gui text on screen
public class ResetScore : MonoBehaviour
{


    public void ResetScoreConfirmButton()
    {
        print("Called ResetScoreConfirmButton");
        TextMeshProUGUI d = GameObject.FindWithTag("WinsCount").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI c = GameObject.FindWithTag("losesCount").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI a = GameObject.FindWithTag("ResetTrackerUserMessage").GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI b = GameObject.FindWithTag("ResetTrackerConfirmButton").GetComponent<TextMeshProUGUI>();
        // b.text = "Resetting...";

        if (c.text == "0" && d.text == "0"){
                //it's already 0 when the button is pressed
                a.text = "Your score is already 0/0";
        }
        else
        {
            //reset the score
            a.text = "Score Reset In Progress";
            lineChanger("0", "Assets/winsandloses.txt", 1);
            lineChanger("0", "Assets/winsandloses.txt", 2);
            c.text = "0";
            d.text = "0";
            a.text = "Score Reset Complete";
        }


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

