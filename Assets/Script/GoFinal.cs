using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoFinal : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timeText;

    public void ShowClear(int finalScore, float finalTime)
    {
        scoreText.text = $"{finalScore}";

        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);
        timeText.text = $"{minutes:00}:{seconds:00}";

        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoNextStage()
    {
        var player = FindObjectOfType<PlayerStatus>();
        var health = player.GetComponent<PlayerHealth>();
        var skillManager = player.GetComponent<SkillManager>();

        GameManager.Instance.playerLevel = player.stats.level;
        GameManager.Instance.playerExp = player.stats.currentExp;
        GameManager.Instance.playerHP = health.CurrentHP;
        GameManager.Instance.playerMaxHP = health.MaxHP;
        GameManager.Instance.acquiredSkills = new List<SkillData>();

        foreach (var skill in skillManager.acquiredSkills)
        {
            GameManager.Instance.acquiredSkills.Add(skill.data);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("FinalBossScene");
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
