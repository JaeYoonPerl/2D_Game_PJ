using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreaBlast : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    [SerializeField] private int damage = 3;
    [SerializeField] private LayerMask enemyLayer;

    private bool isEnabled = false;

    public void EnableSkill()
    {
        isEnabled = true;
        Debug.Log("AreaBlast 스킬 활성화됨");
    }

    private void Update()
    {
        if (!isEnabled) return;

        if (Input.GetKeyDown(KeyCode.F)) // 스킬 키
        {
            Blast();
        }
    }

    private void Blast()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        foreach (var enemy in enemies)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null)
                e.TakeDamage(damage);
        }

        Debug.Log($"폭발 스킬 사용! {enemies.Length}명의 적에게 피해를 줌");
        // TODO: 이펙트 추가
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
