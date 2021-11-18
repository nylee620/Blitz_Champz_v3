using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class sendmessagetoconsole : MonoBehaviour
{
    public void Messagetoconsole(string a)
    {
        TextMeshProUGUI t = GameObject.FindWithTag("console").GetComponent<TextMeshProUGUI>();
        t.text = a;
        return;
    }
}
