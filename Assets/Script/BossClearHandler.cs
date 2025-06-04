using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClearHandler : MonoBehaviour
{
    [SerializeField] ClearUI clearUI;

    public void OnBossKilled()
    {
        int score = ScoreManager.Instance.GetScore();
        float time = Time.timeSinceLevelLoad;

        clearUI.ShowClear(score, time); // �����ϰ� ����� ���¿��� ȣ��
    }
}
