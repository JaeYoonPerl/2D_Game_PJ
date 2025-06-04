using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    MoveToTarget m_perfabEnemy;

    [SerializeField]
    float m_spawnTime = 1f;

    [SerializeField]
    Transform m_target;

    IEnumerator Start()
    {
        while (true)
        {
            var obj = Instantiate(m_perfabEnemy);
            var x = Random.Range(-8, 8);
            var y = Random.Range(-8, 8);

            obj.transform.position = new Vector3(x, y);
            obj.Target = m_target;

            yield return new WaitForSeconds(m_spawnTime);
        }

    }
}
