using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 플레이어 상태
    public int playerLevel = 1;
    public int playerExp = 0;
    public int playerHP = 10;
    public int playerMaxHP = 10;

    // 스킬 데이터
    public List<SkillData> acquiredSkills = new List<SkillData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
