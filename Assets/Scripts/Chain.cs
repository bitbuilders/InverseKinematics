using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] GameObject m_prisoner = null;
    [SerializeField] IK m_chain = null;

    public float Length
    {
        get
        {
            float l = 0.0f;

            foreach (Segment s in m_chain.m_segments)
            {
                l += s.m_length;
            }

            return l;
        }
    }

    public Vector2 Position { get { return m_chain.m_segments[m_chain.m_segments.Length - 1].start; } }

    private void Start()
    {
        m_chain.m_target = m_prisoner;
    }
}
