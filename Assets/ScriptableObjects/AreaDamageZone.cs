using System.Collections;
using UnityEngine;

public class AreaDamageZone : MonoBehaviour
{
    private float radius;
    private int damage;
    private LayerMask enemyLayer;


    public void Init(float radius, int damage, float duration, LayerMask enemyLayer)
    {
        this.radius = radius;
        this.damage = damage;
        this.enemyLayer = enemyLayer;

        StartCoroutine(DamageCoroutine());
    }

    public void UpdateStats(float newRadius, int newDamage)
    {
        this.radius = newRadius;
        this.damage = newDamage;
    }

    private IEnumerator DamageCoroutine()
    {
        while (true)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
            foreach (var hit in hits)
            {
                var enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
