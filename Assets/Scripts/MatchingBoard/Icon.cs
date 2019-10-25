﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    [Header("Board Varriables")]
    public int column, row; //x, y
    public int previousColumn, previousRow;
    public int targetX, targetY;
    public bool isMatched = false;

    private FindMatches findMatches;
    private Board board;

    private GameObject otherIcon;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;


    public float swipeAngle = 0;
    public float swipeResist = 1f;

    // Maria Edit Part 20 6:10
    #region Powerups
    public bool isColorBomb;
    public GameObject colorBomb;

    // Maria Edit 
    //TESTING FOR UPPER FOR NOW
    public bool isAreaBomb; 
    public GameObject areaBomb;





    //TESTING

    public bool isColumnBomb;
    public GameObject columnBomb;

    public bool isRowBomb;
    public GameObject rowBomb;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        isAreaBomb = false;

        isColumnBomb = false;
        isRowBomb = false;

        //Use this for initialization
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();

       // targetX = (int)transform.position.x;
       // targetY = (int)transform.position.y;

       // row = targetY;
       //column = targetX;

        //previousColumn = column;
       // previousRow = row;
    }

    // Maria Edit Part 20 6:35 TESTING ONLY for color, cross, and area
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            if (board.currentState == GameState.move)
            {
                isAreaBomb = true;
                GameObject area = Instantiate(areaBomb, transform.position, Quaternion.identity);
                area.transform.parent = this.transform;


                // UNCOMMENT THIS TO SEE THE COLOR BOMB FUNCTIONALITY
  
                //isColorBomb = true;
                //GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
                //color.transform.parent = this.transform;

                // UNCOMMENT THIS TO SEE THE ROW BOMB FUNCTIONALITY 
                // You can use Row Bomb and Column Bomb at the same time 

                //isRowBomb = true;
                //GameObject bomb = Instantiate(rowBomb, transform.position, Quaternion.identity);
                //bomb.transform.parent = this.transform;

                //isColumnBomb = true;
                //GameObject cBomb = Instantiate(columnBomb, transform.position, Quaternion.identity);
                //cBomb.transform.parent = this.transform;
            }
            
        }
    }



    // Update is called once per frame
    void Update()
    {
        //FindMatches(); // for testing binch 
        //FindDiagonalMatches(); // for testing binch 

        if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(0f, 0f, 0f, .2f);
        }
        targetX = column;
        targetY = row;

        if(Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //Move towards target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if(board.allIcons[column, row] != this.gameObject)
            {
                board.allIcons[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else
        {
            //Directly set the postion
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //Move towards target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allIcons[column, row] != this.gameObject)
            {
                board.allIcons[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else
        {
            //Directly set the postion
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }

    public IEnumerator CheckMoveCo()
    {
        
        // Maria Edit Part 20 7:37
        if (isColorBomb)
        {
            // this piece is a color bomb, and the other piece is the color to destroy
            findMatches.MatchPiecesOfColor(otherIcon.tag);
            isMatched = true;
        }

        else if (otherIcon.GetComponent<Icon>().isColorBomb)
        {
            // the other piece is a color bomb, and this piece has the color to destroy
            findMatches.MatchPiecesOfColor(this.gameObject.tag);
            otherIcon.GetComponent<Icon>().isMatched = true;
        }

        if (isAreaBomb)
        {
            FindMatches();
            isMatched = true;
        }

        else if (otherIcon.GetComponent<Icon>().isAreaBomb)
        {
            FindMatches();
            otherIcon.GetComponent<Icon>().isMatched = true;
        }

        // Maria Edit

        /*
        //DOESNT WORK
        if (isAreaBomb)
        {
            findMatches.AreaBomb();
            isMatched = true;
        }

        */

        yield return new WaitForSeconds(.5f);

        if(otherIcon != null )
        {
            if(!isMatched && !otherIcon.GetComponent<Icon>().isMatched)
            {
                otherIcon.GetComponent<Icon>().row = row;
                otherIcon.GetComponent<Icon>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentState = GameState.move;
            }
            else
            {
                board.DestroyMatches();
                
            }
            otherIcon = null;
        }

    }
    private void OnMouseDown()
    {
        if (board.currentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        //Debug.Log(firstTouchPosition);
    }

    private void OnMouseUp()
    {
        if (board.currentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            board.currentState = GameState.wait;
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            movePieces();
            
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void MovePiecesActual(Vector2 direction)
    {
        otherIcon = board.allIcons[column + (int)direction.x, row + (int)direction.y];
        previousRow = row;
        previousColumn = column;
        if (otherIcon != null)
        {
            otherIcon.GetComponent<Icon>().column += -1 * (int)direction.x;
            otherIcon.GetComponent<Icon>().row += -1 * (int)direction.y;
            column += (int)direction.x;
            row += (int)direction.y;
            StartCoroutine(CheckMoveCo());
        }
            board.currentState = GameState.move;
        
    }

    void movePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            //Right Swipe
            //otherIcon = board.allIcons[column + 1, row];
            //previousColumn = column;
            //previousRow = row;
            //otherIcon.GetComponent<Icon>().column -= 1;
            //column += 1;
            MovePiecesActual(Vector2.right);
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            //Up Swipe
            //otherIcon = board.allIcons[column, row + 1];
            //previousColumn = column;
            //previousRow = row;
            //otherIcon.GetComponent<Icon>().row -= 1;
            //row += 1;
            MovePiecesActual(Vector2.up);
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            //Left Swipe
            //otherIcon = board.allIcons[column - 1, row];
            //previousColumn = column;
            //previousRow = row;
            //otherIcon.GetComponent<Icon>().column += 1;
            //column -= 1;
            MovePiecesActual(Vector2.left);
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            //Down Swipe
            //otherIcon = board.allIcons[column, row - 1];
            //previousColumn = column;
            //previousRow = row;
            //otherIcon.GetComponent<Icon>().row += 1;
            //row -= 1;
            MovePiecesActual(Vector2.down);
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    
    void FindMatches()
    {
        // Find Horizontal Matches
        if(column > 0 && column < board.width - 1)
        {
            GameObject leftIcon1 = board.allIcons[column - 1, row];
            GameObject rightIcon1 = board.allIcons[column + 1, row];
            if (leftIcon1 != null && rightIcon1 != null)
            {
                if (leftIcon1.tag == this.gameObject.tag && rightIcon1.tag == this.gameObject.tag)
                {
                    leftIcon1.GetComponent<Icon>().isMatched = true;
                    rightIcon1.GetComponent<Icon>().isMatched = true;
                    isMatched = true;
                }
            }

            
            // this is a change too
            if(row < board.height - 1 && isAreaBomb == true) // isAreaBomb change
            {
                GameObject upperLeft = board.allIcons[column - 1, row + 1]; //something wrong
                GameObject upperRight = board.allIcons[column + 1, row + 1]; //something wrong??

                if (upperLeft != null && upperRight != null)
                {
                    //if (upperLeft.tag == this.gameObject.tag && upperRight.tag == this.gameObject.tag)
                    //{
                        upperLeft.GetComponent<Icon>().isMatched = true;
                        upperRight.GetComponent<Icon>().isMatched = true;
                        leftIcon1.GetComponent<Icon>().isMatched = true;
                        rightIcon1.GetComponent<Icon>().isMatched = true;
                    isMatched = true;
                    //}
                }
            }
            //change end
            
        }

        // Find Vertical Matches
        if (row > 0 && row < board.height - 1)
        {
            GameObject upIcon1 = board.allIcons[column, row + 1];
            GameObject downIcon1 = board.allIcons[column, row - 1];
            if (upIcon1 != null && downIcon1 != null)
            {
                if (upIcon1.tag == this.gameObject.tag && downIcon1.tag == this.gameObject.tag)
                {
                    upIcon1.GetComponent<Icon>().isMatched = true;
                    downIcon1.GetComponent<Icon>().isMatched = true;
                    isMatched = true;
                }
            }

            // change here
            if (row > 0 && row < board.height - 1 && column < board.width - 1 && column > 0 && isAreaBomb == true)
            {
                GameObject lowerLeft = board.allIcons[column - 1, row - 1]; //something wrong
                GameObject lowerRight = board.allIcons[column + 1, row - 1]; //something wrong

                //if (lowerLeft.tag == this.gameObject.tag && lowerRight.tag == this.gameObject.tag)
                //{
                    lowerLeft.GetComponent<Icon>().isMatched = true;
                    lowerRight.GetComponent<Icon>().isMatched = true;
                    upIcon1.GetComponent<Icon>().isMatched = true;
                    downIcon1.GetComponent<Icon>().isMatched = true;
                    isMatched = true;
                //}
            }

            //change here
        }


    }
    

    
    
    
}

/*
 * 
 * 
    // Maria Edit
    // Find Diagonal Matches

    void FindDiagonalMatches()
    {
        
        if (column > 0 && column < board.width - 1 && row < board.height - 1) // may have to change this 
        {
            GameObject upperLeft = board.allIcons[column - 1, row + 1]; //something wrong
            GameObject upperRight = board.allIcons[column + 1, row + 1]; //something wrong??

            if (upperLeft != null && upperRight != null)
            {
                if (upperLeft.tag == this.gameObject.tag && upperRight.tag == this.gameObject.tag)
                {
                    upperLeft.GetComponent<Icon>().isMatched = true;
                    upperRight.GetComponent<Icon>().isMatched = true;
                    isMatched = true;
                }
            }

        }
        
        
        if (row > 0 && row < board.height - 1 && column < board.width - 1 & column > 0) // may have to change this
        {
            GameObject lowerLeft = board.allIcons[column - 1, row - 1]; //something wrong
            GameObject lowerRight = board.allIcons[column + 1, row - 1]; //something wrong

            if (lowerLeft.tag == this.gameObject.tag && lowerRight.tag == this.gameObject.tag)
            {
                lowerLeft.GetComponent<Icon>().isMatched = true;
                lowerRight.GetComponent<Icon>().isMatched = true;
                isMatched = true;
            }

        }

        
    }
 */
