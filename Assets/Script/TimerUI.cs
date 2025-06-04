using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] TimerManager timerManager;

    private void Start()
    {
        if (timerManager != null)
        {
            timerManager.onTimeChanged.AddListener(UpdateUI);
        }
    }

    void UpdateUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
