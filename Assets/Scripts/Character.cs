using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 20.0f)] float m_speed = 5.0f;
    [SerializeField] [Range(0.0f, 20.0f)] float m_craziness = 0.1f;
    [SerializeField] [Range(0.0f, 20.0f)] float m_waviness = 3.0f;
    [SerializeField] IK[] m_limbs = null;
    [SerializeField] GameObject[] m_controllers = null;
    [SerializeField] GameObject m_projectile = null;

    Vector3 offset1;
    Vector3 offset2;
    Vector3 offset3;
    Vector3 offset4;
    float m_crazyTime;
    int m_limb = 1;

    private void Start()
    {
        for (int i = 0; i < m_limbs.Length; i++)
        {
            m_limbs[i].m_target = m_controllers[i];
        }
        m_crazyTime = 0.0f;
    }

    private void Update()
    {
        Vector2 velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.A)) velocity.x = -1.0f;
        if (Input.GetKey(KeyCode.D)) velocity.x = 1.0f;
        if (Input.GetKey(KeyCode.W)) velocity.y = 1.0f;
        if (Input.GetKey(KeyCode.S)) velocity.y = -1.0f;
        velocity *= m_speed * Time.deltaTime;
        
        transform.position += (Vector3)velocity;

        if (Input.GetKey(KeyCode.Alpha1)) m_limb = 1;
        if (Input.GetKey(KeyCode.Alpha2)) m_limb = 2;
        if (Input.GetKey(KeyCode.Alpha3)) m_limb = 3;
        if (Input.GetKey(KeyCode.Alpha4)) m_limb = 4;
        CalculateOffsets();

        if (Input.GetButtonDown("Jump"))
        {
            GameObject p = Instantiate(m_projectile, transform);
            Vector2 force = Random.insideUnitCircle.normalized * Random.Range(10.0f, 15.0f);
            p.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            p.GetComponent<IK>().m_target = gameObject;
            Destroy(p, 5.0f);
        }
    }

    private void LateUpdate()
    {
        UpdateLimbs();
    }

    private void CalculateOffsets()
    {
        Vector3 velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) velocity.x = -1.0f;
        if (Input.GetKey(KeyCode.RightArrow)) velocity.x = 1.0f;
        if (Input.GetKey(KeyCode.UpArrow)) velocity.y = 1.0f;
        if (Input.GetKey(KeyCode.DownArrow)) velocity.y = -1.0f;
        velocity *= 5.0f * Time.deltaTime;

        if (m_limb == 1) offset1 += velocity;
        if (m_limb == 2) offset2 += velocity;
        if (m_limb == 3) offset3 += velocity;
        if (m_limb == 4) offset4 += velocity;

        offset1 = Vector3.ClampMagnitude(offset1, m_waviness);
        offset2 = Vector3.ClampMagnitude(offset2, m_waviness);
        offset3 = Vector3.ClampMagnitude(offset3, m_waviness);
        offset4 = Vector3.ClampMagnitude(offset4, m_waviness);
    }

    private void UpdateLimbs()
    {
        m_controllers[0].transform.localPosition = new Vector3(-5.0f, 0.0f) *  2.0f + offset1;
        m_controllers[1].transform.localPosition = new Vector3(5.0f, 0.0f)  *  2.0f + offset2;
        m_controllers[2].transform.localPosition = new Vector3(-1.0f, -5.0f) * 2.0f + offset3;
        m_controllers[3].transform.localPosition = new Vector3(1.0f, -5.0f) *  2.0f + offset4;
    }
}
