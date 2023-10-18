using System.Collections.Generic;
using UnityEngine;
using GABFERR.Core.Singleton;
public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;

    public AudioSource musicSource;
    public void PlayMusicByType(MusicType musicType) {
        var music = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return  musicSetups.Find(i => i.musicType == musicType);
 
    }

    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.SFXType == sfxType);

    }

}

public enum MusicType
{
    TYPE01,
    TYPE02,
    TYPE03
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;

}

public enum SFXType
{
    NONE,
    TYPE01,
    TYPE02,
    TYPE03
}

[System.Serializable]
public class SFXSetup
{
    public SFXType SFXType;
    public AudioClip audioClip;

}

