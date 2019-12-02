using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// ADD A SMALL SOUND EFFECT AFTER A VOLUME BAR IS CHANGED
public class SettingsMenu : MonoBehaviour
{
    #region MonoBehavior
    public Toggle BGMmute;
    public Toggle SFXmute;

    
    #region Runtime Members
    private float previousBGMVol, previousSFXVol;
    public Slider BGM;
    public Slider SFX;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        previousBGMVol = PlayerPrefs.GetFloat("BGMVol");
        previousSFXVol = PlayerPrefs.GetFloat("SFXVol");
        //AudioManager.main.Mixer.SetFloat("BGMVol", PlayerPrefs.GetFloat("BGMVol"));
        //AudioManager.main.Mixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
        
        SFX.value = PlayerPrefs.GetFloat("SFXVol");
        BGM.value = PlayerPrefs.GetFloat("BGMVol");

    }

    #endregion

    

    #region Public Methods

    public void Back()
    {
        UIManager.main.ShowScreen(UIManager.main.PreviousScreenName);
    }

    
    public void SetBGMVolume()
    {
        AudioManager.main.Mixer.SetFloat("BGMVol", BGM.value);
        PlayerPrefs.SetFloat("BGMVol", BGM.value);
    }

    public void SetSFXVolume()
    {
        AudioManager.main.Mixer.SetFloat("SFXVol", SFX.value);
        PlayerPrefs.SetFloat("SFXVol", SFX.value);
    }

    public void muteBGM()
    {
        bool BGMmuted = BGMmute.GetComponent<Toggle>().isOn;

        if(BGMmuted)
        {
            BGM.value = -45;
        }
        else
        {
            BGM.value = previousBGMVol;
        }
    }

    public void muteSFX()
    {
        bool SFXmuted = SFXmute.GetComponent<Toggle>().isOn;

        if (SFXmuted)
        {
            SFX.value = -45;
        }
        else
        {
            SFX.value = previousSFXVol;
        }
    }
    #endregion
}
