using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        for(int i = 0; i < board.width; i++)
        {
            for(int j = 0; j < board.height; j++)
            {
                GameObject currentIcon = board.allIcons[i, j];
                if(currentIcon != null)
                {
                    if(i > 0 && i < board.width - 1)
                    {
                        GameObject leftIcon = board.allIcons[i - 1, j];
                        GameObject rightIcon = board.allIcons[i + 1, j];

                        if(leftIcon != null && rightIcon != null)
                        {
                            if(leftIcon.tag == currentIcon.tag && rightIcon.tag == currentIcon.tag)
                            {
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
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upIcon = board.allIcons[i, j + 1];
                        GameObject downIcon = board.allIcons[i, j - 1];

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
}
