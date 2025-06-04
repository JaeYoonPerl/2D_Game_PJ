using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
   public static ScoreManager Instance {  get; private set; }
    public int CurrentScore => score;
    private int score = 0;

    public UnityEvent<int> onScoreChanged;

    private void Awake()
    {
        if(Instance != null & Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환시 유지?
    }
    public void AddScore(int amount)
    {
        score += amount;
        onScoreChanged?.Invoke(score);
    }
    public int GetScore()
    {
        return score;
    }
}
