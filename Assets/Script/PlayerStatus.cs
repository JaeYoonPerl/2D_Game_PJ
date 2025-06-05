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
            Debug.Log($"���� ��! {previousLevel} �� {stats.level}");

            ShowLevelUpEffect(); // �� ����Ʈ ����!
            ShowLevelUpText();   // �� �ؽ�Ʈ ���� 

            var health = GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.FullyHeal(); // ������ �� ü�� ���� ȸ��
            }
        }

        // ü�¹� ���� (���������� �ʾƵ� �ʿ��� �� ����)
        GetComponent<PlayerHealth>()?.UpdateHealthUI();
    }

    void ShowLevelUpEffect()
    {
        if (levelUpEffectPrefab != null)
        {
            // ����Ʈ ����
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
