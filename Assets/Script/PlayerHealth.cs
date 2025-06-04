using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int maxHP = 30;
    int currentHP;

    public UnityEvent<int, int> onHealthChanged;


    [SerializeField]
    float invincibleDuration = 1.0f; // 무적 시간 (초 단위)
    bool isInvincible = false;

    // 무적 깜빡임
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField] 
    GameObject gameOverUI;

    public void Start()
    {
        currentHP = maxHP;
        onHealthChanged?.Invoke(currentHP, maxHP);

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // 시작 시 비활성화
        }

    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        onHealthChanged?.Invoke(currentHP, maxHP);

        if (currentHP <= 0)
        {
            Debug.Log("사망-게임오버");
            GameOver();
        }

        StartCoroutine(Invincibility());
    }

    void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);

            // 점수와 시간 가져오기
            int score = ScoreManager.Instance.GetScore();
            float time = FindObjectOfType<TimerManager>()?.currentTime ?? 0f;

            // 점수/시간 표시
            GameOverDisplay display = gameOverUI.GetComponent<GameOverDisplay>();
            if (display != null)
            {
                display.ShowGameOver(score, time);
            }

            Time.timeScale = 0f;
        }
    }
    IEnumerator Invincibility()
    {
        isInvincible = true;

        //무적
        // 경과시간
        float elapsed = 0f;
        while (elapsed < invincibleDuration)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled; // 깜빡임
            }

            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        if(spriteRenderer != null)
        {
            spriteRenderer.enabled = true; // 깜빡임 종료시 다시 보이게
        }


        isInvincible = false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        onHealthChanged?.Invoke(currentHP, maxHP);
    }

}
