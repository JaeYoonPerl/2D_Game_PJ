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
    float invincibleDuration = 1.0f; // ���� �ð� (�� ����)
    bool isInvincible = false;

    // ���� ������
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField] 
    GameObject gameOverUI;

    [SerializeField]
    PlayerStatus m_status;
    [SerializeField]
    Guage healthGuage;

    public int CurrentHP => currentHP;     //  �ܺ� ���ٿ� ������Ƽ
    public int MaxHP => maxHP;

    public void Start()
    {
        maxHP = m_status.MaxHP;

        // GameManager���� ���� ������ �װ� ���
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
            gameOverUI.SetActive(false); // ���� �� ��Ȱ��ȭ
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
            Debug.Log("���-���ӿ���");
            GameOver();
        }

        StartCoroutine(Invincibility());
    }

    void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);

            // ������ �ð� ��������
            int score = ScoreManager.Instance.GetScore();
            float time = FindObjectOfType<TimerManager>()?.currentTime ?? 0f;

            // ����/�ð� ǥ��
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

        //����
        // ����ð�
        float elapsed = 0f;
        while (elapsed < invincibleDuration)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled; // ������
            }

            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        if(spriteRenderer != null)
        {
            spriteRenderer.enabled = true; // ������ ����� �ٽ� ���̰�
        }


        isInvincible = false;
    }

    public void FullyHeal()
    {
        currentHP = m_status.MaxHP;
        onHealthChanged?.Invoke(currentHP, m_status.MaxHP);
        UpdateHealthUI(); // ü�¹� ����
    }

    public void SetHP(int current, int max)
    {
        maxHP = max;
        currentHP = Mathf.Clamp(current, 0, maxHP);
        UpdateHealthUI(); // ü�� UI ���� �Լ��� �ִٸ� ���� ȣ��
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, m_status.MaxHP);
        onHealthChanged?.Invoke(currentHP, m_status.MaxHP);
        UpdateHealthUI();
    }

}
