using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
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

    #region Runtime Members

    public Button PauseButton;
    private Board board;

    #endregion

    #region Public Methods

    public void Pause()
    {
        board = FindObjectOfType<Board>();

        if (board.currentState != GameState.wait)
        {
            UIManager.main.PauseGame();
        }
    }

    #endregion
}
