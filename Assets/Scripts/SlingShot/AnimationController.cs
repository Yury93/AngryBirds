using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animatorCharacter;
    [SerializeField] private SlingShot slingShot;
    [SerializeField] private GameObject projectile;
    public void CharThrowAnim()
    {
        animatorCharacter.SetBool("Throw", true);
    }
    public void CharIdleAnim()
    {
        animatorCharacter.SetBool("Throw", false);
    }
    public void CharReturnAxeAnim()
    {
        animatorCharacter.SetBool("Return", true);
        StartCoroutine(CorReturnAxe());
        IEnumerator CorReturnAxe()
        {
            yield return new WaitForSeconds(1.5f);

            animatorCharacter.SetBool("Return", false);
            slingShot.SetNewProjectile(projectile);
        }
    }
}
