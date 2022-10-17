using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChoiceButtonManger : MonoBehaviour
{
    private GameObject[] player;

    [SerializeField] int maxLife;
    [SerializeField] int maxPower;
    private int cntLife;
    private int cntPower;

    private int payment;

    public bool isStart;

    private ButtonManager theButtonManager;
    private AudioSource theAudioSource;
    public AudioClip[] audioClip;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        theButtonManager = GetComponent<ButtonManager>();
        theAudioSource = theButtonManager.theAudioSource;
    }

    public void GameStart()
    {
        isStart = true;

        theButtonManager.SFX();
    }

    public void ChoiceCharacter()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        GameObject clickPlayer = clickObject.GetComponentInParent<PlayerMoveChoice>().gameObject;
        int type = clickPlayer.GetComponentInParent<PlayerMoveChoice>().type;
        cntLife = 0;
        cntPower = 0;

        for (int i = 0; i < player.Length; i++)
        {
            player[i].transform.Find("ChoiceIcon").gameObject.SetActive((player[i] == clickPlayer) ? true : false);
        }
        Data.instance.DataInitial(type); // 캐릭터 기본값 세팅

        theButtonManager.SFX();

        if (0 < payment)
        {
            Data.instance.UpdateCoin(payment);
            payment = 0;
            StartCoroutine(ChoiceUIManager.instance.CharacterReset());
            SFX(1);
        }
        else
        {
            ChoiceUIManager.instance.CharacterChoice(true);
        }
    }

    public void AddLife()
    {
        int price = 100 * ++cntLife;

        SFX(0);

        if (Data.instance.GetCharacterLife() == maxLife)
        {
            ChoiceUIManager.instance.WarningCheck("life", "This is the maximum.");
            SFX(1);
            return;
        }

        if(Data.instance.GetCoin() < price)
        {
            ChoiceUIManager.instance.WarningCheck("life", "Not enough coins.");
            SFX(1);
            return;
        }

        Data.instance.UpdateCoin(-price);
        payment += price;

        Data.instance.AddLife();
        ChoiceUIManager.instance.UpdateLife();
    }

    public void AddPower()
    {
        int price = 100 * ++cntPower;

        SFX(0);

        if (Data.instance.GetCharacterPower() == maxPower)
        {
            ChoiceUIManager.instance.WarningCheck("power", "This is the maximum.");
            SFX(1);
            return;
        }

        if (Data.instance.GetCoin() < price)
        {
            ChoiceUIManager.instance.WarningCheck("power", "Not enough coins.");
            SFX(1);
            return;
        }

        Data.instance.UpdateCoin(-price);
        payment += price;

        Data.instance.AddPower();
        ChoiceUIManager.instance.UpdatePower();
    }

    private void SFX(int num)
    {
        theAudioSource.Stop();
        theAudioSource.PlayOneShot(audioClip[num]);
    }

}
