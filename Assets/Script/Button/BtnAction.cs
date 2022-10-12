using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnAction : MonoBehaviour
{
    public int ScenePage;
    public GameObject popupHudUI;
    public GameObject palsePopupUI;
    public GameObject soundPopupUI;

    ButtonManager theButtonManager;

    private void Awake()
    {
        theButtonManager = GetComponent<ButtonManager>();
    }
    private void Start()
    {
        popupHudUI.SetActive(false);
        palsePopupUI.SetActive(false);
        soundPopupUI.SetActive(false);

        Data.coin = 0;
    }

    public void JumpButtonDown()
    {
        PlayerController.isJump = true;
    }
    public void JumpButtonUp()
    {
        PlayerController.isJump = false;
    }
    public void AttackButtonDown()
    {
        PlayerController.isAttack = true;
    }
    public void AttackButtonUp()
    {
        PlayerController.isAttack = false;
    }
    public void GameSoundOption()
    {
        Time.timeScale = 0;
        popupHudUI.SetActive(true);
        soundPopupUI.SetActive(true);
        theButtonManager.SFX();
        /*
        Slider musicSlider = GameObject.Find("SoundSlider").GetComponent<Slider>();
        musicSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        */
    }
    public void GamePause()
    {
        Time.timeScale = 0;
        popupHudUI.SetActive(true);
        palsePopupUI.SetActive(true);
        theButtonManager.SFX();
    }
    public void GamePlay()
    {
        Time.timeScale = Data.gameSpeed;
        popupHudUI.SetActive(false);
        palsePopupUI.SetActive(false);
        soundPopupUI.SetActive(false);
        theButtonManager.SFX();
    }
    public void GameRestart()
    {
        Time.timeScale = Data.gameSpeed;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        theButtonManager.SFX();

        Data.coin = 0;
    }
    public void SceneMove()
    {
        Time.timeScale = Data.gameSpeed;
        SceneManager.LoadScene(ScenePage);
        theButtonManager.SFX();

        Data.coin = 0;
    }
    public void GameQuit()
    {
        Application.Quit();
        theButtonManager.SFX();
    }
}
