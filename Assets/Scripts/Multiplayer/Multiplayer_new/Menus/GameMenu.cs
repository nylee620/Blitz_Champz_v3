using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject loadPlayerOptions;
    public void PlayGame ()
    {
        SceneManager.LoadScene("BlitzChampzGame");
    }
    public void TwoPlayer()
    {
        Manager.PlayerCount = 2;
        PlayGame();
    }
    public void ThreePlayer()
    {
        Manager.PlayerCount = 3;
        PlayGame();
    }
    public void FourPlayer()
    {
        Manager.PlayerCount = 4;
        PlayGame();
    }
    public void OpenPlayerOptions()
    {
        loadPlayerOptions.SetActive(true);
    }

    public void LoadTutorial()
    {
        Application.OpenURL("http://blitzchampz.com/rules/");
    }


    

}
