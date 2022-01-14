using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private GameObject m_prefabProjectile;
    [SerializeField] private float m_velocityMult = 8f;

    [Header ("Set Dinamically")]
    [SerializeField] private GameObject m_launchPoint;
    [SerializeField] private Vector3 m_launchPos;
    [SerializeField] private GameObject m_projectile;
    public GameObject newProjectile => m_projectile;
    /// <summary>
    /// ����� ������������
    /// </summary>
    [SerializeField] private bool m_aimingMode;
    private Rigidbody m_projectileRigidbody;
    /// <summary>
    /// ��� ����������
    /// </summary>
    public delegate void DelegateEventCreatePrefab();
    public event DelegateEventCreatePrefab OnCreate;
    [SerializeField] private float timer = 5f;
    private float startTimer;
    [SerializeField] private GameObject m_PrefabT;
  
    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        m_launchPoint = launchPointTrans.gameObject;
        m_launchPoint.SetActive(false);

        m_launchPos = launchPointTrans.position;
        startTimer = timer;
        timer = 1;
    }
    private void OnMouseEnter()
    {
        m_launchPoint.SetActive(true);
    }
    private void OnMouseExit()
    {
        m_launchPoint.SetActive(false);
    }
    private void OnMouseDown()
    {
        m_aimingMode = true;
        m_projectile = Instantiate(m_prefabProjectile);
        OnCreate();
        m_projectile.transform.position = m_launchPos;
        m_projectileRigidbody = m_projectile.GetComponent<Rigidbody>();
        m_projectileRigidbody.isKinematic = true;
    }
    private void Update()
    {
        if(!m_aimingMode)//���� ������� �� � ������ ������������
        {
            return;
        }
        
        Vector3 mousePos2D = Input.mousePosition;// ������� ����
        mousePos2D.z = -Camera.main.transform.position.z;//������� ���������� �� ������
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);//������������ � ���������� ����������

        Vector3 mousePosDelta = mousePos3D - m_launchPos;

        //���������� mousePosDelta �������� ����������
        float maxMagnitude = GetComponent<SphereCollider>().radius;
        if(mousePosDelta.magnitude > maxMagnitude)
        {
            mousePosDelta.Normalize();//����� ������� ���������� ������ 1
            mousePosDelta *= maxMagnitude;//����� ������� ���������� ������ maxMagnitude
        }

        //����������� ������ � ����� �������
        Vector3 projPos = mousePosDelta + m_launchPos;
        projPos.z = 0;
        m_projectile.transform.position = projPos;

        //������ ��� �������� ����������
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            var t = Instantiate(m_PrefabT, m_prefabProjectile.transform.position, Quaternion.identity);
            Vector3 mousePosDeltaT = mousePos3D - m_launchPos;

            //���������� mousePosDelta �������� ����������
            float maxMagnitudeT = GetComponent<SphereCollider>().radius;
            if (mousePosDeltaT.magnitude > maxMagnitudeT)
            {
                mousePosDeltaT.Normalize();//����� ������� ���������� ������ 1
                mousePosDeltaT *= maxMagnitudeT;//����� ������� ���������� ������ maxMagnitude
            }

            //����������� ������ � ����� �������
            Vector3 projPosT = mousePosDeltaT + m_launchPos;
            projPosT.z = 0;
            t.transform.position = projPosT;
            t.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            t.GetComponent<Rigidbody>().velocity = -mousePosDelta * m_velocityMult;
            timer = startTimer;
        }

        if (Input.GetMouseButtonUp(0))//������ ��������
        {
            m_aimingMode = false;
            m_projectileRigidbody.isKinematic = false;
            m_projectileRigidbody.velocity = -mousePosDelta * m_velocityMult;
            FollowCamera.POI = m_projectile;
            m_projectile = null;
            OnCreate();
        }
    }
    private void OnMouseUp()
    {
        UIManager.Instance.UpdateLives();
        LevelController.Instance.ResetGame();
    }
}
