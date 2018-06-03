﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
	[SerializeField] Segment m_segment = null;
	[SerializeField] [Range(1, 40)] int m_count = 5;
	[SerializeField] GameObject m_target = null;
	[SerializeField] GameObject m_base = null;

	Segment[] m_segments = null;

	void Start()
	{
		for (int i = 0; i < m_count; i++)
		{
			Instantiate<Segment>(m_segment, transform);
		}

		m_segments = GetComponentsInChildren<Segment>();
	}

	void Update()
	{
		for (int i = 0; i < m_segments.Length; i++)
        {
            Vector2 target = (i == 0) ? (Vector2)m_target.transform.position : m_segments[i - 1].start;
            m_segments[i].Follow(target);
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
