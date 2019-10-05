using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    public int column, row; //x, y
    public int targetX, targetY;
    public bool isMatched = false;

    private Board board;

    private GameObject otherIcon;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;

        row = targetY;
        column = targetX;
    }

    // Update is called once per frame
    void Update()
    {
        findMatches();

        if(isMatched)
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
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            //Directly set the postion
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            board.allIcons[column, row] = this.gameObject;
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //Move towards target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            //Directly set the postion
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            board.allIcons[column, row] = this.gameObject;
        }
    }

    private void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(firstTouchPosition);
    }

    private void OnMouseUp()
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        //Debug.Log(swipeAngle);
        movePieces();
    }
    void movePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width)
        {
            //Right Swipe
            otherIcon = board.allIcons[column + 1, row];
            otherIcon.GetComponent<Icon>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height)
        {
            //Up Swipe
            otherIcon = board.allIcons[column, row + 1];
            otherIcon.GetComponent<Icon>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            //Left Swipe
            otherIcon = board.allIcons[column - 1, row];
            otherIcon.GetComponent<Icon>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            //Down Swipe
            otherIcon = board.allIcons[column, row - 1];
            otherIcon.GetComponent<Icon>().row += 1;
            row -= 1;
        }
    }

    void findMatches()
    {
        //Find Horizontal Matches
        if(column > 0 && column < board.width - 1)
        {
            GameObject leftIcon1 = board.allIcons[column - 1, row];
            GameObject rightIcon1 = board.allIcons[column + 1, row];

            if (leftIcon1.tag == this.gameObject.tag && rightIcon1.tag == this.gameObject.tag)
            {
                leftIcon1.GetComponent<Icon>().isMatched = true;
                rightIcon1.GetComponent<Icon>().isMatched = true;
                isMatched = true;
            }
        }

        if (row > 0 && row < board.height - 1)
        {
            GameObject upIcon1 = board.allIcons[column, row + 1];
            GameObject downIcon1 = board.allIcons[column, row - 1];

            if (upIcon1.tag == this.gameObject.tag && downIcon1.tag == this.gameObject.tag)
            {
                upIcon1.GetComponent<Icon>().isMatched = true;
                downIcon1.GetComponent<Icon>().isMatched = true;
                isMatched = true;
            }
        }
    }
}
