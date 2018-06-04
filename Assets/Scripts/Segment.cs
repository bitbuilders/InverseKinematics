using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
	[SerializeField] [Range(0.0f, 10.0f)] public float m_length = 1;

    public Vector2 target { get; set; }
    public Vector2 start { get { return transform.position; } set { transform.position = value; } }
	public Vector2 end { get; set; }
	public float angle { get; set; }

	public void Start()
	{
		Transform childTransform = transform.GetChild(0);
		childTransform.localScale = new Vector3(m_length, 1.0f, 1.0f);
		Vector3 position = childTransform.position;
		position.x = m_length * 0.5f;
		childTransform.position = position;
	}

    public void Follow(Vector2 target)
    {
        Vector2 direction = target - start;
        angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        start = target - (direction.normalized * m_length);
    }

    public void CalculateEnd()
    {
        Vector2 v2 = Vector2.zero;
        v2.x = Mathf.Cos(angle);
        v2.y = Mathf.Sin(angle);

        end = start + v2 * m_length;
    }
}

