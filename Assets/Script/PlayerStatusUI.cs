using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI speedText;

    [SerializeField] PlayerStatus playerStatus;

    void Update()
    {
        if (playerStatus == null) return;

        levelText.text = $"Level: {playerStatus.stats.level}";
        expText.text = $"EXP: {playerStatus.stats.currentExp}/{playerStatus.stats.expToNextLevel}";
        atkText.text = $"ATK: {playerStatus.stats.attackPower}";
        speedText.text = $"SPD: {playerStatus.stats.moveSpeed:0.0}";
    }
}
