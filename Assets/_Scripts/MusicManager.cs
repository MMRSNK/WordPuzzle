using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : Singleton<MusicManager>
{
    public Slider MusicSlider;
    public Slider SFXSlider;
    public AudioSource musicSourse;
    public AudioSource sfxSoure;

    public AudioClip BackgroundMusic;
    public AudioClip PopSound;
    public AudioClip WrongSound;
    public AudioClip CorrectSound;
    private void Start()
    {
        PlayMusic();
        MusicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        SFXSlider.onValueChanged.AddListener(ChangeSFXVolume);
    }
    private void OnDestroy()
    {
        MusicSlider.onValueChanged.RemoveAllListeners();
        SFXSlider.onValueChanged.RemoveAllListeners();
    }

    private void ChangeSFXVolume(float value)
    {
        sfxSoure.volume = value;
    }

    private void ChangeMusicVolume(float value)
    {
        musicSourse.volume = value;
    }

    public void PlayMusic()
    {
        musicSourse.clip = BackgroundMusic;
        musicSourse.Play();
    }
    public void PlayEffect(ESFXType type)
    {
        switch (type)
        {
            case (ESFXType.pop):
                AudioSource.PlayClipAtPoint(PopSound, transform.position, sfxSoure.volume);
                break;

            case (ESFXType.wrong):
                AudioSource.PlayClipAtPoint(WrongSound, transform.position, sfxSoure.volume);
                break;
            case ESFXType.correct:
                AudioSource.PlayClipAtPoint(CorrectSound, transform.position, sfxSoure.volume);
                break;
        }        
    }
}
public enum ESFXType
{
    pop,
    wrong,
    correct
}
