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

    private void GetNearByPieces(GameObject icon1, GameObject icon2, GameObject icon3)
    {
        AddToListAndMatch(icon1);

        AddToListAndMatch(icon2);

        AddToListAndMatch(icon3);

    }


    private IEnumerator FindAllMatchesCo()
    {

        // I changed the WaitForSeconds here Original: .2f
        yield return new WaitForSeconds(.2f);

        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentIcon = board.allIcons[i, j];
                Icon currentIconIcon = currentIcon.GetComponent<Icon>();

                if (currentIcon != null)
                {
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftIcon = board.allIcons[i - 1, j];
                        Icon leftIconIcon = leftIcon.GetComponent<Icon>();

                        GameObject rightIcon = board.allIcons[i + 1, j];
                        Icon rightIconIcon = rightIcon.GetComponent<Icon>();

                        // Maria Edit
                        if (leftIcon != null && rightIcon != null)
                        {
                            if (leftIcon.tag == currentIcon.tag && rightIcon.tag == currentIcon.tag)
                            {

                                // Maria Edit
                                CurrentMatches.Union(IsRowBomb(leftIconIcon, currentIconIcon, rightIconIcon));

                                CurrentMatches.Union(IsColumnBomb(leftIconIcon, currentIconIcon, rightIconIcon));

                                GetNearByPieces(leftIcon, currentIcon, rightIcon);

                                // Maria Edit
                            }
                        }
                    }
                }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upIcon = board.allIcons[i, j + 1];
                        Icon upIconIcon = upIcon.GetComponent<Icon>();    

                        GameObject downIcon = board.allIcons[i, j - 1];
                        Icon downIconIcon = downIcon.GetComponent<Icon>();

                    // Maria Edit 
                    if (upIcon != null && downIcon != null)
                    {

                        CurrentMatches.Union(IsColumnBomb(upIconIcon, currentIconIcon, downIconIcon));

                        CurrentMatches.Union(IsRowBomb(upIconIcon, currentIconIcon, downIconIcon));

                        GetNearByPieces(upIcon, currentIcon, downIcon);

                        // Edit

                        
                    }

                }
            }
        }
    }

    

    List<GameObject> GetAreaBomb(int column, int row)
    {
        List<GameObject> icons = new List<GameObject>();
        for(int i = column - 1; i <= column + 1; i++)
        {
            for(int j = row - 1; j <= row + 1; j++)
            {
                //Check if the piece is inside the board
                if(i >= 0 && i < board.width && j >= 0 && j < board.height)
                {
                    icons.Add(board.allIcons[i, j]);
                    board.allIcons[i, j].GetComponent<Icon>().isMatched = true;
                }
            }
        }

        return icons;
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




