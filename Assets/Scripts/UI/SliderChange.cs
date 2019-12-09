using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    #region Variables
    private int doOnce, previousHIGHscore;
    public int currentScore;
    public float VictoryDelay = 10f;
    public float scaleRatio = 1.0f;
    public float maxScore;
    public bool gameOver;
    
    public Slider slider;

    public GameObject TrappedZodiac;
    public GameObject FreedomParticle;
    public SpriteRenderer whiteCrystal;

    public GameObject Sparkles;
    private GameObject SparklesParticle;
    private Board theBoard;
    private HintManager Hint;
    private ScoreManager theScore;

    // Maria Edit Part 33 - Scoring System
    public Text scoreText; // 10:11
    public Text earnedScore;
    public Text previousHighScore;
    public Text NewHighScore;
    #endregion

    #region MonoBehaviour

    private void Start()
    {
        Hint = FindObjectOfType<HintManager>();
        theBoard = FindObjectOfType<Board>();
        theScore = FindObjectOfType<ScoreManager>();

        previousHIGHscore = PlayerPrefs.GetInt("HighScore:" + theScore.thisLevel.ToString());
        doOnce = 0;
        gameOver = false;

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateSliderValue();

        scoreText.text = currentScore.ToString(); // 11:32


        //FOR DEBUG PURPOSES
        if (Input.GetKeyDown(KeyCode.LeftControl) == true)
        {
            IncreaseScore(500);
        }

        StartCoroutine(CheckGameWin());
    }
    #endregion

    #region Private Methods

    float CalculateSliderValue()
    {
        return (currentScore / maxScore);
    }

    #endregion
    IEnumerator CheckGameWin() 
    {
    int width = theBoard.width;
    int height = theBoard.height;
        yield return new WaitForSeconds(0.3f);
        if (currentScore < maxScore && theBoard.moveCounter <= 0)
        {
            
            theBoard.currentState = GameState.wait;
            //Time.timeScale = 0;
            for (int i = 0; i<width; i++)
            {
                for (int j = 0; j<height; j++)
                {
                    //Destory the board and current Hint Particle
                    Destroy(theBoard.allIcons[j, i]);
                    Hint.DestroyHint();
                    yield return new WaitForSeconds(0.030f);
                }
            }
            //Change Values on TryAgain Screen
            earnedScore.text = getScore().ToString();
            previousHighScore.text = theScore.GetHighScore().ToString();

            //If player gets a new HighScore display "New Highscore!"
            if(getScore() > previousHIGHscore)
            {
                NewHighScore.transform.gameObject.SetActive(true);
            }

            //Show Progress Screen
            theBoard.currentState = GameState.move;
            gameOver = true;
            
            UIManager.main.ShowScreen("TryAgain");
        }

        if (currentScore >= maxScore && theBoard.moveCounter >= 0)
        {
            theBoard.currentState = GameState.wait;
            for (int i = 0; i<width; i++)
            {
                for (int j = 0; j<height; j++)
                {
                    //Destory the board and current Hint Particle
                    Destroy(theBoard.allIcons[j, i]);
                    Hint.DestroyHint();
                    yield return new WaitForSeconds(0.030f);
                }
            }
            
            Debug.Log("Level Complete!");

            gameOver = true;
            //Play Victory Animation then Show Win Screen
            if (doOnce == 0)
            { 
                StartCoroutine(ShowVictoryScreen(VictoryDelay));
                doOnce++;
            }

        }
}

    IEnumerator ShowVictoryScreen(float time)
    {
        GameObject HUD = GameObject.FindGameObjectWithTag("HUD");
        GameObject Grid = GameObject.FindGameObjectWithTag("Grid");
        GameObject Cat = GameObject.FindGameObjectWithTag("Cat");
        Hint.DestroyHint();

        
        yield return new WaitForSeconds(1.0f);
        Cat.SetActive(false);
        HUD.SetActive(false);
        Grid.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(MoveTrappedZodiac());
        yield return new WaitForSeconds(time);
        theBoard.currentState = GameState.move;
        UIManager.main.ShowScreen("Victory");
        Debug.Log("Waiting: " + time + "seconds");
    }
    
    IEnumerator MoveTrappedZodiac()
    {
        //Move Zodiac Animal to Center of Board
        
        StartCoroutine(MoveOverSeconds(TrappedZodiac, new Vector3(3.0f, 7.1f, 40.0f), 1.5f));
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(ShowFreeZodiac());
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(FadeOut());
    }
    IEnumerator ShowFreeZodiac()
    {
        SpriteRenderer sprite;
        GameObject FreeZodiac = GameObject.FindGameObjectWithTag("FreeZodiac");

        SparklesParticle = Instantiate(Sparkles, TrappedZodiac.transform.position, Quaternion.identity);

        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(3.8f);
        Destroy(SparklesParticle);
        sprite = FreeZodiac.GetComponent<SpriteRenderer>();
        Color c = sprite.color;
        c.a = 1;
        sprite.color = c;
        
    }

    private IEnumerator FadeIn()
    {
        SpriteRenderer crystal;

        crystal = whiteCrystal;
        for (float f = 0f; f <= 1.5f; f += 0.10f)
        {
            
            Color c = crystal.color;
            c.a = f;
            
            crystal.color = c;
            yield return new WaitForSeconds(0.20f);
        }
        TrappedZodiac.SetActive(false);
        
    }

    private IEnumerator FadeOut()
    {
        SpriteRenderer crystal;

        crystal = whiteCrystal ;
        for (float f = 1f; f >= -0.5f; f -= 0.10f)
        {
            Color c = crystal.color;
            c.a = f;
            crystal.color = c;
            yield return new WaitForSeconds(0.10f);
        }
        yield return new WaitForSeconds(5f);
    }
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0f;
        
        Vector3 startingScale = objectToMove.transform.localScale;
        Vector3 startingPos = objectToMove.transform.position;

        
        while (elapsedTime < seconds)
        {

            objectToMove.transform.localScale = Vector3.Lerp(startingScale, startingScale * scaleRatio, (elapsedTime / seconds));
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
        

            width = theBoard.width;
            height = theBoard.height;
        
        if (currentScore >= 0)
        {
            if (currentScore <= maxScore)
            {
                currentScore += amountToIncrease;

                Debug.Log(currentScore + "/" + maxScore + " " + theBoard.moveCounter);

                
            }
        }

    }

    public float getScore()
    {
        return currentScore;
    }
    #endregion
}
