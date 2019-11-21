using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TigerButton : MonoBehaviour
{

    public bool clickedTiger;
    public bool isTigerBomb;

    //private DragonButton dragonButton;

    

    // Start is called before the first frame update
    void Start()
    {
        //dragonButton = FindObjectOfType<DragonButton>();

        clickedTiger = false;
    }

    public void TigerBomb()
    {


        if (isTigerBomb == true)
        {
            clickedTiger = true;
            Debug.Log("Tiger Button Clicked Binch!");
        }


    }

}
