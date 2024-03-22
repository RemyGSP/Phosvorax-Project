using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    const string VCAPath = "vca:/";
    const string generalVCAPath = "General";
    const string musicVCAPath = "Music";
    const string SFXVCAPath = "SFX";

    public static AudioManager Instance { get; private set; }

    VCA generalVCA;
    VCA sfxVCA;
    VCA musicVCA;

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    public void Start()
    {
        Instance = this;
        generalVCA = RuntimeManager.GetVCA(VCAPath + generalVCAPath);
        sfxVCA = RuntimeManager.GetVCA(VCAPath + SFXVCAPath);
        musicVCA = RuntimeManager.GetVCA(VCAPath + musicVCAPath);
        SetSliders();
    }

    public void SetSliders()
    {
        generalVCA.getVolume(out float generalVolume);
        masterSlider.value = generalVolume;

        sfxVCA.getVolume(out float sfxVolume);
        sfxSlider.value = sfxVolume;

        musicVCA.getVolume(out float musicVolume);
        musicSlider.value = musicVolume;
    }
    public void CallOneShot(string eventRoute)
    {
        RuntimeManager.PlayOneShot(eventRoute);
    }

    public void ChangeVolumeMusic(float volume)
    {
        generalVCA.setVolume(volume);
    }

    public void ChangeVolumeSFX(float volume)
    {
        sfxVCA.setVolume(volume);
    }
    public void ChangeVolumeMaster(float volume)
    {
        musicVCA.setVolume(volume);
    }


}
