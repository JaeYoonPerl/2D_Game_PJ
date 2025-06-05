using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D m_rigid;

    [SerializeField]
    Vector2 m_velocity = Vector2.zero;

    [SerializeField]
    float m_speed = 1.5f;

    [SerializeField]
    Bullet2D m_prefabBullet;

    [SerializeField]
    Animator m_anim; // Animator ����

    [SerializeField] 
    PlayerStatus m_status;


    [SerializeField]
    // ���ݰ� ������ �ð�
    float attackDelay = 0.3f;
    // ������ ���� �ð�
    float lastAttackTime = -999f;

    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        m_velocity = new Vector2(x, y);


        // �ִϸ��̼� �Ķ���� ������Ʈ
        if (m_anim != null)
        
            m_anim.SetFloat("Speed", m_velocity.magnitude); // 0�� �� Idle, �� �̻��̸� Walk
        

        if (Input.GetMouseButtonDown(0)&&Time.time >=lastAttackTime+attackDelay)
        {

            lastAttackTime = Time.time;

            var pos = transform.position;
            var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            var dir = targetPos - pos;

            if (dir.x != 0)
            {
                var scale = transform.localScale;
                scale.x = Mathf.Sign(dir.x) * Mathf.Abs(scale.x); // ���� ����, ũ�� ��ȭ ����
                transform.localScale = scale;
            }

            var obj = Instantiate(m_prefabBullet);
            obj.transform.position = pos;
            obj.Fire(dir.normalized * 5f);
            obj.SetDamage(m_status.AttackPower);
        }


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
