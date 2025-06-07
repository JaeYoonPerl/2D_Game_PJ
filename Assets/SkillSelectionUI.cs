using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonParent;

    private SkillManager skillManager;

    private void Awake()
    {
        skillManager = FindObjectOfType<SkillManager>();
        gameObject.SetActive(false); // 시작 시 꺼두기
    }

    public void Show(List<SkillData> options)
    {


        Debug.Log("스킬 선택 UI 실행");
        gameObject.SetActive(true);

        // 기존 버튼 제거
        foreach (Transform child in buttonParent)
            Destroy(child.gameObject);

        // 스킬 옵션 표시
        foreach (var skill in options)
        {
            Debug.Log($"버튼 생성됨: {skill.skillName}");
            var obj = Instantiate(buttonPrefab, buttonParent);
            obj.transform.Find("Icon").GetComponent<Image>().sprite = skill.icon;
            obj.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = skill.skillName;
            obj.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>().text = skill.description;

            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                skillManager.AddOrLevelUpSkill(skill);
                gameObject.SetActive(false); // UI 숨기기
            });
        }
    }
}
