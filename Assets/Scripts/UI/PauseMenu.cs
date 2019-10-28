using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    #region MonoBehavior

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public Methods

    public void Back()
    {
        UIManager.main.IsPaused = false;
        UIManager.main.UnPauseGame();
    }

    public void Settings()
    {
        UIManager.main.ShowScreen("Settings");
    }

    public void Quit()
    {
        UIManager.main.Quit();
    }

    #endregion
}
