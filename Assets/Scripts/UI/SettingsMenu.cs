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
    public Slider BGMslider;
    public Slider SFXslider;

    // Start is called before the first frame update
    void Start()
    {

        AudioManager.main.Mixer.SetFloat("BGMVol", PlayerPrefs.GetFloat("BGMVol"));
        AudioManager.main.Mixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
        SFXslider.value = PlayerPrefs.GetFloat("SFXVol");
        BGMslider.value = PlayerPrefs.GetFloat("BGMvol");

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
        PlayerPrefs.SetFloat("BGMVol", BGM.value);
    }

    public void SetSFXVolume()
    {
        AudioManager.main.Mixer.SetFloat("SFXVol", SFX.value);
        PlayerPrefs.SetFloat("SFXVol", SFX.value);
    }

    public void muteBGM()
    {
        bool muted = BGMmute.GetComponent<Toggle>().isOn;

        if(muted)
        {
            BGM.value = -45;
        }
        else
        {
            BGM.value = SFX.value = PlayerPrefs.GetFloat("BGMVol");
        }
    }

    public void muteSFX()
    {
        bool muted = SFXmute.GetComponent<Toggle>().isOn;

        if (muted)
        {
            SFX.value = -45;
        }
        else
        {
            SFX.value = PlayerPrefs.GetFloat("SFXVol");
        }
    }
    #endregion
}
