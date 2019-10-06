using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        UIManager.main.ShowScreen("Settings");
    }

    public void LevelSelection()
    {
        UIManager.main.ShowScreen("Level Selection");
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
}
