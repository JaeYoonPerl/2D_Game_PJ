using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSelectUI : MonoBehaviour
{
    [System.Serializable]
    public class SkillOption
    {
        public Button button;
        public Image icon;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
    }

    public List<SkillOption> options;
    private List<SkillData> currentChoices = new List<SkillData>();

    public void Show(List<SkillData> skills)
    {
        currentChoices = skills;

        for (int i = 0; i < options.Count; i++)
        {
            var opt = options[i];
            var skill = skills[i];

            opt.icon.sprite = skill.icon;
            opt.nameText.text = skill.skillName;
            opt.descriptionText.text = skill.description;

            int index = i; // local copy
            opt.button.onClick.RemoveAllListeners();
            opt.button.onClick.AddListener(() => SelectSkill(index));
        }

        gameObject.SetActive(true);
    }

    void SelectSkill(int index)
    {
        var selected = currentChoices[index];

        // SkillManager에 등록
        FindObjectOfType<SkillManager>()?.AddOrLevelUpSkill(selected);

        // 선택 UI 숨기기
        gameObject.SetActive(false);
    }
}
