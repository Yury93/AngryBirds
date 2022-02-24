using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private float m_velocityMult = 8f;
    [SerializeField] private WeaponAttacher weaponAttacher;

    [Header ("Set Dinamically")]
    [SerializeField] private GameObject m_launchPoint;
    [SerializeField] private Vector3 m_launchPos;
    [SerializeField] private GameObject m_projectile;
    public GameObject newProjectile => m_projectile;

    [SerializeField] private AnimationController characterController;
    
    [Header("Trajectory")]
    /// <summary>
    /// Режим прицеливания
    /// </summary>
    [SerializeField] private bool m_aimingMode;
    [SerializeField] private Transform transformCreateObj;
    private Rigidbody m_projectileRigidbody;
    [SerializeField] private float timer = 5f;
    private float startTimer;
    [SerializeField] private GameObject m_PrefabT;
    [SerializeField] private AudioSource audioMan;
    [SerializeField] private AudioSource axeSound;
    private void Awake()
    {
        characterController = GetComponent<AnimationController>();
        weaponAttacher.ViewAxe(true);

        Transform launchPointTrans = transform.Find("LaunchPoint");
        m_launchPoint = launchPointTrans.gameObject;
        m_launchPoint.SetActive(false);

        m_launchPos = launchPointTrans.position;
        startTimer = timer;
        timer = 0.2F;
    }
    private void OnMouseEnter()
    {
        m_launchPoint.SetActive(true);
        Time.timeScale = 1;
    }
    private void OnMouseExit()
    {
        m_launchPoint.SetActive(false);
    }
    private void OnMouseDown()
    {
        m_aimingMode = true;
        
    }
    private void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;// Позиция мыши
        mousePos2D.z = -Camera.main.transform.position.z;//Вычитае расстояние от камеры
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);//Конвертируем в глобальные координаты

        Vector3 mousePosDelta = mousePos3D - m_launchPos;

        //Ограничить mousePosDelta размером коллайдера
        float maxMagnitude = GetComponent<SphereCollider>().radius;
        if (mousePosDelta.magnitude > maxMagnitude)
        {
            mousePosDelta.Normalize();//Длина вектора становится равной 1
            mousePosDelta *= maxMagnitude;//Длина вектора становится равной maxMagnitude
        }


        if (Input.GetMouseButtonUp(0) && m_projectile)//Кнопка отпущена
        {
            AudioManager.Instance.PlayAudio(audioMan);

            characterController.CharThrowAnim();

            StartCoroutine(CorThrow());

            m_projectileRigidbody = m_projectile.GetComponent<Rigidbody>();


            IEnumerator CorThrow()
            {
                yield return new WaitForSeconds(1);
                weaponAttacher.ViewAxe(false);
                weaponAttacher.ToFree();

                m_aimingMode = false;
               

                var p = m_projectile.gameObject.GetComponent<Projectile>();
                if(p)
                {
                    p.ReturnAxe();
                }
                //Debug.Log("Возвращение топора");

                m_projectileRigidbody.isKinematic = false;
                m_projectileRigidbody.velocity = -mousePosDelta * m_velocityMult;
                AudioManager.Instance.PlayAudio(axeSound);
                m_projectile = null;

                characterController.CharIdleAnim();
            }
        }
        Trajectory();
    }
    public void Trajectory()
    {
        if (!m_aimingMode)//Если рогатка не в режиме прицеливания
        {
            return;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos2D = Input.mousePosition;// Позиция мыши
                mousePos2D.z = -Camera.main.transform.position.z;//Вычитае расстояние от камеры
                Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);//Конвертируем в глобальные координаты

                Vector3 mousePosDelta = mousePos3D - m_launchPos;
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    var t = Instantiate(m_PrefabT, transformCreateObj.position, Quaternion.identity);

                    t.GetComponent<Rigidbody>().isKinematic = false;
                    t.GetComponent<Rigidbody>().velocity = -mousePosDelta * m_velocityMult;
                    timer = startTimer;
                    Destroy(t, 2);
                }
            }
        }
    }
        public void SetNewProjectile(GameObject project)
    {
        m_projectile = project;
    }
    
}
