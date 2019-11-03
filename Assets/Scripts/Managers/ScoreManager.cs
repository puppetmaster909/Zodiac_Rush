using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region MonoBehavior
    private int newScore;
    public bool playerWin;

    public int thisLevel = 0;

    public Text[] highScoreText;
    public int numOfLevels;
    public bool[] LevelsPlayer;

    SliderChange theScore;
    private void Start()
    {
        //Change highscores on LevelSelection_scene
        if (SceneManager.GetActiveScene().name == "LevelSelection_Scene")
        {
            for (int i = 1; i <= numOfLevels; i++)
            {
                PrintHighScore();
            }
        }

    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "LevelSelection_Scene")
        {
            SetNewHighScore(GetCurrentScore());
        }
    }

    private void SetNewHighScore(int Score)
    {
        if (GetCurrentScore() > PlayerPrefs.GetInt("HighScore:" + thisLevel.ToString()))
        {
            PlayerPrefs.SetInt("HighScore:" + thisLevel.ToString(), Score);

            Debug.Log("New HighScore:" + (PlayerPrefs.GetInt("HighScore:" + thisLevel.ToString(), 0)));
        }
        else
            Debug.Log("Not Getting in " + GetHighScore());
        
    }

    public void PrintHighScore()
    {
        
            int j = 1;
            for (int i = 0; i < numOfLevels; i++)
            {
                highScoreText[i].text = PlayerPrefs.GetInt("HighScore:" + j.ToString(), 0).ToString();
                j++;
            }
        
    }

    private int GetCurrentScore()
    {
        theScore = FindObjectOfType<SliderChange>();
        int Score;

        Score = (int)theScore.currentScore;
        return Score;
        
    }
    private int GetHighScore()
    {

         return PlayerPrefs.GetInt("HighScore:" + thisLevel.ToString(), 0);

    }

    #endregion
    

}
