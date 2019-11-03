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
    public bool[] LevelPassed;

    public int Level1_Star1 = 600, Level1_Star2 = 1000, Level1_Star3 = 1500;
    public int Level2_Star1 = 800, Level2_Star2 = 1200, Level2_Star3 = 2000;
    public int Level3_Star1 = 1200, Level3_Star2 = 2000, Level3_Star3 = 3000;

    private SliderChange theScore;
    private Board board;

    private void Start()
    {
        
        

        if (SceneManager.GetActiveScene().name == "LevelSelection_Scene")
        {
            if(IntToBool(PlayerPrefs.GetInt("Level2Complete")))
            {
                Debug.Log("Level 2 Unlocked");
                GameObject LevelAccess = GameObject.FindGameObjectWithTag("Level2Block");
                LevelAccess.SetActive(false);
            }
            else if (IntToBool(PlayerPrefs.GetInt("Level3Complete")))
            {
                Debug.Log("Level 3 Unlocked");
                GameObject LevelAccess = GameObject.FindGameObjectWithTag("Level3Block");
                LevelAccess.SetActive(false);
            }
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
            CheckLevelComplete();
            
        }
    }

    private void SetNewHighScore(int Score)
    {
        if (GetCurrentScore() > PlayerPrefs.GetInt("HighScore:" + thisLevel.ToString()))
        {
            PlayerPrefs.SetInt("HighScore:" + thisLevel.ToString(), Score);

            Debug.Log("New HighScore:" + (PlayerPrefs.GetInt("HighScore:" + thisLevel.ToString(), 0)));
        }
        
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


    public void CheckLevelComplete()
    {
        if (GetHighScore() > Level1_Star3 && thisLevel == 1)
            PlayerPrefs.SetInt("Level2Complete", BoolToInt(true));
        else if (GetHighScore() > Level2_Star3 && thisLevel == 2)
            PlayerPrefs.SetInt("Level3Complete", BoolToInt(true));
    }
    #endregion
    
    private int BoolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    private bool IntToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

}
