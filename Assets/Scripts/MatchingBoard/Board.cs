using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Maria Edit

public enum GameState
{
    wait,
    move
}

public class Board : MonoBehaviour
{
    public GameState currentState = GameState.move;
    public int width, height;
    public int offSet;

    public float Delay;

    public GameObject tilePrefab;
    public GameObject[] icons;
    private Background_Tile[,] allTiles;
    public GameObject[,] allIcons;
    private FindMatches findMatches;

    // Maria Edit Part 33: Scoring System 
    // Time Stamps: 13:22 and 14:10
    public int basePieceValue = 20;
    private int streakValue = 1;
    private SliderChange sliderChange;

    public int pointsValue = 1;
    private PowerUpPoints powerUpPoints;
    public Text pointsValueText;

    // Maria Edit
    public int moveCounter;
    public bool playerMatch;
    public Text moveCounterText;


    public AudioClip GemSFX;

    // Start is called before the first frame update
    void Awake()
    {
        // Maria Edit Part 33: Scoring System 
        // Time Stamps: 13:46
        sliderChange = FindObjectOfType<SliderChange>();
        powerUpPoints = FindObjectOfType<PowerUpPoints>();

        //moveCounter = 20;
        moveCounterText.text = moveCounter.ToString();
        playerMatch = false;

        findMatches = FindObjectOfType<FindMatches>();
        allTiles = new Background_Tile[width, height];
        allIcons = new GameObject[width, height];
        //Setup the board
        SetUp();
    }

