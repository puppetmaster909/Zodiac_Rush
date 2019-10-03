using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region Static Member

    public static AudioManager main;

    #endregion

    #region MonoBehavior

    private void Awake()
    {
        // Singleton Behavior
        if (main == null)
        {
            main = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
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

    #region Runtime Members

    public AudioSource FxSource;
    public AudioSource MusicSource;

    private string SceneName = "";

    #endregion

    #region Public Methods

    // Plays a sound once
    public void PlaySingle(AudioClip clip)
    {
        FxSource.clip = clip;
        FxSource.PlayOneShot(clip);
    }

    // Plays music consistently
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    #endregion

    #region Private Methods

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Main Menu":
                // PlayMusic(mainMenuMusic);
                break;

            case "Level Selection":
                // PlayMusic(levelSelectMusic);
                break;

            default:
                // PlayMusic(mainMenuMusic);
                break;
        }
    }

    #endregion
}