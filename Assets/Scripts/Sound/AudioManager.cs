using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


        public void Start()
        {
            generalVCA = RuntimeManager.GetVCA(VCAPath + generalVCAPath);
            sfxVCA = RuntimeManager.GetVCA(VCAPath + musicVCAPath);
            musicVCA = RuntimeManager.GetVCA(VCAPath + SFXVCAPath);
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
