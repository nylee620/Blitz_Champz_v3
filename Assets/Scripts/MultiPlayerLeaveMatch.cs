using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiPlayerLeaveMatch : MonoBehaviour
{
 
    public void LoadScene(string sceneName)//allow unity to access this method and send the scene name i'd like to change to when the multi player button leave match is pressed
    {
        SceneManager.LoadScene(sceneName);
    }
}
