using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    #region Variables

    public int currentScore;
    public float VictoryDelay = 10f;
    public float maxScore;
    public Slider slider;
    public GameObject TrappedZodiac;
    public GameObject FreedomParticle;
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
        StartCoroutine(MoveTrappedZodiac());
        yield return new WaitForSeconds(time);
        theBoard.currentState = GameState.move;
        UIManager.main.ShowScreen("Victory");
        Debug.Log("Waiting: " + time + "seconds");
    }
    IEnumerator MoveTrappedZodiac()
    {
        //Move Zodiac Animal to Center of Board
        StartCoroutine(MoveOverSeconds(TrappedZodiac, new Vector3(2.9f, 3.1f, 40.0f), 2.5f));
        yield return new WaitForSeconds(5f);
        StartCoroutine(ShowFreeZodiac());
    }
    IEnumerator ShowFreeZodiac()
    {
        SpriteRenderer sprite;
        GameObject FreeZodiac = GameObject.FindGameObjectWithTag("FreeZodiac");

        FreedomParticle = Instantiate(FreedomParticle, FreeZodiac.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOut());
        sprite = FreeZodiac.GetComponent<SpriteRenderer>();
        Color c = sprite.color;
        c.a = 1;
        sprite.color = c;
        
    }
    private IEnumerator FadeOut()
    {
        Image image;

        image = TrappedZodiac.GetComponent<Image>();
        for (float f = 1f; f >= -0.05f; f -= 0.15f)
        {
            Color c = image.color;
            c.a = f;
            image.color = c;
            yield return new WaitForSeconds(0.05f);
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
                    theBoard.currentState = GameState.wait;
                    StartCoroutine(ShowVictoryScreen(VictoryDelay));
                    
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
