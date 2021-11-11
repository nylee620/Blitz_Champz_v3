using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonSelected : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    private void Update()
    {
        if (menu.activeSelf) //if the menu related to the button is active
        {
            this.GetComponent<Image>().color = new Color32(0, 0, 0, 150); //increases alpha on button to show button selected
        }
        else
        {
            this.GetComponent<Image>().color = new Color32(0, 0, 0, 50); //sets alpha back to default on button
        }
    }
}
