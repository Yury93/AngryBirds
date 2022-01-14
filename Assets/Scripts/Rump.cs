using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rump : MonoBehaviour
{
    [SerializeField] private SlingShot m_slingShot;
    [SerializeField] private GameObject m_projectile;
    [SerializeField] private LineRenderer m_lineRenderer;
    private Vector3 startPosLine;
    void Start()
    {
        startPosLine = m_lineRenderer.GetPosition(1);
        m_slingShot.OnCreate += M_slingShot_OnCreate;
    }

    private void M_slingShot_OnCreate()
    {
        m_projectile = m_slingShot.newProjectile;
    }

    void Update()
    {
        if(m_projectile)
        {
            m_lineRenderer.SetPosition(1, new Vector3(m_projectile.transform.position.x + 0.6f,
            m_projectile.transform.position.y, m_projectile.transform.position.z));
        }
        else
        {
            m_lineRenderer.SetPosition(1, startPosLine);
        }
    }
}
