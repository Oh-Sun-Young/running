using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 참고
 * - 유니티에서 데이터 관리하는 법 : https://luv-n-interest.tistory.com/794
 * - Object.DontDestroyOnLoad : https://docs.unity3d.com/kr/530/ScriptReference/Object.DontDestroyOnLoad.html
 */
[System.Serializable]
public class CharacterData
{
    public string name;
    public int life;
    public float power;
    public Sprite img;
}

public class Data : MonoBehaviour
{
    public static Data instance;

    [SerializeField] CharacterData[] data;

    public static float gameSpeed;
    public static int coin;
    private int gameCoin {
        get
        {
            if(m_gameCoin > 9999)
            {
                m_gameCoin = 9999;
            }
            return m_gameCoin;
        }
        set
        {
            m_gameCoin = value;
        }
    }
    private int m_gameCoin;

    public int characterType;
    public int characterLife;
    public float characterPower;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        DontDestroyOnLoad(this);

        gameCoin = (PlayerPrefs.HasKey("Coin") && PlayerPrefs.GetInt("Coin") != 0) ? PlayerPrefs.GetInt("Coin") : 0;
    }

    private void Start()
    {
        gameSpeed = 1.0f;
        coin = 0;
        characterType = 1;

        PlayerPrefs.SetInt("Name", 0);
    }

    // 캐릭터 기본값 세팅
    public void DataInitial(int num)
    {
        characterType = num;
        characterLife = data[num].life;
        characterPower = data[num].power;
    }

    // 캐릭터 종류 확인
    public int GetCharacterType()
    {
        return characterType;
    }

    // 캐릭터 이름 확인
    public string GetCharacterName()
    {
        return data[characterType].name;
    }

    // 캐릭터 생명력 확인
    public int GetCharacterLife()
    {
        return characterLife;
    }

    // 캐릭터 공격력 확인
    public float GetCharacterPower()
    {
        return characterPower;
    }

    // 캐릭터 이미지 확인
    public Sprite GetCharacterImage()
    {
        return data[characterType].img;
    }

    // 코인 확인
    public int GetCoin()
    {
        return gameCoin;
    }

    // 캐릭터 생명력 추가
    public void AddLife()
    {
        characterLife++;
    }

    // 캐릭터 공격력 추가
    public void AddPower()
    {
        characterPower += 5;
    }

    // 코인 업데이트
    public void UpdateCoin(int num)
    {
        gameCoin += num;
    }
}
