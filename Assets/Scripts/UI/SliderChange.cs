using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    #region Variables
    private float currentScore;

    // public float addScore; //dont think i need this
    public float maxScore;
    public Slider slider;

    //Maria Edit Part 33 - Scoring System
    public Text scoreText; //10:11

    //public int score; don't need this 
    #endregion

    #region MonoBehaviour
    void Update()
    {
        slider.value = CalculateSliderValue();
        scoreText.text = currentScore.ToString(); //11:32

    }
    #endregion

    #region Private(?) Methods
    float CalculateSliderValue()
    {
        return (currentScore / maxScore);
    }

    #endregion

    #region Public Methods
    //Maria Edit 
    public void IncreaseScore(int amountToIncrease ) //10:30
    {
       
        if(currentScore >= 0)
        {
            if (currentScore <= maxScore)
            {
                currentScore += amountToIncrease; //he wrote score += amountToIncrease;
                Debug.Log(currentScore);
            }

            if (currentScore >= maxScore)
            {
                //add scene manager here 

                Debug.Log("Level Complete!");
            }

        }


    }
    #endregion
    
}

#region Previous Score Update

/*
 * This was previously in Update()
 * 
 * 
if (Input.GetKeyDown(KeyCode.Space) && currentScore >= 0)
        {
        
                if (currentScore <= maxScore)
                {
                    currentScore += addScore;
                }

            Debug.Log(currentScore);
            Debug.Log("Pressing Button");
        }

        if (currentScore == maxScore)
        {
            //add scene manager here 

            Debug.Log("Level Complete!");
        }
        */

#endregion
