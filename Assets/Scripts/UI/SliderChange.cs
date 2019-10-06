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
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
        
                if (currentScore >= 0)
                {
                    currentScore += addScore;
                }

            Debug.Log(currentScore);
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
