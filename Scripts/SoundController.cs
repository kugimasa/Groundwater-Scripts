using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    // HomeBGM is in audioSource
    AudioSource audioSource;
    public AudioClip MainBGM;
    public AudioClip StageBGM;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMainBGM();
    }

    public void ChangeBGMVolume()
    {
        audioSource.volume = GameObject.Find("BGMSlider").GetComponent<Slider>().normalizedValue;
        GameObject.Find("SettingCanvas").GetComponent<AudioSource>().Play();
    }

    public void ChangeSEVolume()
    {
        GameObject.Find("Player").GetComponent<AudioSource>().volume = GameObject.Find("SESlider").GetComponent<Slider>().normalizedValue;
        GameObject.Find("SettingCanvas").GetComponent<AudioSource>().volume = GameObject.Find("SESlider").GetComponent<Slider>().normalizedValue;
        GameObject.Find("SettingCanvas").GetComponent<AudioSource>().Play();
    }

    public void PlayMainBGM()
    {
        audioSource.clip = MainBGM;
        audioSource.Play();
    }

    public void PlayStageBGM()
    {        
        audioSource.clip = StageBGM;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public IEnumerator FadeOut(float fadeDuration)
    {
        float startVolue = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolue * Time.deltaTime / fadeDuration;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolue;
    }

}