    private void SetUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j + offSet);
                Vector2 titlePosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, titlePosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";

                int iconToUse = Random.Range(0, icons.Length);
                int maxIterations = 0;
                while (MatchesAt(i, j, icons[iconToUse]) && maxIterations < 100)
                {
                    iconToUse = Random.Range(0, icons.Length);
                    maxIterations++;
                    //Debug.Log(maxIterations);
                }
                maxIterations = 0;

                GameObject icon = Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);
                icon.GetComponent<Icon>().row = j;
                icon.GetComponent<Icon>().column = i;
                icon.transform.parent = this.transform;
                icon.name = "( " + i + ", " + j + " )";
                icon.GetComponent<SpriteRenderer>().sortingLayerName = "Icons";
                allIcons[i, j] = icon;


            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allIcons[column - 1, row] != null && allIcons[column - 2, row] != null)
            {
                if (allIcons[column - 1, row].tag == piece.tag && allIcons[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
            if (allIcons[column, row - 1] != null && allIcons[column, row - 2] != null)
            {
                if (allIcons[column, row - 1].tag == piece.tag && allIcons[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allIcons[column, row - 1] != null && allIcons[column, row - 2] != null)
                {
                    if (allIcons[column, row - 1].tag == piece.tag && allIcons[column, row - 2].tag == piece.tag)
                    {
                        return true;
                    }
                }
            }
            if (column > 1)
            {
                if (allIcons[column - 1, row] != null && allIcons[column - 2, row] != null)
                {
                    if (allIcons[column - 1, row].tag == piece.tag && allIcons[column - 2, row].tag == piece.tag)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void DestroyMatchesAt(int column, int row)
    {
        if (allIcons[column, row].GetComponent<Icon>().isMatched)
        {
            findMatches.CurrentMatches.Remove(allIcons[column, row]);
            Destroy(allIcons[column, row]);
            AudioManager.main.PlaySingle(GemSFX);
            // Maria Edit Part 33: Scoring System 
            // Time Stamps: 16:24
            sliderChange.IncreaseScore(basePieceValue * streakValue);

            powerUpPoints.IncreasePoints(pointsValue);

            allIcons[column, row] = null;
        }


    }

    public void DestroyMatches()
    {

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    DestroyMatchesAt(i, j);


                }
            }
        }
        findMatches.CurrentMatches.Clear();
        if (sliderChange.getScore() <= sliderChange.maxScore)
        {
            //Maria Edit
            if (moveCounter >= 0 && playerMatch)
            {
                moveCounter--;
                moveCounterText.text = moveCounter.ToString();
                playerMatch = false;
            }
            StartCoroutine(DecreaseRowCo2());


        }

        if (powerUpPoints.GetPoints() <= powerUpPoints.ratMaxReached 
            ||powerUpPoints.GetPoints() <= powerUpPoints.dragonReached
            ||powerUpPoints.GetPoints() <= powerUpPoints.tigerReached)
        {
            if(pointsValue >= 0 && playerMatch)
            {
                powerUpPoints.IncreasePoints(pointsValue);
                pointsValueText.text = pointsValue.ToString();
                playerMatch = false;

            }
        }
    }
    private IEnumerator DecreaseRowCo2()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] == null)
                {
                    for (int k = j + 1; k < height; k++)
                    {
                        if (allIcons[i, k] != null)
                        {
                            //move dot to empty space
                            allIcons[i, k].GetComponent<Icon>().row = j;
                            //set that spot to be null
                            allIcons[i, k] = null;
                            //break the lob
                            break;
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Refilling Board");
        StartCoroutine(FillBoardCo());
    }
    //private IEnumerator DecreaseRowCo()
    //{
    //    int nullCount = 0;
    //    for (int i = 0; i < width; i++)
    //    {
    //        for (int j = 0; j < height; j++)
    //        {
    //            if (allIcons[i, j] == null)
    //            {
    //                nullCount++;
    //            }
    //            else if (nullCount > 0) {
    //                allIcons[i, j].GetComponent<Icon>().row -= nullCount;
    //                allIcons[i, j] = null;
    //            }
    //        }
    //        nullCount = 0;
    //    }
    //    yield return new WaitForSeconds(.4f);
    //    if (sliderChange.getScore() <= sliderChange.maxScore)
    //    {
    //        StartCoroutine(FillBoardCo());
    //    }
    //}

    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] == null)
                {
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    int iconToUse = Random.Range(0, icons.Length); //was Random.Range(0, icons.Length);
                    GameObject piece = Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);
                    allIcons[i, j] = piece;
                    piece.GetComponent<Icon>().row = j;
                    piece.GetComponent<Icon>().column = i;
                    piece.GetComponent<SpriteRenderer>().sortingLayerName = "Icons";
                    piece.GetComponent<Icon>().transform.parent = GameObject.Find("Board").transform;
                }
            }
        }

    }

    private bool MatchesOnBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    if (allIcons[i, j].GetComponent<Icon>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo()
    {
        RefillBoard();
        yield return new WaitForSeconds(.3f);

        while (MatchesOnBoard())
        {
            // Maria Edit Part 33: Scoring System 
            // Time Stamps: 15:39
            streakValue++;

            yield return new WaitForSeconds(.1f);
            DestroyMatches();
        }
        findMatches.CurrentMatches.Clear();

        if (IsDeadlocked())
        {
            StartCoroutine(ShuffleBoard());
            Debug.Log("Deadlocked!!");
        }
        yield return new WaitForSeconds(.2f);
        currentState = GameState.move;

        // Maria Edit Part 33: Scoring System 
        // Time Stamps: 15:33
        streakValue = 1;

    }

    private void SwitchPieces(int column, int row, Vector2 direction)
    {
        //Take the first piece and save it in a holder
        GameObject holder = allIcons[column + (int)direction.x, row + (int)direction.y] as GameObject;
        //Switching the first piece to be the second position
        allIcons[column + (int)direction.x, row + (int)direction.y] = allIcons[column, row];
        //Set the first piece to be the second piece
        allIcons[column, row] = holder;
    }

    private bool CheckForMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    //Make sure that one and two to the right are in the board
                    if (i < width - 2)
                    {
                        //Check if the pieces to the right and two to the right exist
                        if (allIcons[i + 1, j] != null && allIcons[i + 2, j] != null)
                        {
                            if (allIcons[i + 1, j].tag == allIcons[i, j].tag && allIcons[i + 2, j].tag == allIcons[i, j].tag)
                            {
                                return true;
                            }
                        }
                    }
                    if (j < height - 2)
                    {
                        //Check if the pieces above exist
                        if (allIcons[i, j + 1] != null && allIcons[i, j + 2] != null)
                        {
                            if (allIcons[i, j + 1].tag == allIcons[i, j].tag && allIcons[i, j + 2].tag == allIcons[i, j].tag)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    public bool SwitchAndCheck(int column, int row, Vector2 direction)
    {
        SwitchPieces(column, row, direction);
        if (CheckForMatches())
        {
            SwitchPieces(column, row, direction);
            return true;
        }
        SwitchPieces(column, row, direction);
        return false;
    }

    private bool IsDeadlocked()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    if (i < width - 1)
                    {
                        if (SwitchAndCheck(i, j, Vector2.right))
                        {
                            return false;
                        }
                    }
                    if (j < height - 1)
                    {
                        if (SwitchAndCheck(i, j, Vector2.up))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    private IEnumerator ShuffleBoard()
    {
        yield return new WaitForSeconds(1.2f);
        //Create list of game objects
        List<GameObject> newBoard = new List<GameObject>();
        //Add every piece on the board to this list
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    newBoard.Add(allIcons[i, j]);
                }
            }
        }
        yield return new WaitForSeconds(0.7f);
        //for ever spot on the board...
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int pieceToUse = Random.Range(0, newBoard.Count);

                //Assign the column to the piece
                int maxIterations = 0;

                while (MatchesAt(i, j, newBoard[pieceToUse]) && maxIterations < 100)
                {
                    pieceToUse = Random.Range(0, newBoard.Count);
                    maxIterations++;
                    Debug.Log(maxIterations);
                }

                //make container for gem piece
                Icon piece = newBoard[pieceToUse].GetComponent<Icon>();
                maxIterations = 0;
                piece.column = i;
                //Assign the row of the piece
                piece.row = j;
                //Fill in the array with this new piece
                allIcons[i, j] = newBoard[pieceToUse];
                //Remove it from teh list
                newBoard.Remove(newBoard[pieceToUse]);
            }
        }
        //Check if it's still deadlocked!
        if (IsDeadlocked())
        {
            StartCoroutine(ShuffleBoard());
        }
    }

}
