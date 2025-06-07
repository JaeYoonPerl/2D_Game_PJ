using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButtonUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text descText;
    private SkillData skill;

    public void Setup(SkillData data, System.Action<SkillData> onClick)
    {
        skill = data;
        icon.sprite = data.icon;
        nameText.text = data.skillName;
        descText.text = data.description;

        GetComponent<Button>().onClick.AddListener(() => onClick?.Invoke(skill));
    }
}
