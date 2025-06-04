using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    MoveToTarget m_perfabEnemy;

    [SerializeField]
    float m_spawnTime = 5f;

    [SerializeField]
    Transform m_target;

    // ���� �ּ� �Ÿ�
    [SerializeField]
    float m_minSpawnDistance = 5f;

    [SerializeField]
    Vector2 m_spawnAreaMin = new Vector2(-8f, -8f);

    [SerializeField]
    Vector2 m_spawnAreaMax = new Vector2(8f, 8f);

    IEnumerator Start()
    {
        while (true)
        {
            Vector3 spawnPos;

            // ������ ������ ������ �ݺ�
            do
            {
                float x = Random.Range(m_spawnAreaMin.x, m_spawnAreaMax.x);
                float y = Random.Range(m_spawnAreaMin.y, m_spawnAreaMax.y);
                spawnPos = new Vector3(x, y);
            }
            while (Vector3.Distance(spawnPos, m_target.position) < m_minSpawnDistance);

            // ����
            var obj = Instantiate(m_perfabEnemy);
            obj.transform.position = spawnPos;
            obj.Target = m_target;

            yield return new WaitForSeconds(m_spawnTime);
        }

    }
}
