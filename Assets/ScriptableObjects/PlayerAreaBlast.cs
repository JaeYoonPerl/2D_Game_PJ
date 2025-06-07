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
        Debug.Log("AreaBlast ��ų Ȱ��ȭ��");
    }

    private void Update()
    {
        if (!isEnabled) return;

        if (Input.GetKeyDown(KeyCode.F)) // ��ų Ű
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

        Debug.Log($"���� ��ų ���! {enemies.Length}���� ������ ���ظ� ��");
        // TODO: ����Ʈ �߰�
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
