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

    private PowerUpPoints powerUpPoints;

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

        //myButtons[0].interactable = false;
        //myButtons[1].interactable = false;
        //myButtons[2].interactable = false;

        powerUpPoints = FindObjectOfType<PowerUpPoints>();
    }

    private void Update()
    {

        // Enable Buttons when certain points are made 

        if (powerUpPoints.points >= powerUpPoints.tigerReached 
            && clickedDragon != true
            && clickedRat != true)
        {
            myButtons[0].interactable = true;
        }
        else
        {
            myButtons[0].interactable = false;
        }

        if (powerUpPoints.points >= powerUpPoints.dragonReached
            && clickedTiger != true
            && clickedRat != true)
        {
            myButtons[1].interactable = true;
        }
        else
        {
            myButtons[1].interactable = false;
        }

        if (powerUpPoints.points >= powerUpPoints.ratMaxReached
            && clickedTiger != true
            && clickedDragon != true)
        {
            myButtons[2].interactable = true;
        }
        else
        {
            myButtons[2].interactable = false;
        }
    }

    public void TigerButton()
    {

            if (isTigerButton == true && myButtons[0].interactable == true)
            {
                clickedTiger = true;

                //myButtons[1].interactable = false;
                //myButtons[2].interactable = false;

                //powerUpPoints.points -= powerUpPoints.tigerReached;

                Debug.Log("Tiger Button Clicked Binch!");
                Debug.Log(powerUpPoints.points);
            }

    }

    public void DragonButton()
    {
        if (isDragonButton == true && myButtons[1].interactable == true)
        {
            clickedDragon = true;
            myButtons[0].interactable = false;
            myButtons[2].interactable = false;
            Debug.Log("Dragon Button Clicked Binch!");
        }
        
    }

    public void RatButton()
    {
        if (isRatButton == true && myButtons[2].interactable == true)
        {
            clickedRat = true;
            myButtons[0].interactable = false;
            myButtons[1].interactable = false;
            Debug.Log("Rat Button Clicked Binch!");
        }
       
    }

    
}
