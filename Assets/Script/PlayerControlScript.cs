using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D m_rigid;

    [SerializeField]
    Vector2 m_velocity = Vector2.zero;

    [SerializeField]
    float m_speed = 3f;

    [SerializeField]
    Bullet2D m_prefabBullet;

    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        m_velocity = new Vector2(x, y);


        if (Input.GetMouseButtonDown(0))
        {
            var pos = transform.position;
            var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            var dir = targetPos - pos;

            if (dir.x != 0)
            {
                var scale = transform.localScale;
                scale.x = Mathf.Sign(dir.x) * Mathf.Abs(scale.x); // 방향 유지, 크기 변화 없음
                transform.localScale = scale;
            }

            var obj = Instantiate(m_prefabBullet);
            obj.transform.position = pos;
            obj.Fire(dir.normalized * 5f);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<PlayerHealth>()?.TakeDamage(2);
        }


    }

    void FixedUpdate()
    {
        m_rigid.velocity = m_velocity * m_speed;
    }


    private void OnDrawGizmos()
    {
        var pos = transform.position;
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;

        var dir = targetPos - pos;

        Debug.DrawLine(pos, pos + dir, Color.blue);
        Debug.DrawLine(pos, pos + dir.normalized, Color.green);
    }


}
