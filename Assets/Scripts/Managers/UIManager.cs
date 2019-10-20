using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Static Members
    
    public static UIManager main;

    #endregion

    #region Runtime Members

    // Screen struct
    [System.Serializable]
    public struct Screen
    {
        public string name;
        public GameObject screen;
        public Animator screenAnimator;
    }

    // List of screens to iterate through
    public List<Screen> Screens = new List<Screen>();

    public AudioClip Confirm;
    public bool IsPaused = false;
    public int CurrentScreen;
    private int PauseScreen = 0;
    public int PrePauseScreen;
    public string PreviousScreenName = "Level";
    public GameObject currentLevel;

    #endregion

    #region MonoBehavior

    private void Awake()
    {
        // Singleton behavior
        if (main == null)
        {
            main = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
        
    }

    void Start()
    {
        
        int i = 0;
        foreach (Screen s in Screens)
        {
            if (s.name.Equals("Pause"))
            {
                PauseScreen = i;
            }
            i++;
        }
    }

    #endregion

    #region Public Methods

    // Displays the screen with the given name
    public void ShowScreen(string name)
    {
        AudioManager.main.PlaySingle(Confirm);

        PreviousScreenName = Screens[CurrentScreen].name;
        for (int i = 0; i < Screens.Count; i++)
        {
            if (Screens[i].name.Equals(name))
            {
                StartCoroutine(ShowScreenCoroutine(i));
            }
        }
    }

    // Coroutine to play screen animation
    public IEnumerator ShowScreenCoroutine(int index)
    {
        if (Screens[CurrentScreen].screenAnimator != null)
        {
            Screens[CurrentScreen].screenAnimator.SetTrigger("Close");
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("Current Screen: " + CurrentScreen);
        if (Screens[CurrentScreen].name != currentLevel.name)
        {
            Screens[CurrentScreen].screen.SetActive(false);
        }
        Screens[index].screen.SetActive(true);
        CurrentScreen = index;
        Debug.Log("New Current Screen: " + CurrentScreen);
    }

    // Quits application
    public void LeaveLevel()
    {
        Debug.Log("Returning to Main Menu");   
        SceneManager.LoadScene("MainMenu_Scene");
        
    }

    public void Quit()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }

    // Pauses game for menu that requires it
    public void PauseGame()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0;
        IsPaused = true;

        // Hide Board gameObject
        GameObject board = gameObject.transform.Find("Board").gameObject;
        board.SetActive(false);

        Screens[PauseScreen].screen.SetActive(true);
        AudioManager.main.MusicSource.Pause();
    }

    // UnPauses game from menu
    public void UnPauseGame()
    {
        Debug.Log("Game UnPaused");
        Time.timeScale = 1;
        IsPaused = false;

        // Show Board gameObject
        GameObject board = gameObject.transform.Find("Board").gameObject;
        board.SetActive(true);

        Screens[PauseScreen].screen.SetActive(false);
        AudioManager.main.MusicSource.UnPause();
    }

    // Continues game from last saved Level
    public void ContinueGame()
    {
        Debug.Log("Continuing Game");
    }

    // Restarts the current level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title_Scene");
    }

    #endregion

    #region Private Methods



    #endregion
}
