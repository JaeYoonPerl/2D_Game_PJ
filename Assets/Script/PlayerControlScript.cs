using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    [Header("�̵� ����")]
    [SerializeField] private Rigidbody2D m_rigid;
    [SerializeField] private float m_speed = 1.5f;
    private Vector2 m_velocity = Vector2.zero;

    [Header("���� ����")]
    [SerializeField] private float attackDelay = 0.3f;
    private float lastAttackTime = -999f;

    [SerializeField] private PlayerShooting m_shooting;

    [Header("��Ÿ")]
    [SerializeField] private Animator m_anim;
    [SerializeField] private PlayerStatus m_status;

    void Update()
    {
        // �̵� �Է� ó��
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        m_velocity = new Vector2(x, y);

        // �ִϸ��̼� ó��
        if (m_anim != null)
        {
            m_anim.SetFloat("Speed", m_velocity.magnitude);
        }

        // ���� ó��
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackDelay)
        {
            lastAttackTime = Time.time;

            var pos = transform.position;
            var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            var dir = targetPos - pos;

            if (dir.x != 0)
            {
                var scale = transform.localScale;
                scale.x = Mathf.Sign(dir.x) * Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            m_shooting.Fire(dir.normalized);
        }


        // �׽�Ʈ�� ������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<PlayerHealth>()?.TakeDamage(2);
        }
    }

    void FixedUpdate()
    {
        m_rigid.velocity = m_velocity * m_status.stats.moveSpeed;
    }

    private void OnDrawGizmos()
    {
        var pos = transform.position;
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;

        var dir = targetPos - pos;
        Debug.DrawLine(pos, pos + dir, Color.blue);
        Debug.DrawLine(pos, pos + dir.normalized, Color.green);
    }
}
