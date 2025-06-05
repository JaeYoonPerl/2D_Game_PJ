using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D m_rigid;

    [SerializeField]
    float m_speed = 2f;
    int m_damage = 1; // ±âº»°ª
    public void SetDamage(int value)
    {
        m_damage = value;
    }

    [ContextMenu(nameof(Fire))]
    void Fire()
    {
        m_rigid.velocity = transform.right * m_speed;
    }

    public void Fire(Vector3 v)
    {
        m_rigid.velocity = v;
        transform.right = v;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"OnCollisionEnter2D:{collision.gameObject.name}");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(m_damage);
            }

            Destroy(gameObject);
        }
    }


}
