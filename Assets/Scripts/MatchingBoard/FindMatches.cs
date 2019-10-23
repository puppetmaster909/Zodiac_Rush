using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq; // Maria Edit

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> CurrentMatches = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();

    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);

        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentIcon = board.allIcons[i, j];

                if (currentIcon != null)
                {
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftIcon = board.allIcons[i - 1, j];
                        GameObject rightIcon = board.allIcons[i + 1, j];

                        // Maria Edit
                        if (leftIcon != null && rightIcon)
                        {
                            if (leftIcon.tag == currentIcon.tag && rightIcon.tag == currentIcon.tag)
                            {

                                // Maria Edit
                                if (currentIcon.GetComponent<Icon>().isRowBomb
                                || leftIcon.GetComponent<Icon>().isRowBomb
                                || rightIcon.GetComponent<Icon>().isRowBomb)
                                {

                                    CurrentMatches.Union(GetRowPieces(j));

                                }

                                if (currentIcon.GetComponent<Icon>().isColumnBomb)
                                {
                                    CurrentMatches.Union(GetColumnPieces(i));
                                }

                                if (leftIcon.GetComponent<Icon>().isColumnBomb)
                                {
                                    CurrentMatches.Union(GetColumnPieces(i - 1));
                                }

                                if (rightIcon.GetComponent<Icon>().isColumnBomb)
                                {
                                    CurrentMatches.Union(GetColumnPieces(i + 1));
                                }



                                // Upper Area Bomb


                                // Maria Edit



                                if (!CurrentMatches.Contains(leftIcon))
                                {
                                    CurrentMatches.Add(leftIcon);
                                }
                                leftIcon.GetComponent<Icon>().isMatched = true;
                                if (!CurrentMatches.Contains(rightIcon))
                                {
                                    CurrentMatches.Add(rightIcon);
                                }
                                rightIcon.GetComponent<Icon>().isMatched = true;
                                if (!CurrentMatches.Contains(currentIcon))
                                {
                                    CurrentMatches.Add(currentIcon);
                                }
                                currentIcon.GetComponent<Icon>().isMatched = true;

                            }
                        }
                    }
                }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upIcon = board.allIcons[i, j + 1];
                        GameObject downIcon = board.allIcons[i, j - 1];

                    // Maria Edit 
                    if (upIcon != null && downIcon != null)
                    {

                        if (currentIcon.GetComponent<Icon>().isColumnBomb
                                || upIcon.GetComponent<Icon>().isColumnBomb
                                || downIcon.GetComponent<Icon>().isColumnBomb)
                        {

                            CurrentMatches.Union(GetColumnPieces(i));

                        }

                        if (currentIcon.GetComponent<Icon>().isRowBomb)
                        {
                            CurrentMatches.Union(GetRowPieces(j));
                        }

                        if (upIcon.GetComponent<Icon>().isRowBomb)
                        {
                            CurrentMatches.Union(GetRowPieces(j + 1));
                        }

                        if (downIcon.GetComponent<Icon>().isRowBomb)
                        {
                            CurrentMatches.Union(GetRowPieces(j - 1));
                        }

                        // Edit

                        if (upIcon != null && downIcon != null)
                        {
                            if (upIcon.tag == currentIcon.tag && downIcon.tag == currentIcon.tag)
                            {
                                if (!CurrentMatches.Contains(upIcon))
                                {
                                    CurrentMatches.Add(upIcon);
                                }
                                upIcon.GetComponent<Icon>().isMatched = true;
                                if (!CurrentMatches.Contains(downIcon))
                                {
                                    CurrentMatches.Add(downIcon);
                                }
                                downIcon.GetComponent<Icon>().isMatched = true;
                                if (!CurrentMatches.Contains(currentIcon))
                                {
                                    CurrentMatches.Add(currentIcon);
                                }
                                currentIcon.GetComponent<Icon>().isMatched = true;

                            }
                        }
                    }

                }
            }
        }
    }

    

    public void AreaBomb()
    {
        for (int i = 0; i < board.width; i++)
        {
            for(int j = 0; j < board.height; j++)
            {

                if(board.allIcons[i, j] != null)
                {
                    if (i > 0 && i < board.width - 1 && j < board.height - 1) // may have to change this 
                    {
                        GameObject upperLeft = board.allIcons[i - 1, j + 1]; //something wrong
                        GameObject upperRight = board.allIcons[i + 1, j + 1]; //something wrong??

                        if (upperLeft != null && upperRight != null)
                        {
                            if (upperLeft.tag == this.gameObject.tag && upperRight.tag == this.gameObject.tag)
                            {
                                upperLeft.GetComponent<Icon>().isMatched = true;
                                upperRight.GetComponent<Icon>().isMatched = true;
                                board.allIcons[i, j].GetComponent<Icon>().isMatched = true;
                            }
                        }

                    }


                    if (j > 0 && j < board.height - 1 && i < board.width - 1 & i > 0) // may have to change this
                    {
                        GameObject lowerLeft = board.allIcons[i - 1, j - 1]; //something wrong
                        GameObject lowerRight = board.allIcons[i + 1, j - 1]; //something wrong

                        if (lowerLeft.tag == this.gameObject.tag && lowerRight.tag == this.gameObject.tag)
                        {
                            lowerLeft.GetComponent<Icon>().isMatched = true;
                            lowerRight.GetComponent<Icon>().isMatched = true;
                            board.allIcons[i, j].GetComponent<Icon>().isMatched = true;
                        }

                    }

                }

            }

        }

    }

    

    //Maria Edit Part 20 3:00
    public void MatchPiecesOfColor(string color)
    {
        for(int i = 0; i<board.width; i++)
        {
            for(int j = 0; j < board.height; j++)
            {
                // check if that piece exists
                if (board.allIcons[i,j] != null)
                {
                    // check the tag on that icon
                    if(board.allIcons[i,j].tag == color)
                    {
                        // set that icon to be matched
                        board.allIcons[i, j].GetComponent<Icon>().isMatched = true;
                    }
                }
            }
        }
    }

    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> icons = new List<GameObject>();

        for (int i = 0; i < board.height; i++)
        {
            if (board.allIcons[column, i] != null)
            {
                icons.Add(board.allIcons[column, i]);
                board.allIcons[column, i].GetComponent<Icon>().isMatched = true;
            }
        }


        return icons;
    }

    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> icons = new List<GameObject>();

        for (int i = 0; i < board.height; i++)
        {
            if (board.allIcons[i, row] != null)
            {
                icons.Add(board.allIcons[i, row]);
                board.allIcons[i, row].GetComponent<Icon>().isMatched = true;
            }
        }


        return icons;
    }


}




