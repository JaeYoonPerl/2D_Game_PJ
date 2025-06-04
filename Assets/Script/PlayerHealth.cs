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

    public void Start()
    {
        currentHP = maxHP;
        onHealthChanged?.Invoke(currentHP, maxHP);

    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        onHealthChanged?.Invoke(currentHP, maxHP);

        if (currentHP <= 0)
        {
            Debug.Log("사망");
        }

        StartCoroutine(Invincibility());
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
