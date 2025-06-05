using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{

    [SerializeField]
    private GameObject levelUpEffectPrefab;

    [SerializeField]
    private TextMeshProUGUI levelUpText;

    [SerializeField]
    private float textShowTime = 1.5f;

    public PlayerStats stats = new PlayerStats();


    public void GainExperience(int amount)
    {

        int previousLevel = stats.level;
        stats.GainExp(amount);

        if (stats.level > previousLevel)
        {
            Debug.Log($"레벨 업! {previousLevel} → {stats.level}");

            ShowLevelUpEffect(); // ← 이펙트 실행!
            ShowLevelUpText();   // ← 텍스트 띄우기 

            var health = GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.FullyHeal(); // 레벨업 시 체력 완전 회복
            }
        }

        // 체력바 갱신 (레벨업하지 않아도 필요할 수 있음)
        GetComponent<PlayerHealth>()?.UpdateHealthUI();
    }

    void ShowLevelUpEffect()
    {
        if (levelUpEffectPrefab != null)
        {
            // 이펙트 생성
            var effect = Instantiate(levelUpEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        if (levelUpText != null)
            StartCoroutine(ShowLevelUpText());
    }

    IEnumerator ShowLevelUpText()
    {
        levelUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(textShowTime);
        levelUpText.gameObject.SetActive(false);
    }



    public int AttackPower => stats.attackPower;
    public float MoveSpeed => stats.moveSpeed;
    public int MaxHP => stats.maxHP;



}
