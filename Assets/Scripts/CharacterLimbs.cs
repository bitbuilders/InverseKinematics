using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLimbs : MonoBehaviour
{
    [SerializeField] Segment m_segment = null;
    [SerializeField] [Range(1, 40)] int m_count = 5;
    [SerializeField] GameObject m_limb = null;
    [SerializeField] Transform[] m_limbLocations = null;
    [SerializeField] Transform[] m_limbStarts = null;
    [SerializeField] Transform[] m_limbControllers = null;

    public float Speed { get; set; }

    List<Segment[]> m_limbSegments;

    void Start()
    {
        m_limbSegments = new List<Segment[]>();

        for (int i = 0; i < m_limbLocations.Length; i++)
        {
            GameObject limb = Instantiate(m_limb, m_limbLocations[i]);
            m_limbSegments.Add(m_limbLocations[i].GetComponentsInChildren<Segment>());
        }
    }

    void Update()
    {
        Walk(Speed);

        for (int j = 0; j < m_limbSegments.Count; j++)
        {
            Segment[] segment = m_limbSegments[j];
            for (int i = 0; i < segment.Length; i++)
            {
                Vector2 target = (i == 0) ? segment[i].target : segment[i - 1].start;
                segment[i].Follow(target);
            }
        }

        for (int j = 0; j < m_limbSegments.Count; j++)
        {
            Segment[] segment = m_limbSegments[j];
            segment[segment.Length - 1].start = m_limbLocations[j].position;
            segment[segment.Length - 1].CalculateEnd();
            for (int i = segment.Length - 2; i >= 0; i--)
            {
                segment[i].start = segment[i + 1].end;
                segment[i].CalculateEnd();
            }
        }
    }

    void Walk(float speed)
    {
        Vector3 gravity = Physics.gravity;
        Vector3 velocity = new Vector3(speed, speed) * 10.0f;
        m_limbControllers[0].position += (gravity + velocity) * Time.deltaTime;
        m_limbControllers[1].position += (gravity + velocity) * Time.deltaTime;
        m_limbControllers[2].position += (gravity + velocity) * Time.deltaTime;
        m_limbControllers[3].position += (gravity + velocity) * Time.deltaTime;
        if (m_limbControllers[0].position.y < 0.0f) m_limbControllers[0].position = new Vector3(m_limbControllers[0].position.x, 0.0f);
        if (m_limbControllers[1].position.y < 0.0f) m_limbControllers[1].position = new Vector3(m_limbControllers[1].position.x, 0.0f);
        if (m_limbControllers[2].position.y < -3.0f) m_limbControllers[2].position = new Vector3(m_limbControllers[2].position.x, -3.0f);
        if (m_limbControllers[3].position.y < -3.0f) m_limbControllers[3].position = new Vector3(m_limbControllers[3].position.x, -3.0f);


        for (int i = 0; i < m_limbControllers.Length; i++)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
            m_limbSegments[i][0].target = position;
        }
    }
}
