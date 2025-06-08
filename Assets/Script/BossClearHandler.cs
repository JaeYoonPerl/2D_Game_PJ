using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClearHandler : MonoBehaviour
{
    [SerializeField] ClearUI clearUI;
    [SerializeField] GoFinal goFinal;

    private void Awake()
    {
        if (clearUI == null)
            clearUI = FindObjectOfType<ClearUI>();
        if (goFinal == null)
            goFinal = FindObjectOfType<GoFinal>();
    }

    public void OnBossKilled()
    {
        int score = ScoreManager.Instance.GetScore();
        float time = Time.timeSinceLevelLoad;

        if (clearUI != null)
            clearUI.ShowClear(score, time);
        else
            Debug.LogWarning("ClearUI�� ������� �ʾҽ��ϴ�.");

        if (goFinal != null)
            goFinal.ShowClear(score, time);
        else
            Debug.LogWarning("GoFinal�� ������� �ʾҽ��ϴ�.");
    }
}
