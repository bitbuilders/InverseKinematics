using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pug : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 10.0f)] float m_speed = 2.0f;
    [SerializeField] GameObject m_target = null;
    [SerializeField] Chain m_chain = null;

    private void Update()
    {
        Vector2 direction = m_target.transform.position - transform.position;
        Vector2 velocity = direction.normalized;
        velocity *= m_speed * Time.deltaTime;

        if (direction.magnitude < 20.0f)
            transform.position += (Vector3)velocity;
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

        if (((Vector3)m_chain.Position - transform.position).magnitude > m_chain.Length)
        {
            Vector3 dir = transform.position - (Vector3)m_chain.Position;
            transform.position = dir.normalized * m_chain.Length;
        }
    }
}
