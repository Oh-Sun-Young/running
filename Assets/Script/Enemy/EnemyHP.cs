using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHP : MonoBehaviour
{
    public float hp;
    [SerializeField] float enemyHp;
    [SerializeField] Slider sliderHp;
    [SerializeField] Transform damagePosition;
    [SerializeField] GameObject damagePrefab;

    private void OnEnable()
    {
        hp = enemyHp;
    }

    public void Damage()
    {
        float damage = Data.instance.characterPower;

        hp -= damage;
        if (hp < 0) hp = 0;
        sliderHp.value = hp / enemyHp * 100;
        StartCoroutine(DamageEffect(damage));
    }

    IEnumerator DamageEffect(float damage)
    {
        GameObject obj = Instantiate(damagePrefab, damagePosition);
        TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "-" + damage;
        yield return new WaitForSeconds(1f);
        Destroy(obj);
    }
}
