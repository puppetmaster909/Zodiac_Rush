using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonButton : MonoBehaviour
{
    public bool clickedDragon;
    public bool isDragonBomb;

    // Start is called before the first frame update
    void Start()
    {
        clickedDragon = false;
    }

    public void DragonBomb()
    {
        if (isDragonBomb == true)
        {
            clickedDragon = true;
            Debug.Log("Dragon Button Clicked Binch!");
        }

    }
}
