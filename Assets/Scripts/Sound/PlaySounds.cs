using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    private StudioBankLoader bankLoader;
    private FMOD.Studio.Bank musicBank;
    private FMOD.Studio.Bank sfxBank;
    private FMOD.Studio.Bank masterBank;

    private void Start()
    {
        bankLoader = GetComponent<StudioBankLoader>();
    }
    public void CallOneShot(string eventRoute)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventRoute);
    }

    public void ChangeVolumeMusic(float volume)
    {
    }

    public void ChangeVolumeSFX(float volume)
    {

    }
    public void ChangeVolumeMaster(float volume)
    {

    }


}
