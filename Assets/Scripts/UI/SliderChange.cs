using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    #region Variables

    private float currentScore;

    public float maxScore;
    public Slider slider;

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
                GameObject board = UIManager.main.transform.Find("Level1").Find("Board").gameObject;
                board.SetActive(false);
                UIManager.main.ShowScreen("Victory");
            }
        }
    }
    #endregion
}
