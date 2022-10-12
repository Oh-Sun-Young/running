using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChoiceUIManager : MonoBehaviour
{
    public static ChoiceUIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<ChoiceUIManager>();
            }
            return m_instance;
        }
    }
    private static ChoiceUIManager m_instance;

    [SerializeField] GameObject choiceNone = null;
    [SerializeField] GameObject characterInfo = null;
    [SerializeField] GameObject characterReset = null;

    [SerializeField] TextMeshProUGUI userCoin = null;

    [SerializeField] Image characterImage = null;
    [SerializeField] TextMeshProUGUI characterID = null;
    [SerializeField] TextMeshProUGUI characterName = null;
    [SerializeField] TextMeshProUGUI characterLife = null;
    [SerializeField] TextMeshProUGUI characterPower = null;

    [SerializeField] CanvasGroup SpeechBubbleLife = null;
    [SerializeField] TextMeshProUGUI warningLife = null;
    [SerializeField] CanvasGroup SpeechBubblePower = null;
    [SerializeField] TextMeshProUGUI warningPower = null;

    TextAnimation theTextAnimation;

    public bool warningLifeCheck;
    public bool warningPowerCheck;

    public int coin;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        theTextAnimation = GetComponent<TextAnimation>();
    }

    private void Start()
    {
        CharacterChoice(false);
        coin = Data.instance.GetCoin();
    }

    public void Update()
    {
        userCoin.text = string.Empty + Data.instance.GetCoin();
    }

    // 캐릭터 선택 여부 체크 및 데이터 기입
    public void CharacterChoice(bool active)
    {
        choiceNone.SetActive(!active);
        characterInfo.SetActive(active);
        characterReset.SetActive(false);

        if (active)
        {
            if (theTextAnimation.enabled)
            {
                theTextAnimation.enabled = false;
            }

            // 데이터 기입
            characterImage.sprite = Data.instance.GetCharacterImage();
            characterName.text = Data.instance.GetCharacterName();
            UpdateLife();
            UpdatePower();

            int id = ChangeBinary(Data.instance.GetCharacterType());
            characterID.text = "ID. " + ((id < 10) ? "000" : ((id < 100) ? "00" : ((id < 1000) ? "0" : ""))) + id;
        }
        else
        {
            if (!theTextAnimation.enabled)
            {
                theTextAnimation.enabled = false;
            }
        }
    }

    public void UpdateLife()
    {
        characterLife.text = string.Empty + Data.instance.GetCharacterLife();
    }

    public void UpdatePower()
    {
        characterPower.text = string.Empty + Data.instance.GetCharacterPower();
    }

    // 10진수를 2진수로 변경
    private int ChangeBinary(int num)
    {
        int result = 0;
        int cnt = 0;

        while (Mathf.Pow(2, cnt) < num)
        {
            cnt++;
        }

        for (int i = 0; i < cnt; i++)
        {
            if(Mathf.Pow(2, cnt - i) < num)
            {
                num -= (int)Mathf.Pow(2, cnt - i);
                result += (int)Mathf.Pow(10, cnt - i);
            }
        }


        if (num == 1)
        {
            num -= 1;
            result += 1;
        }

        return result;
    }

    public void WarningCheck(string type = "life", string text = "success")
    {
        if (type == "power" && warningPowerCheck || type == "life" && warningLifeCheck)
        {
            return;
        }

        StartCoroutine(Warning(type, text));
    }

    // 캐릭터 안내 문구
    private IEnumerator Warning(string type, string text)
    {
        CanvasGroup canvas;
        TextMeshProUGUI warning;

        if (type == "power")
        {
            canvas = SpeechBubblePower;
            warning = warningPower;
            warningPowerCheck = true;
        }
        else
        {
            canvas = SpeechBubbleLife;
            warning = warningLife;
            warningLifeCheck = true;
        }

        warning.text = text;
        
        while (canvas.alpha < 1)
        {
            canvas.alpha = Mathf.Lerp(canvas.alpha, 1, 0.5f);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.25f);

        while (0.01f < canvas.alpha)
        {
            canvas.alpha = Mathf.Lerp(canvas.alpha, 0, 0.5f);
            yield return new WaitForSeconds(0.01f);
        }


        if (type == "power")
        {
            warningPowerCheck = false;
        }
        else
        {
            warningLifeCheck = false;
        }
    }

    public IEnumerator CharacterReset()
    {
        characterInfo.SetActive(false);
        characterReset.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        CharacterChoice(true);
    }
}
