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

    [SerializeField]
    PlayerStatus m_status;
    [SerializeField]
    Guage healthGuage;

    public int CurrentHP => currentHP;     //  외부 접근용 프로퍼티
    public int MaxHP => maxHP;

    public void Start()
    {
        maxHP = m_status.MaxHP;

        // GameManager에서 값이 있으면 그걸 사용
        if (GameManager.Instance != null)
        {
            SetHP(GameManager.Instance.playerHP, GameManager.Instance.playerMaxHP);
        }
        else
        {
            currentHP = maxHP;
            UpdateHealthUI();
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // 시작 시 비활성화
        }

    }
    public void UpdateHealthUI()
    {
        if (healthGuage != null)
        {
            healthGuage.SetGuage((float)currentHP / m_status.MaxHP);
            healthGuage.SetLable(currentHP.ToString());
            healthGuage.SetLableMax(m_status.MaxHP.ToString());
        }
    }
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, m_status.MaxHP);

        onHealthChanged?.Invoke(currentHP, m_status.MaxHP);
        UpdateHealthUI();

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

    public void FullyHeal()
    {
        currentHP = m_status.MaxHP;
        onHealthChanged?.Invoke(currentHP, m_status.MaxHP);
        UpdateHealthUI(); // 체력바 갱신
    }

    public void SetHP(int current, int max)
    {
        maxHP = max;
        currentHP = Mathf.Clamp(current, 0, maxHP);
        UpdateHealthUI(); // 체력 UI 갱신 함수가 있다면 같이 호출
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, m_status.MaxHP);
        onHealthChanged?.Invoke(currentHP, m_status.MaxHP);
        UpdateHealthUI();
    }

}
