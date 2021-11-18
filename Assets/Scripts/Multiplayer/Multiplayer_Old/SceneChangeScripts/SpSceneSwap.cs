using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpSceneSwap : MonoBehaviour
{
    public void Onclick()
    {
        SceneManager.LoadScene("singleplayer"); //in file>build settings, there is a list of scenes in build with a number, scene 3 is singleplayer. 
    }
}
