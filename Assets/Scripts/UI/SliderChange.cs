﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    #region Variables

    private float currentScore;

    public float maxScore;
    public Slider slider;

    private Board theBoard;

    // Maria Edit Part 33 - Scoring System
    public Text scoreText; // 10:11

    #endregion

    #region MonoBehaviour
    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateSliderValue();
        scoreText.text = currentScore.ToString(); // 11:32


        if (Input.GetKeyDown(KeyCode.LeftControl) == true)
        {
            IncreaseScore(500);
        }
    }
    #endregion

    #region Private Methods

    float CalculateSliderValue()
    {
        return (currentScore / maxScore);
    }

    #endregion

    #region Public Methods

    // Maria Edit
    public void IncreaseScore( int amountToIncrease) // 10:30
    {
        int width = 0, height = 0;
        theBoard = FindObjectOfType<Board>();

        if (theBoard != null)
        {
            width = theBoard.width;
            height = theBoard.height;
        }
        
        if (currentScore >= 0)
        {
            if (currentScore <= maxScore)
            {
                currentScore += amountToIncrease;
                Debug.Log(currentScore);
            }

            
            if (currentScore >= maxScore)
            {
                Debug.Log("Level Complete!");
                // Hide Board gameObject
                GameObject board = UIManager.main.transform.Find("Board").gameObject;

                for (int i = 0; i < width; i++)
                {
                    for(int j = 0; j < height; j++)
                    {
                        Destroy(theBoard.allIcons[j,i]);
                    }
                }

                //board.SetActive(false);

                UIManager.main.ShowScreen("Victory");
            }
            
        }
    }
    public float getScore()
    {
        return currentScore;
    }
    #endregion
}