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

    [Header("Level 1 Star Requirements")]
    public int Level1_Star1 = 600;
    public int Level1_Star2 = 1000;
    public int Level1_Star3 = 1500;

    [Header("Level 2 Star Requirements")]
    public int Level2_Star1 = 800;
    public int Level2_Star2 = 1200;
    public int Level2_Star3 = 2000;

    [Header("Level 3 Star Requirements")]
    public int Level3_Star1 = 1200;
    public int Level3_Star2 = 2000;
    public int Level3_Star3 = 3000;

    private SliderChange theScore;
    Board board;
    private void Start()
    {

        board = FindObjectOfType<Board>();

        if (SceneManager.GetActiveScene().name == "LevelSelection_Scene")
        {
            CheckStars();

            if(IntToBool(PlayerPrefs.GetInt("Level1Complete")))
            {
                Debug.Log("Level 2 Unlocked");
                GameObject LevelAccess = GameObject.FindGameObjectWithTag("Level2Block");
                LevelAccess.SetActive(false);
            }

            if (IntToBool(PlayerPrefs.GetInt("Level2Complete")))
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
        if (SceneManager.GetActiveScene().name != "LevelSelection_Scene" && board.currentState != GameState.wait) 
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
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore:" + thisLevel.ToString(), 0);
    }


    public void CheckLevelComplete()
    {
        if (PlayerPrefs.GetInt("HighScore:" + thisLevel.ToString()) >= Level1_Star3 && thisLevel == 1)
        {
            PlayerPrefs.SetInt("Level1Complete", BoolToInt(true));
        }
        if (PlayerPrefs.GetInt("HighScore:" + thisLevel.ToString()) >= Level2_Star3 && thisLevel == 2)
        {
            PlayerPrefs.SetInt("Level2Complete", BoolToInt(true));
        }
    }

    private void CheckStars()
    {
        Image image;

        //Level 3 Progression
        if (PlayerPrefs.GetInt("HighScore:3") >= Level3_Star3 && IntToBool(PlayerPrefs.GetInt("Level2Complete")) == true)
        {
            //3 Stars
            GameObject ZeroStars = GameObject.FindGameObjectWithTag("Level3_0Star");
            GameObject OneStar = GameObject.FindGameObjectWithTag("Level3_1Star");
            GameObject TwoStars = GameObject.FindGameObjectWithTag("Level3_2Star");
            TwoStars.SetActive(false);
            OneStar.SetActive(false);
            ZeroStars.SetActive(false);


            GameObject ThreeStars = GameObject.FindGameObjectWithTag("Level3_3Star");
            image = ThreeStars.GetComponent<Image>();
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
        else if(PlayerPrefs.GetInt("HighScore:3") >= Level3_Star2 && PlayerPrefs.GetInt("HighScore:3") < Level3_Star3 && IntToBool(PlayerPrefs.GetInt("Level2Complete")) == true)
        {
            //2 Stars
            GameObject ZeroStars = GameObject.FindGameObjectWithTag("Level3_0Star");
            GameObject OneStar = GameObject.FindGameObjectWithTag("Level3_1Star");
            GameObject ThreeStars = GameObject.FindGameObjectWithTag("Level3_3Star");
            ThreeStars.SetActive(false);
            OneStar.SetActive(false);
            ZeroStars.SetActive(false);


            GameObject TwoStars = GameObject.FindGameObjectWithTag("Level3_2Star");
            image = TwoStars.GetComponent<Image>();
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
        else if(PlayerPrefs.GetInt("HighScore:3") < Level3_Star2 && PlayerPrefs.GetInt("HighScore:3") > 0 && IntToBool(PlayerPrefs.GetInt("Level2Complete")) == true)
        {
            //1 Star
            GameObject ZeroStars = GameObject.FindGameObjectWithTag("Level3_0Star");
            GameObject TwoStars = GameObject.FindGameObjectWithTag("Level3_2Star");
            GameObject ThreeStars = GameObject.FindGameObjectWithTag("Level3_3Star");
            ThreeStars.SetActive(false);
            TwoStars.SetActive(false);
            ZeroStars.SetActive(false);


            GameObject OneStar = GameObject.FindGameObjectWithTag("Level3_1Star");
            image = OneStar.GetComponent<Image>();
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }


        //Level 2 Progression
        if (PlayerPrefs.GetInt("HighScore:2") >= Level2_Star3 && IntToBool(PlayerPrefs.GetInt("Level1Complete")) == true)
        {
            //3 Star
            GameObject ZeroStars = GameObject.FindGameObjectWithTag("Level2_0Star");
            GameObject OneStar = GameObject.FindGameObjectWithTag("Level2_1Star");
            GameObject TwoStars = GameObject.FindGameObjectWithTag("Level2_2Star");
            TwoStars.SetActive(false);
            OneStar.SetActive(false);
            ZeroStars.SetActive(false);


            GameObject ThreeStars = GameObject.FindGameObjectWithTag("Level2_3Star");
            image = ThreeStars.GetComponent<Image>();
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
        else if (PlayerPrefs.GetInt("HighScore:2") >= Level2_Star2 && PlayerPrefs.GetInt("HighScore:2") < Level2_Star3 && IntToBool(PlayerPrefs.GetInt("Level1Complete")) == true)
        {
            //2 Stars
            GameObject ZeroStars = GameObject.FindGameObjectWithTag("Level2_0Star");
            GameObject OneStar = GameObject.FindGameObjectWithTag("Level2_1Star");
            GameObject ThreeStars = GameObject.FindGameObjectWithTag("Level2_3Star");
            ThreeStars.SetActive(false);
            OneStar.SetActive(false);
            ZeroStars.SetActive(false);


            GameObject TwoStars = GameObject.FindGameObjectWithTag("Level2_2Star");
            image = TwoStars.GetComponent<Image>();
            Color c = image.color;
            c.a = 255;
            image.color = c;
        }
        else if (PlayerPrefs.GetInt("HighScore:2") < Level2_Star2 && PlayerPrefs.GetInt("HighScore:2") > 0 && IntToBool(PlayerPrefs.GetInt("Level1Complete")) == true)
        {
            //1 Star
            GameObject ZeroStars = GameObject.FindGameObjectWithTag("Level2_0Star");
            GameObject TwoStars = GameObject.FindGameObjectWithTag("Level2_2Star");
            GameObject ThreeStars = GameObject.FindGameObjectWithTag("Level2_3Star");
            ThreeStars.SetActive(false);
            TwoStars.SetActive(false);
            ZeroStars.SetActive(false);


            GameObject OneStar = GameObject.FindGameObjectWithTag("Level2_1Star");
            image = OneStar.GetComponent<Image>();
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }

        //Level 1 Progression
        if (PlayerPrefs.GetInt("HighScore:1") >= Level1_Star3)
        {
            //3 Stars
            GameObject ZeroStars = GameObject.FindGameObjectWithTag("Level1_0Star");
            GameObject OneStar = GameObject.FindGameObjectWithTag("Level1_1Star");
            GameObject TwoStars = GameObject.FindGameObjectWithTag("Level1_2Star");
            TwoStars.SetActive(false);
            OneStar.SetActive(false);
            ZeroStars.SetActive(false);


            GameObject ThreeStars = GameObject.FindGameObjectWithTag("Level1_3Star");
            image = ThreeStars.GetComponent<Image>();
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
        else if (PlayerPrefs.GetInt("HighScore:1") >= Level1_Star2 && PlayerPrefs.GetInt("HighScore:1") < Level1_Star3)
        {
            //2 Stars
            GameObject ZeroStars = GameObject.FindGameObjectWithTag("Level1_0Star");
            GameObject OneStar = GameObject.FindGameObjectWithTag("Level1_1Star");
            GameObject ThreeStars = GameObject.FindGameObjectWithTag("Level1_3Star");
            ThreeStars.SetActive(false);
            OneStar.SetActive(false);
            ZeroStars.SetActive(false);


            GameObject TwoStars = GameObject.FindGameObjectWithTag("Level1_2Star");
            image = TwoStars.GetComponent<Image>();
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
        else if (PlayerPrefs.GetInt("HighScore:1") < Level1_Star2 && PlayerPrefs.GetInt("HighScore:1") > 0)
        {
            //1 Star
            GameObject ZeroStars = GameObject.FindGameObjectWithTag("Level1_0Star");
            GameObject TwoStars = GameObject.FindGameObjectWithTag("Level1_2Star");
            GameObject ThreeStars = GameObject.FindGameObjectWithTag("Level1_3Star");
            ThreeStars.SetActive(false);
            TwoStars.SetActive(false);
            ZeroStars.SetActive(false);


            GameObject OneStar = GameObject.FindGameObjectWithTag("Level1_1Star");
            image = OneStar.GetComponent<Image>();
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
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
