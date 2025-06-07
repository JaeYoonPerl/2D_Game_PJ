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
        gameObject.SetActive(false); // ���� �� ���α�
    }

    public void Show(List<SkillData> options)
    {


        Debug.Log("��ų ���� UI ����");
        gameObject.SetActive(true);

        // ���� ��ư ����
        foreach (Transform child in buttonParent)
            Destroy(child.gameObject);

        // ��ų �ɼ� ǥ��
        foreach (var skill in options)
        {
            Debug.Log($"��ư ������: {skill.skillName}");
            var obj = Instantiate(buttonPrefab, buttonParent);
            obj.transform.Find("Icon").GetComponent<Image>().sprite = skill.icon;
            obj.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = skill.skillName;
            obj.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>().text = skill.description;

            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                skillManager.AddOrLevelUpSkill(skill);
                gameObject.SetActive(false); // UI �����
            });
        }
    }
}
