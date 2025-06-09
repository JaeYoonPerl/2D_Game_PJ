using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    [Header("레벨업 관련")]
    [SerializeField] private GameObject levelUpEffectPrefab;
    [SerializeField] private TextMeshProUGUI levelUpText;
    [SerializeField] private float textShowTime = 1.5f;

    [Header("스킬 관련")]
    [SerializeField] private SkillSelectionUI skillUI;
    [SerializeField] private List<SkillData> allSkills;


    [SerializeField] private SkillSelectUI skillSelectUI;
    [SerializeField] private SkillDatabase skillDatabase;



    public PlayerStats stats = new PlayerStats();

    private SkillManager skillManager;

    private void Start()
    {
        skillManager = GetComponent<SkillManager>();
        if (GameManager.Instance != null)
        {
            stats.level = GameManager.Instance.playerLevel;
            stats.currentExp = GameManager.Instance.playerExp;
            stats.attackPower = GameManager.Instance.attackPower;
            stats.moveSpeed = GameManager.Instance.moveSpeed;
            stats.expToNextLevel = GameManager.Instance.expToNextLevel;

            var health = GetComponent<PlayerHealth>();
            health.SetHP(GameManager.Instance.playerHP, GameManager.Instance.playerMaxHP);

            foreach (var skill in GameManager.Instance.acquiredSkills)
            {
                skillManager.AddOrLevelUpSkill(skill); // 스킬 등록
            }
        }

    }

    public void GainExperience(int amount)
    {
        int previousLevel = stats.level;
        stats.GainExp(amount);

        if (stats.level > previousLevel)
        {
            Debug.Log($"레벨 업! {previousLevel} → {stats.level}");

            ShowLevelUpEffect();
            //ShowLevelUpText();
            StartCoroutine(ShowLevelUpText());

            // 체력 회복
            var health = GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.FullyHeal();
                health.UpdateHealthUI();
            }

           
        Debug.Log("ShowSkillSelection 호출!");
            //  스킬 선택 UI 띄우기
            ShowSkillSelection();
        }
        else
        {
            GetComponent<PlayerHealth>()?.UpdateHealthUI();
        }
    }

    void ShowLevelUpEffect()
    {
        if (levelUpEffectPrefab != null)
        {
            var effect = Instantiate(levelUpEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }

    IEnumerator ShowLevelUpText()
    {
        levelUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(textShowTime);
        levelUpText.gameObject.SetActive(false);
    }

    void ShowSkillSelection()
    {

        Debug.Log("스킬 선택 UI 실행");
        var allSkills = skillDatabase.allSkills;

        // 3개 랜덤 선택
        List<SkillData> selectedSkills = new List<SkillData>();
        List<SkillData> tempList = new List<SkillData>(allSkills);

        for (int i = 0; i < 3 && tempList.Count > 0; i++)
        {
            int rand = Random.Range(0, tempList.Count);
            selectedSkills.Add(tempList[rand]);
            tempList.RemoveAt(rand);
        }

        skillSelectUI.Show(selectedSkills);
    }

    List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> result = new List<SkillData>();
        List<SkillData> candidates = new List<SkillData>(allSkills);

        while (result.Count < count && candidates.Count > 0)
        {
            int index = Random.Range(0, candidates.Count);
            result.Add(candidates[index]);
            candidates.RemoveAt(index);
        }

        return result;
    }

    public int AttackPower => stats.attackPower;
    public float MoveSpeed => stats.moveSpeed;
    public int MaxHP => stats.maxHP;
}
