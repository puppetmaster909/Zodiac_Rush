using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Static Fields

    public static GameManager main;

    #endregion

    #region Runtime Members



    #endregion

    #region MonoBehavior

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion
}