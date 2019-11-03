using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    #region Variables

    public int currentScore;

    public float maxScore;
    public Slider slider;
    public GameObject TrappedZodiac;
    private Board theBoard;

    // Maria Edit Part 33 - Scoring System
    public Text scoreText; // 10:11

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateSliderValue();

        scoreText.text = currentScore.ToString(); // 11:32



        if (Input.GetKeyDown(KeyCode.LeftControl) == true)
        {
            IncreaseScore(500);
        }
    }
    #endregion

    #region Private Methods

    float CalculateSliderValue()
    {
        return (currentScore / maxScore);
    }

    #endregion
    IEnumerator ShowVictoryScreen(float time)
    {
        StartCoroutine(MoveOverSeconds(TrappedZodiac, new Vector3(2.9f, 3.1f, 40.0f), 2.5f));
        yield return new WaitForSeconds(time);
        UIManager.main.ShowScreen("Victory");
        Debug.Log("Waiting: " + time + "seconds");
    }
    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            Debug.Log(objectToMove.transform.position);
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            Debug.Log(objectToMove.transform.position);
        }
        objectToMove.transform.position = end;
    }
    #region Public Methods

    // Maria Edit
    public void IncreaseScore( int amountToIncrease) // 10:30
    {
        int width = 0, height = 0;
        theBoard = FindObjectOfType<Board>();


        if (theBoard != null)
        {
            width = theBoard.width;
            height = theBoard.height;
        }
        
        if (currentScore >= 0)
        {
            if (currentScore <= maxScore)
            {
                currentScore += amountToIncrease;

                Debug.Log(currentScore);
            

            
                if (currentScore >= maxScore)
                {
                    for (int i = 0; i < width; i++)
                    {
                        for(int j = 0; j < height; j++)
                        {
                            Destroy(theBoard.allIcons[j,i]);
                        }
                    }
                    Debug.Log("Level Complete!");
                    StartCoroutine(ShowVictoryScreen(10f));
                    
                }
            }
        }

    }

    public float getScore()
    {
        return currentScore;
    }
    #endregion
}
