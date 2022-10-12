using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioSource theAudioSource;
    public AudioClip audioClip;

    public void SceneMove(int num)
    {
        SceneManager.LoadScene(num);
        SFX();
    }

    public void GameQuit()
    {
        Application.Quit();
        SFX();
    }

    public void SFX()
    {
        theAudioSource.Stop();
        theAudioSource.PlayOneShot(audioClip);
    }
}
