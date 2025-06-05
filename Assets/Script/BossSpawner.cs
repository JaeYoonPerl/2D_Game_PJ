using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("보스 설정")]
    [SerializeField] GameObject bossPrefab;
    [SerializeField] GameObject bossHPBarUI;

    [Header("타겟")]
    [SerializeField] Transform playerTransform;

    [Header("스폰 설정")]
    [SerializeField] float spawnRadius = 20f;
    [SerializeField] float safeDistance = 5f;
    [SerializeField] float spawnTime = 120f;

    private void Start()
    {
        StartCoroutine(SpawnBossAfterTime());
    }

    IEnumerator SpawnBossAfterTime()
    {
        yield return new WaitForSeconds(spawnTime);

        FindFirstObjectByType<BossAlertUI>()?.ShowAlert("BOSS APPROACHING");
        Debug.Log("보스 등장 알림 호출됨");


        Vector2 spawnPos = GetRandomPositionNearPlayer();
        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        // MoveToTarget 연결
        var move = boss.GetComponent<MoveToTarget>();
        if (move != null)
            move.Target = playerTransform;

       

        // 보스 UI
        var enemy = boss.GetComponent<Enemy>();
        if (enemy != null && bossHPBarUI != null)
        {
            enemy.IsBoss = true;
            bossHPBarUI.SetActive(true);

            var gauge = bossHPBarUI.GetComponent<Guage>();
            if (gauge != null)
            {
                typeof(Enemy).GetField("bossHPBar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                             ?.SetValue(enemy, gauge);
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
        while (Vector2.Distance(randomPos, basePos) < safeDistance && attempt < 10);

        return randomPos;
    }
}
