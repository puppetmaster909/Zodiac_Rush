using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region MonoBehavior
    private SliderChange theScore;
    private int highScore, newScore;
    public bool playerWin;

    public Text[] highScoreText;

    // Start is called before the first frame update
    void Start()
    {
       theScore = FindObjectOfType<SliderChange>();
        
    }

    // Update is called once per frame
    void Update()
    {
        setNewHighScore();
    }

    private int setCurrentScore()
    {
        return theScore.currentScore;
    }

    private void setNewHighScore()
    {
        newScore = setCurrentScore();
        if (playerWin && newScore > highScore)
        {
            highScore += getCurrentScore();
            highScoreText[0].text = highScore.ToString();
            playerWin = false;
        }
    }
    #endregion
    

}
