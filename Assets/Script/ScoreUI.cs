using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;

    private void Start()
    {
        UpdateScore(ScoreManager.Instance.GetScore());

        // �̺�Ʈ ����
        ScoreManager.Instance.onScoreChanged.AddListener(UpdateScore);
    }

    void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }
}
