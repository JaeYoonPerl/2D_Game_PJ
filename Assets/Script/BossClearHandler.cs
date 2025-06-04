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

        clearUI.ShowClear(score, time); // 안전하게 연결된 상태에서 호출
    }
}
