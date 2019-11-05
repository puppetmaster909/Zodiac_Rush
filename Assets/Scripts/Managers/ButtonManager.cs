using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public bool clickedTiger;
    public bool isTigerButton;

    public bool clickedDragon;
    public bool isDragonButton;

    public bool clickedRat;
    public bool isRatButton;


    private void Start()
    {
        clickedTiger = false;
        clickedDragon = false;
        clickedRat = false;
    }

    public void TigerButton()
    {

        if(isTigerButton == true)
        {
            clickedTiger = true;
            Debug.Log("Tiger Button Clicked Binch!");
        }

      
    }

    public void DragonButton()
    {
        if (isDragonButton == true)
        {
            clickedDragon = true;
            Debug.Log("Dragon Button Clicked Binch!");
        }
        
    }

    public void RatButton()
    {
        if (isRatButton)
        {
            clickedRat = true;
            Debug.Log("Rat Button Clicked Binch!");
        }
       
    }
}
