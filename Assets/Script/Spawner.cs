using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] MoveToTarget m_perfabEnemy;

    [Header("Spawn Settings")]
    [SerializeField] Transform m_target;
    [SerializeField] float m_spawnTime = 4f;
    [SerializeField] float m_minSpawnTime = 1f;
    [SerializeField] float m_spawnDecreaseRate = 0.2f;
    [SerializeField] float m_timeBetweenDecrease = 5f;

    [Header("Spawn Area")]
    [SerializeField] Vector2 m_spawnAreaMin = new Vector2(-8f, -8f);
    [SerializeField] Vector2 m_spawnAreaMax = new Vector2(8f, 8f);
    [SerializeField] float m_minSpawnDistance = 5f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(DecreaseSpawnTimeRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Vector3 spawnPos;

            // �÷��̾�� �ʹ� ����� ��ġ�� ���ؼ� ����
            do
            {
                float x = Random.Range(m_spawnAreaMin.x, m_spawnAreaMax.x);
                float y = Random.Range(m_spawnAreaMin.y, m_spawnAreaMax.y);
                spawnPos = new Vector3(x, y);
            }
            while (Vector3.Distance(spawnPos, m_target.position) < m_minSpawnDistance);

            // ���� �� Ÿ�� �Ҵ�
            var obj = Instantiate(m_perfabEnemy, spawnPos, Quaternion.identity);
            obj.Target = m_target;

            yield return new WaitForSeconds(m_spawnTime);
        }
    }

    IEnumerator DecreaseSpawnTimeRoutine()
    {
        while (m_spawnTime > m_minSpawnTime)
        {
            yield return new WaitForSeconds(m_timeBetweenDecrease);
            m_spawnTime -= m_spawnDecreaseRate;
            m_spawnTime = Mathf.Max(m_minSpawnTime, m_spawnTime);
            //Debug.Log($"���� �ֱ� ����: {m_spawnTime:F2}��");
        }
    }
}
