using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    private float currentScore;
    private float i;
    public float addScore;
    public float maxScore;
    public Slider slider;

    void Update()
    {
        slider.value = CalculateSliderValue();
        

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

    }

    float CalculateSliderValue()
    {
        return (currentScore / maxScore);
    }

}
