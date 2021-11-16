using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager
{
    public AudioManager(GameFacade gameFacade) : base(gameFacade) { }
    private const string Sound_Prefix = "Sounds/";
    public const string Sound_Alert = "Alert";
    public const string Sound_ArrowShot = "ArrowShoot";
    public const string Sound_Bg_Fast = "Bg(fast)";
    public const string Sound_Bg_Moderate = "Bg(moderate)";
    public const string Sound_ButtonClick = "ButtonClick";
    public const string Sound_Miss = "Miss";
    public const string Sound_ShootPerson = "ShootPerson";
    public const string Sound_Sound_Timer = "Timer";

    private AudioSource bgAudioSource;
    private AudioSource normalAudioSource;
    public override void OnInit()
    {
        base.OnInit();
        GameObject audioSourceGO = new GameObject("AudioSource(GameObject)");
        bgAudioSource = audioSourceGO.AddComponent<AudioSource>();
        normalAudioSource = audioSourceGO.AddComponent<AudioSource>();

        //PlaySound(bgAudioSource, LoadSound(Sound_Bg_Moderate), true);
    }
    public void PlayBgSound(string soundName)
    {
        PlaySound(bgAudioSource, LoadSound(soundName), true,0.8f);
    }
    public void PlayNormalSound(string soundName)
    {
        PlaySound(bgAudioSource, LoadSound(soundName));
    }
    private void PlaySound(AudioSource audioSource,AudioClip clip,bool loop=false, float volume = 1)
    {
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
    private AudioClip LoadSound(string soundsName)
    {
        return Resources.Load<AudioClip>(Sound_Prefix + soundsName);
        //return null;
    }
}
