using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Animator m_anim;


    [SerializeField]
    int m_maxHP = 10;

    [SerializeField]
    int m_hp = 10;

    // ���� �̺�Ʈ �߰�
    [SerializeField]
    public UnityEvent<Enemy> KilledEvent;

    [SerializeField]
    public UnityEvent<int, int> ChangedHPEvent;

    [SerializeField]
    int scoreOnKill = 5;

    [field: SerializeField]
    public bool IsBoss { get; private set; } = false;

    [SerializeField] 
    private Guage bossHPBar;


    private void Start()
    {
        // �����Ҷ� ü�� ��ȭ�� �����ڿ��� �˸�
        ChangedHPEvent.Invoke(m_hp, m_maxHP);

        if (IsBoss && bossHPBar != null)
        {
            // ó�� ü�� ǥ��
            bossHPBar.SetGuage((float)m_hp / m_maxHP);
           
            

            // ü�� ���� �� UI ������Ʈ
            ChangedHPEvent.AddListener((cur, max) =>
            {
                bossHPBar.SetGuage((float)cur / max);
                
            });
        }
    }



    // �ν����� �˾��޴� �߰�, �Լ��� ��Ʈ������ ��ȯ
    [ContextMenu(nameof(TakeDamage))]
    void TakeDamage()
    {
        TakeDamage(4);
    }
    public void TakeDamage(int damage)
    {
        m_hp -= damage;
        //�ǰ� �ִϸ��̼� ����
       // m_anim.SetTrigger("Damage");
        // ���� �� ü�� ��ȭ�� �����ڿ��� �˸�
        ChangedHPEvent.Invoke(m_hp, m_maxHP);

        //  �ǰ� �ִϸ��̼�
        if (m_anim != null && m_hp > 0)
        {
            m_anim.SetTrigger("Damage");
        }

        // ü�� 0���Ͻ� ����
        if (m_hp <= 0)
        {
            //  ��� �ִϸ��̼�
            if (m_anim != null)
            {
                m_anim.SetTrigger("Death");
            }


            // ���ھ� �߰�
            ScoreManager.Instance?.AddScore(scoreOnKill);

            //m_anim.SetTrigger("Death");
            // ������ ���� ������Ʈ ����
            Destroy(gameObject, 0.1f);

            // ���� �˸�
            KilledEvent.Invoke(this);

        }
    }


    // �÷��̾� �浹 ���� �� ������
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log($"�浹�� ���: {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

            if (player != null)
            {
                // �÷��̾�� ������
                player.TakeDamage(2);
               
            }
        }
    }


    


}
