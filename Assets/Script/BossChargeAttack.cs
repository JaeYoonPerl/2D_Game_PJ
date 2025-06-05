using System.Collections;
using UnityEngine;

public class BossChargeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Enemy bossEnemy;
    [SerializeField] private Transform target;

    [Header("Charge Settings")]
    [SerializeField] private float chargeSpeed = 15f;
    [SerializeField] private float chargeDuration = 1f;
    [SerializeField] private int chargeDamage = 3;
    [SerializeField] private float chargeDistance = 5f;

    private bool hasCharged = false;
    private bool isCharging = false;

    public bool IsCharging => isCharging;
    void Start()
    {
        if (bossEnemy == null) bossEnemy = GetComponent<Enemy>();
        if (target == null) target = GameObject.FindWithTag("Player")?.transform;

        bossEnemy.ChangedHPEvent.AddListener(OnHPChanged);
    }

    void OnHPChanged(int currentHP, int maxHP)
    {
        float ratio = (float)currentHP / maxHP;

        if (!hasCharged && ratio <= 0.5f)
        {
            hasCharged = true;
            StartCoroutine(ChargeTowardsDirection());
        }
    }

    IEnumerator ChargeTowardsDirection()
    {
        if (isCharging || target == null) yield break;

        isCharging = true;
        Debug.Log("���� ����!");

        var moveScript = GetComponent<MoveToTarget>();
        if (moveScript != null) moveScript.enabled = false;

        Vector2 dir = (target.position - transform.position).normalized;

        // ������ ����
        rb.velocity = dir * chargeSpeed;

        // ������ �Ÿ���ŭ �̵� �� ����
        float elapsed = 0f;
        while (elapsed < chargeDuration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;

        if (moveScript != null) moveScript.enabled = true;

        isCharging = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCharging) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(chargeDamage);
                Debug.Log("�������� �÷��̾�� ���ظ� ����");
            }
        }
    }
}
