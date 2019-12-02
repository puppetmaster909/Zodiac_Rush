using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq; // Maria Edit

public class FindMatches : MonoBehaviour
{
    private Board board;
    private SliderChange slide;
    public List<GameObject> CurrentMatches = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        slide = FindObjectOfType<SliderChange>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private List<GameObject> IsAreaBomb(Icon icon1, Icon icon2, Icon icon3)
    {
        List<GameObject> currentIcons = new List<GameObject>();

        if (icon1.isAreaBomb)
        {
            CurrentMatches.Union(GetAreaPieces(icon1.column, icon1.row));
        }

        if (icon2.isAreaBomb)
        {
            CurrentMatches.Union(GetAreaPieces(icon2. column, icon2.row));
        }

        if (icon3.isAreaBomb)
        {
            CurrentMatches.Union(GetAreaPieces(icon3.column, icon3.row));
        }

        return currentIcons;
    }

    private List<GameObject> IsRowBomb(Icon icon1, Icon icon2, Icon icon3)
    {
        List<GameObject> currentIcons = new List<GameObject>();

        if (icon1.isRowBomb)
        {
            CurrentMatches.Union(GetRowPieces(icon1.row));
        }

        if (icon2.isRowBomb)
        {
            CurrentMatches.Union(GetRowPieces(icon2.row));
        }

        if (icon3.isRowBomb)
        {
            CurrentMatches.Union(GetRowPieces(icon3.row));
        }

        return currentIcons;
    }

    private List<GameObject> IsColumnBomb(Icon icon1, Icon icon2, Icon icon3)
    {
        List<GameObject> currentIcons = new List<GameObject>();

        if (icon1.isColumnBomb)
        {
            CurrentMatches.Union(GetColumnPieces(icon1.column));
        }

        if (icon2.isColumnBomb)
        {
            CurrentMatches.Union(GetColumnPieces(icon2.column));
        }

        if (icon3.isColumnBomb)
        {
            CurrentMatches.Union(GetColumnPieces(icon3.column));
        }

        return currentIcons;
    }

    private void AddToListAndMatch(GameObject icon)
    {
        if (!CurrentMatches.Contains(icon))
        {
            CurrentMatches.Add(icon);
        }
        icon.GetComponent<Icon>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject icon1, GameObject icon2, GameObject icon3)
    {
        AddToListAndMatch(icon1);

        AddToListAndMatch(icon2);

        AddToListAndMatch(icon3);
    }

    private IEnumerator FindAllMatchesCo()
    {
        if (!slide.gameOver)
        {
            // I changed the WaitForSeconds here Original: .2f
            yield return new WaitForSeconds(.2f);

            for (int i = 0; i < board.width; i++)
            {
                for (int j = 0; j < board.height; j++)
                {
                    GameObject currentIcon = board.allIcons[i, j];
                    Icon currentIconIcon = currentIcon.GetComponent<Icon>(); //Edit

                    if (currentIcon != null)
                    {
                        if (i > 0 && i < board.width - 1)
                        {
                            GameObject leftIcon = board.allIcons[i - 1, j];
                            Icon leftIconIcon = leftIcon.GetComponent<Icon>(); //Edit

                            GameObject rightIcon = board.allIcons[i + 1, j];
                            Icon rightIconIcon = rightIcon.GetComponent<Icon>(); //Edit

                            // Maria Edit
                            if (leftIcon != null && rightIcon != null)
                            {
                                if (leftIcon.tag == currentIcon.tag && rightIcon.tag == currentIcon.tag)
                                {

                                    //CurrentMatches.Union(IsRowBomb(leftIconIcon, currentIconIcon, rightIconIcon));
                                    /*
                                     * ^
                                     * ^
                                     * ^
                                     * delete later
                                    // Maria Edit
                                    if (currentIcon.GetComponent<Icon>().isRowBomb
                                    || leftIcon.GetComponent<Icon>().isRowBomb
                                    || rightIcon.GetComponent<Icon>().isRowBomb)
                                    {

                                        CurrentMatches.Union(GetRowPieces(j));

                                    }
                                    */

                                    //CurrentMatches.Union(IsColumnBomb(leftIconIcon, currentIconIcon, rightIconIcon));
                                    /*
                                     * ^
                                     * ^
                                     * ^
                                     * delete later
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
                                    */


                                    // Upper Area Bomb

                                    //CurrentMatches.Union(IsAreaBomb(leftIconIcon, currentIconIcon, rightIconIcon));

                                    // Maria Edit

                                    GetNearbyPieces(leftIcon, currentIcon, rightIcon);

                                    /*
                                     * ^
                                     * ^
                                     * ^
                                     * delete later
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

                                    */

                                }

                                //test

                                CurrentMatches.Union(IsRowBomb(leftIconIcon, currentIconIcon, rightIconIcon));
                                CurrentMatches.Union(IsColumnBomb(leftIconIcon, currentIconIcon, rightIconIcon));
                                CurrentMatches.Union(IsAreaBomb(leftIconIcon, currentIconIcon, rightIconIcon));

                                //test



                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upIcon = board.allIcons[i, j + 1];
                        Icon upIconIcon = upIcon.GetComponent<Icon>(); //Edit

                        GameObject downIcon = board.allIcons[i, j - 1];
                        Icon downIconIcon = downIcon.GetComponent<Icon>(); //Edit

                        // Maria Edit 
                        if (upIcon != null && downIcon != null)
                        {

                            if (upIcon != null && downIcon != null)
                            {
                                if (upIcon.tag == currentIcon.tag && downIcon.tag == currentIcon.tag)
                                {


                                    //CurrentMatches.Union(IsColumnBomb(upIconIcon, currentIconIcon, downIconIcon));
                                    /*
                                     * ^
                                     * ^
                                     * ^
                                     * delete later
                                    if (currentIcon.GetComponent<Icon>().isColumnBomb
                                   || upIcon.GetComponent<Icon>().isColumnBomb
                                   || downIcon.GetComponent<Icon>().isColumnBomb)
                                    {

                                        CurrentMatches.Union(GetColumnPieces(i));

                                    }
                                    */

                                    //CurrentMatches.Union(IsRowBomb(upIconIcon, currentIconIcon, downIconIcon));
                                    /*
                                     * ^
                                     * ^
                                     * ^
                                     * delete later
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
                                    */

                                    //CurrentMatches.Union(IsAreaBomb(upIconIcon, currentIconIcon, downIconIcon));

                                    // Edit

                                    GetNearbyPieces(upIcon, currentIcon, downIcon);
                                    /*
                                     * ^
                                     * ^
                                     * ^
                                     * delete later
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
                                    */
                                }
                            }

                            //test
                            CurrentMatches.Union(IsColumnBomb(upIconIcon, currentIconIcon, downIconIcon));
                            CurrentMatches.Union(IsRowBomb(upIconIcon, currentIconIcon, downIconIcon));
                            CurrentMatches.Union(IsAreaBomb(upIconIcon, currentIconIcon, downIconIcon));
                            //test


                        }

                    }
                }
            }
        }
    }

    
    List<GameObject> GetAreaPieces(int column, int row)
    {
        List<GameObject> icon = new List<GameObject>();
        for (int i = column - 1; i <= column + 1; i++ )
        {
            for(int j = row - 1; j <= row + 1; j++)
            {
                //Check if the piece is inside the board
                if(i >= 0 && i < board.width && j >= 0 && j < board.height)
                {
                    icon.Add(board.allIcons[i, j]);
                    board.allIcons[i, j].GetComponent<Icon>().isMatched = true;
                }
            }
        }

        return icon;
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




