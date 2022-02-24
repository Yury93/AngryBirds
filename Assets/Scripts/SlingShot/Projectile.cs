using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private AnimationController animationController;
    [SerializeField] private Transform transformArm;
    [SerializeField] private float speed;
    [SerializeField] private float timeReturn;
    private void Update()
    {
        if(GetComponent<Rigidbody>().isKinematic == true 
            && animationController.GetComponent<SlingShot>().newProjectile == null)
        {
            transform.Translate((transformArm.position - transform.position) * speed * Time.deltaTime, Space.World);
        }
        
    }
   public void ReturnAxe()
    {
        StartCoroutine(CorReturnAxe());
        IEnumerator CorReturnAxe()
        {
            yield return new WaitForSeconds(timeReturn);
            CountPlayers.Instance.ThrowCalculate();
            Time.timeScale = 0.1f;
            GetComponent<Rigidbody>().isKinematic = true;
            animationController.CharReturnAxeAnim();
            animationController.gameObject.GetComponentInChildren <WeaponAttacher>().ViewAxe(true);
            
        }
    }
}
