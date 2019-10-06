using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject tilePrefab;
    public GameObject[] icons;
    private Background_Tile[,] allTiles;
    public GameObject[,] allIcons;
    private FindMatches findMatches;

    // Start is called before the first frame update
    void Start()
    {
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
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";

                int iconToUse = Random.Range(0, icons.Length);
                int maxIterations = 0;
                while (MatchesAt(i, j, icons[iconToUse]) && maxIterations < 100)
                {
                    iconToUse = Random.Range(0, icons.Length);
                    maxIterations++;
                    Debug.Log(maxIterations);
                }
                maxIterations = 0;

                GameObject icon = Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);
                icon.GetComponent<Icon>().row = j;
                icon.GetComponent<Icon>().column = i;
                icon.transform.parent = this.transform;
                icon.name = "( " + i + ", " + j + " )";
                allIcons[i, j] = icon;
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allIcons[column - 1, row].tag == piece.tag && allIcons[column - 2, row].tag == piece.tag)
            {
                return true;
            }
            if (allIcons[column, row - 1].tag == piece.tag && allIcons[column, row - 2].tag == piece.tag)
            {
                return true;
            }
        }
        else if(column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allIcons[column, row - 1].tag == piece.tag && allIcons[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
            if(column > 1)
            { 
                if (allIcons[column - 1, row].tag == piece.tag && allIcons[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void DestroyMatchesAt(int column, int row)
    {
        if(allIcons[column, row].GetComponent<Icon>().isMatched)
        {
            findMatches.CurrentMatches.Remove(allIcons[column, row]);
            Destroy(allIcons[column, row]);
            allIcons[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        StartCoroutine(DecreateRowCo());
    }

    private IEnumerator DecreateRowCo()
    {
        int nullCount = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0) {
                    allIcons[i, j].GetComponent<Icon>().row -= nullCount;
                    allIcons[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (allIcons[i, j] == null)
                {
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    int iconToUse = Random.Range(0, icons.Length);
                    GameObject piece = Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);
                    allIcons[i, j] = piece;
                    piece.GetComponent<Icon>().row = j;
                    piece.GetComponent<Icon>().column = i;
                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(allIcons[i,j] != null)
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
        yield return new WaitForSeconds(.5f);

        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
        findMatches.CurrentMatches.Clear();
        yield return new WaitForSeconds(.5f);
        currentState = GameState.move;
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
            for(int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    //Make sure that one and two to the right are in the board
                    if (i < width - 1)
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
                    if (j < height)
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

    private bool SwitchAndCheck(int column, int row, Vector2 direction)
    {
        SwitchPieces(column, row, direction);
        if(CheckForMatches())
        {
            SwitchPieces(column, row, direction);
        }
        SwitchPieces(column, row, direction);
        return false;
    }

    private bool IsDeadlocked()
    {
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allIcons[i, j] != null)
                {
                    if(i < width - 1)
                    {
                        if(SwitchAndCheck(i, j, Vector2.right))
                        {
                            return false;
                        }
                    }
                    if (j < height - 1)
                    {
                        if(SwitchAndCheck(i, j, Vector2.up))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}
