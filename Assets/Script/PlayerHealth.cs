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

    public void Start()
    {
        currentHP = maxHP;
        onHealthChanged?.Invoke(currentHP, maxHP);

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // ���� �� ��Ȱ��ȭ
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

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        onHealthChanged?.Invoke(currentHP, maxHP);
    }

}
