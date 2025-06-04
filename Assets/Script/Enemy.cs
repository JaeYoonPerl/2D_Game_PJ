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

    // 죽음 이벤트 추가
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
        // 시작할때 체력 변화를 구독자에게 알림
        ChangedHPEvent.Invoke(m_hp, m_maxHP);

        if (IsBoss && bossHPBar != null)
        {
            // 처음 체력 표시
            bossHPBar.SetGuage((float)m_hp / m_maxHP);
           
            

            // 체력 변동 시 UI 업데이트
            ChangedHPEvent.AddListener((cur, max) =>
            {
                bossHPBar.SetGuage((float)cur / max);
                
            });
        }
    }



    // 인스펙터 팝업메뉴 추가, 함수명 스트링으로 변환
    [ContextMenu(nameof(TakeDamage))]
    void TakeDamage()
    {
        TakeDamage(4);
    }
    public void TakeDamage(int damage)
    {
        m_hp -= damage;
        //피격 애니메이션 실행
       // m_anim.SetTrigger("Damage");
        // 맞을 때 체력 변화를 구독자에게 알림
        ChangedHPEvent.Invoke(m_hp, m_maxHP);

        //  피격 애니메이션
        if (m_anim != null && m_hp > 0)
        {
            m_anim.SetTrigger("Damage");
        }

        // 체력 0이하시 죽음
        if (m_hp <= 0)
        {
            //  사망 애니메이션
            if (m_anim != null)
            {
                m_anim.SetTrigger("Death");
            }


            // 스코어 추가
            ScoreManager.Instance?.AddScore(scoreOnKill);

            //m_anim.SetTrigger("Death");
            // 죽음시 게임 오브젝트 제거
            Destroy(gameObject, 0.1f);

            // 죽음 알림
            KilledEvent.Invoke(this);

        }
    }


    // 플레이어 충돌 감지 및 데미지
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log($"충돌한 대상: {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

            if (player != null)
            {
                // 플레이어에게 데미지
                player.TakeDamage(2);
               
            }
        }
    }


    


}
