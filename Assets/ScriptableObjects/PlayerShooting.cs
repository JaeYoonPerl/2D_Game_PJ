using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Bullet2D bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PlayerStatus status;

    private int multiShotLevel = 1;

    public void EnableMultiShot()
    {
        multiShotLevel++;
        Debug.Log($"멀티샷 레벨: {multiShotLevel}");
    }

    public void Fire(Vector2 direction)
    {
        Shoot(direction.normalized);
    }

    public void Shoot(Vector2 direction)
    {
        int bulletCount = GetBulletCountByLevel(multiShotLevel);
        float angleStep = 10f; // 각 총알 간 각도

        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startAngle = baseAngle - (angleStep * (bulletCount - 1) / 2);

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

            var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.Fire(dir.normalized * 5f);
            bullet.SetDamage(status.AttackPower);
        }
    }

    int GetBulletCountByLevel(int level)
    {
        switch (level)
        {
            case 1: return 1;
            case 2: return 3;
            case 3: return 5;
            case 4: return 7;
            default: return 9;
        }
    }
}
