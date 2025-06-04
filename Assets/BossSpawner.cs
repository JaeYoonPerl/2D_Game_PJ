using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab;
    [SerializeField] GameObject bossHPBarUI;
    [SerializeField] Transform playerTransform;

    [SerializeField] float spawnRadius = 20f;
    [SerializeField] float safeDistance = 5f; // �÷��̾�� �ּ� �Ÿ�

    [SerializeField] float spawnTime = 120f;

    private void Start()
    {
        StartCoroutine(SpawnBossAfterTime());
    }


    IEnumerator SpawnBossAfterTime()
    {
        yield return new WaitForSeconds(spawnTime);

        Vector2 spawnPos = GetRandomPositionNearPlayer();

        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        // �÷��̾� Ÿ�� ����
        boss.GetComponent<MoveToTarget>().m_target = playerTransform;

        var enemy = boss.GetComponent<Enemy>();
        if (enemy != null && bossHPBarUI != null)
        {
            enemy.IsBoss = true;
            bossHPBarUI.SetActive(true);

            var guage = bossHPBarUI.GetComponent<Guage>();
            if (guage != null)
            {
                // bossHPBar�� private�̸� �ʵ� ���� �ʿ�
                typeof(Enemy).GetField("bossHPBar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.SetValue(enemy, guage);
            }
        }
    }

    Vector2 GetRandomPositionNearPlayer()
    {
        Vector2 basePos = playerTransform.position;

        Vector2 randomPos;
        int attempt = 0;
        do
        {
            Vector2 offset = Random.insideUnitCircle.normalized * Random.Range(safeDistance, spawnRadius);
            randomPos = basePos + offset;
            attempt++;
        }
        while (Vector2.Distance(randomPos, basePos) < safeDistance && attempt < 10); // �����

        return randomPos;
    }

}
