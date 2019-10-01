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

    public void Quit()
    {
        UIManager.main.Quit();
    }

    #endregion
}
