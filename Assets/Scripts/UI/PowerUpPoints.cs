using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPoints : MonoBehaviour
{
    public Text pointsText;
    public int points;
    public int tigerReached;
    public int dragonReached;
    public int ratMaxReached;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = points.ToString();
    }

    public void IncreasePoints(int amountToIncrease)
    {
        if(points >= 0 && points < ratMaxReached || points < tigerReached || points < dragonReached)
        {
            points += amountToIncrease;
        }
        
    }
}
