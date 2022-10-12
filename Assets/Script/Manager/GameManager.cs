using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;
    public TextMeshProUGUI coin;
    public GameObject popupHudUI;
    public GameObject gameoverUI;

    public AudioSource musicSource;
    public Slider musicSlider;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두 개 이상의 개임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameoverUI.SetActive(false);

        float volume = PlayerPrefs.GetFloat("SoundVolume");
    }
    public void OnPlayerDead()
    {
        Time.timeScale = 0;
        popupHudUI.SetActive(true);
        gameoverUI.SetActive(true);

        Data.instance.UpdateCoin(Data.coin);
        int all = Data.instance.GetCoin();
        coin.text = string.Empty + all;
        PlayerPrefs.SetInt("Coin", all);
    }
}
