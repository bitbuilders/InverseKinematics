using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
	[SerializeField] Segment m_segment = null;
	[SerializeField] [Range(1, 40)] int m_count = 5;
	[SerializeField] public GameObject m_target = null;
	[SerializeField] GameObject m_base = null;
    [SerializeField] [Range(-180.0f, 180.0f)] float m_angle = 0;

    public Segment[] m_segments = null;

	void Awake()
	{
		for (int i = 0; i < m_count; i++)
		{
			Segment s = Instantiate<Segment>(m_segment, transform);
		}

		m_segments = GetComponentsInChildren<Segment>();
        //m_segments[0].gameObject.AddComponent<Rigidbody2D>();
	}

	void Update()
	{
		for (int i = 0; i < m_segments.Length; i++)
        {
            Vector2 target = Vector2.zero;
            if (m_target != null) 
                target = (i == 0) ? (Vector2)m_target.transform.position : m_segments[i - 1].start;
            if (m_target == null)
                target = m_segments[i].start;

            float prevAngle = 0.0f;
            if (i < m_segments.Length - 1)
            {
                //Vector3 direction = m_segments[i + 1].transform.position - m_segments[i].transform.position;
                //Vector3 dir = m_segments[i + 1].transform.rotation * (Vector3.right * 5.0f);
                //Debug.DrawLine(m_segments[i + 1].transform.position, m_segments[i + 1].transform.position + dir, Color.red);
                //Debug.DrawLine(m_segments[i].transform.position, m_segments[i].transform.position + direction * 10.0f, Color.blue);
                //prevAngle = Vector3.Angle(dir, direction);
            }
            m_segments[i].Follow(target, m_angle, prevAngle);
        }

        if (m_base)
        {
            m_segments[m_segments.Length - 1].start = m_base.transform.position;
            m_segments[m_segments.Length - 1].CalculateEnd();
            for (int i = m_segments.Length - 2; i >= 0; i--)
            {
                m_segments[i].start = m_segments[i + 1].end;
                m_segments[i].CalculateEnd();
            }
        }
	}
}
