using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region MonoBehavior

    void Start()
    {
       
    }

    void Update()
    {
        
    }

    #endregion
    #region Public Methods

    public void Settings()
    {
        Debug.Log("Showing Settings Menu!");
        UIManager.main.ShowScreen("Settings");
    }

    public void LevelSelection()
    {
        Debug.Log("Showing Level Selection!");
        //UIManager.main.ShowScreen("Level Selection");
        // switch scene to level selection
        SceneManager.LoadScene("LevelSelection_Scene");
    }

    public void ContinueGame()
    {
        UIManager.main.ContinueGame();
    }

    public void Scores()
    {
        UIManager.main.ShowScreen("Scores");
    }

    public void Quit()
    {
        UIManager.main.Quit();
    }

    #endregion

    #region Private Methods

    //IEnumerator DelayUI

    #endregion
}
