using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    /// <summary>
    /// Ссылка на интересующий объект point of interest
    /// </summary>
    public static GameObject POI;

    [Range(0,1)]
    [SerializeField] private float m_eysing;

    [SerializeField] private float timeResetPos;
    private float timer;
    [Range(0,100)]
    [SerializeField] private float fieldCam;
    [Header("Set Dinamically")]
    [SerializeField] private float m_camZ;
    [SerializeField] private float startFieldCam;
    [SerializeField] private Vector3 camPosition;


    void Awake()
    {
        m_camZ = transform.position.z;
        startFieldCam = Camera.main.fieldOfView;
        timer = timeResetPos;
        camPosition = Camera.main.transform.position;
    }
   
    // Update is called once per frame
    private void FixedUpdate()
    {
        if(POI)
        {
            ObjView(POI);
            Camera.main.fieldOfView = Mathf.Lerp(startFieldCam, fieldCam, 0.01f);
            timeResetPos -= Time.deltaTime;
            if(timeResetPos < 0)
            {
                Destroy(POI.gameObject, 5f);
            }
        }
        else
        {
            Camera.main.transform.position = camPosition;
            Camera.main.fieldOfView = Mathf.Lerp(fieldCam, startFieldCam,  0.01f);
            timeResetPos = timer;
        }
    }
    private void ObjView(GameObject obj)
    {
            Vector3 destination = obj.transform.position;
            destination = Vector3.Lerp(transform.position, destination, m_eysing);
            destination.z = m_camZ;//задаём направление по Z
            transform.position = destination;//ПРИСВАЕВАЕМ КАМЕРЕ НОВУЮ ПОЗИЦИЮ
    }
}
