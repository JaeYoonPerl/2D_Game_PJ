using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // �÷��̾� ����
    public int playerLevel = 1;
    public int playerExp = 0;
    public int playerHP = 10;
    public int playerMaxHP = 10;

    // ��ų ������
    public List<SkillData> acquiredSkills = new List<SkillData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �ı����� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
