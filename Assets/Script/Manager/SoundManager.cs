using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider musicSlider;

    private float volume;

    private void Awake()
    {
        volume = PlayerPrefs.HasKey("SoundVolume") ? PlayerPrefs.GetFloat("SoundVolume") : 0.75f;
        musicSource.volume = volume;
        musicSlider.value = volume;
    }

    public void SetMusicVolume()
    {
        /* 이슈 발생
         * NullReferenceException: Object reference not set to an instance of an object
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        volume = clickObject.GetComponent<Slider>().value;
        */
        volume = musicSlider.value;
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }
}
