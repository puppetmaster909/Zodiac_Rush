using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    // SCRIPT NOT IN USE
    //public Button [] buttons;
    public GameObject[] buttons;
    public Button[] myButtons;
    

    public bool clickedTiger;
    //public bool isTigerButton;

    public bool clickedDragon;
    public bool isDragonButton;

    public bool clickedRat;
    public bool isRatButton;


    private void Start()
    {
        //clickedTiger = false;
        clickedDragon = false;
        clickedRat = false;
    }

    public void TigerButton()
    {
        
            clickedTiger = true;
            Debug.Log("Tiger Button Clicked Binch!");
            myButtons[1].interactable = false;
            myButtons[2].interactable = false;
        

      
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
