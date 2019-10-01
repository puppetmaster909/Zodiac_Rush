using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Counter int to track the current screen in the list
    public int CurrentScreen;

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

    }

    void Update()
    {

    }

    #endregion

    #region Public Methods

    // Displays the screen with the given name
    public void ShowScreen(string name)
    {
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

        Screens[CurrentScreen].screen.SetActive(false);
        Screens[index].screen.SetActive(true);
        CurrentScreen = index;
    }

    // Quits application
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }

    #endregion

    #region Private Methods



    #endregion
}
