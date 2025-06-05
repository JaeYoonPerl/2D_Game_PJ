using System.Collections;
using UnityEngine;

public class BossChargeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Enemy bossEnemy;
    [SerializeField] private Transform target;

    [Header("Charge Settings")]
    [SerializeField] private float chargeSpeed = 20f;
    [SerializeField] private float overshootDistance = 3f; // �߰� ���� �Ÿ�
    [SerializeField] private int chargeDamage = 3;

    private bool hasCharged = false;
    private bool isCharging = false;
    private Vector2 chargeDirection;

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

        // AI �̵� ��Ȱ��ȭ
        var moveScript = GetComponent<MoveToTarget>();
        if (moveScript != null) moveScript.enabled = false;

        // ���� ���� ����
        chargeDirection = (target.position - transform.position).normalized;

        // ��ǥ ��ġ ��� (�÷��̾� ��ġ + �߰� �Ÿ�)
        Vector2 startPos = transform.position;
        Vector2 targetPos = (Vector2)target.position + chargeDirection * overshootDistance;

        float chargeTime = Vector2.Distance(startPos, targetPos) / chargeSpeed;

        float elapsed = 0f;
        while (elapsed < chargeTime)
        {
            rb.velocity = chargeDirection * chargeSpeed;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ����
        rb.velocity = Vector2.zero;

        // MoveToTarget �ٽ� Ȱ��ȭ
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
