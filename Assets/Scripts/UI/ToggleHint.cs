using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHint : MonoBehaviour
{
    public GameObject board;
    HintManager hintManager;

    public void toggleHint()
    {
        hintManager = FindObjectOfType<HintManager>();

       if(board != null && board.GetComponent<HintManager>().enabled == true)
        {
            hintManager.DestroyHint();

            board.GetComponent<HintManager>().enabled = false;

            Debug.Log("Hint System Disabled");
        }
       else if (board != null)
        {
            board.GetComponent<HintManager>().enabled = true;
            Debug.Log("Hint System Enabled");

        }
    }
}
