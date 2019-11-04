using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public bool clickedTiger;

    private void Start()
    {
        clickedTiger = false;
    }

    public void TigerButton()
    {


        clickedTiger = true;

        if (clickedTiger == true)
        {
            Debug.Log("CLICKED");
        }

    }
}
