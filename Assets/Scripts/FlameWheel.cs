using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameWheel : MonoBehaviour
{
    [SerializeField] GameObject m_rope = null;
    [SerializeField] Transform[] m_positions;

    private void Start()
    {
        foreach (Transform p in m_positions)
        {
            GameObject rope = Instantiate(m_rope, p);

        }
    }

    private void Update()
    {
        Vector3 rot = Vector3.forward * 100.0f;
        rot *= Time.deltaTime;
        transform.rotation *= Quaternion.Euler(rot);
        //foreach (Transform p in m_positions)
        //{
        //    p.position += new Vector3(Mathf.Cos(Time.time), Mathf.Sin(Time.time)) * Time.deltaTime;
        //}
    }
}
