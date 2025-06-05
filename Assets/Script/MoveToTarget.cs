using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D m_rigid;

    [SerializeField]
    public Transform m_target;

    [SerializeField]
    Vector2 m_velocity = Vector2.zero;

    [SerializeField]
    float m_speed = 1f;

    private void Update()
    {
        
        var dir = m_target.position - transform.position;

        m_velocity = dir.normalized * m_speed;

        // 좌우만 바라보게 하기
        if (dir.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(dir.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }


        // 방향바라보며 
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);


    }

    private void FixedUpdate()
    {
        m_rigid.velocity = m_velocity;
    }

    public Transform Target { get => m_target; set => m_target = value; }
}
