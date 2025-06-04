using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    TMP_Text timeText;

    public void ShowGameOver(int finalScore, float finalTime)
    {
        scoreText.text = $"{finalScore}";

        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
