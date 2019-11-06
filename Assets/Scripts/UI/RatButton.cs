using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatButton : MonoBehaviour
{
    public bool clickedRat;
    public bool isRatBomb;

    // Start is called before the first frame update
    void Start()
    {
        clickedRat = false;
    }

    public void RatBomb()
    {
        if (isRatBomb == true)
        {
            clickedRat = true;
            Debug.Log("Rat Button Clicked Binch!");
        }

    }
}
