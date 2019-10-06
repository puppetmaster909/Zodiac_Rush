using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// ADD A SMALL SOUND EFFECT AFTER A VOLUME BAR IS CHANGED
public class SettingsMenu : MonoBehaviour
{
    #region MonoBehavior

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

    public Slider BGM;
    public Slider SFX;

    #endregion

    #region Public Methods

    public void Back()
    {
        UIManager.main.ShowScreen(UIManager.main.PreviousScreenName);
    }

    public void SetBGMVolume()
    {
        AudioManager.main.Mixer.SetFloat("BGMVol", BGM.value);
    }

    public void SetSFXVolume()
    {
        AudioManager.main.Mixer.SetFloat("SFXVol", SFX.value);
    }

    #endregion
}
