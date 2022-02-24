using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rump : MonoBehaviour
{
    [SerializeField] private SlingShot m_slingShot;
    [SerializeField] private GameObject m_projectile;
    private Vector3 startPosLine;

    void Start()
    {
        //m_slingShot.OnCreate += M_slingShot_OnCreate;
    }

    private void M_slingShot_OnCreate()
    {
        m_projectile = m_slingShot.newProjectile;
    }
}
