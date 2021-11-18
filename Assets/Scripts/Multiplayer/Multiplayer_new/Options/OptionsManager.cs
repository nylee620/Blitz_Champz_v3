
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class OptionsManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MusicPref = "MusicPref";
    private static readonly string EffectPref = "EffectPref";
    private static readonly string MutedPref = "MutedPref";

    
    public Slider musicSlider, effectSlider;
    public AudioMixer musicMixer,effectMixer;
    
    private int firstPlayInt;
    private float musicFloat, effectFloat;
    private bool isMuted;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        
        if(firstPlayInt == 0)
        {
            musicFloat = .5f;
            effectFloat = .25f;

            musicSlider.value = musicFloat;
            effectSlider.value = effectFloat;
            isMuted = false;
            PlayerPrefs.SetFloat(MusicPref, musicFloat);
            PlayerPrefs.SetFloat(EffectPref, effectFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }

        else
        {
            isMuted = false;
            musicFloat = PlayerPrefs.GetFloat(MusicPref);
            musicSlider.value = musicFloat;

            effectFloat = PlayerPrefs.GetFloat(EffectPref);
            effectSlider.value = effectFloat;

        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value);
        PlayerPrefs.SetFloat(EffectPref, effectSlider.value);
        PlayerPrefs.SetInt(FirstPlay, -1);
        Debug.Log("saved");

    }

    private void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSettings();
            Debug.Log("notfocus");
        }
    }

    public void UpdateMusicVol(float musicVal)
    {
        //Debug.Log("Music Updated");
        musicMixer.SetFloat("MusicVol", Mathf.Log10(musicVal) * 20);
    }

    public void UpdateEffectVol(float effectVal)
    {
        //Debug.Log("Effect Updated");
        effectMixer.SetFloat("EffectVol", Mathf.Log10(effectVal) * 20);
    }

    public void MuteSounds()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }
}
