using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicOpening : MonoBehaviour
{
    public static int SceneNumber;
    public bool notViewed;
    void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "LevelSelection_Scene")
        {
            if (PlayerPrefs.GetInt("ComicViewed") == 0)
            {

                OpeningComic();
            }
           
        }


    }

    public void OpeningComic()
    {
        SceneManager.LoadScene(5);

        PlayerPrefs.SetInt("ComicViewed", 1);
    }

    public void LoadSceneSelect()
    {
        SceneManager.LoadScene(1);
    }
}

