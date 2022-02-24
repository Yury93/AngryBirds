using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private int score;
    public int Score => score;

    public static Action OnTarget;
    [SerializeField] private Animator animator;
    [SerializeField] private bool versus = false;
    [SerializeField] private AudioSource axeToTarget;
    [SerializeField] private AudioSource endFlyAxe;
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        axeToTarget = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var axe = collision.gameObject.GetComponent<Projectile>();
        if (axe)
        {
            AudioManager.Instance.PlayAudio(axeToTarget);
            AudioManager.Instance.StopAudio(endFlyAxe);
            if (animator)
            {
                animator.enabled = false;
            }
            axe.gameObject.GetComponentInChildren<RotateProjectile>().enabled = false;
            axe.GetComponent<Rigidbody>().useGravity = false;
            axe.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            score = Convert.ToInt32(gameObject.name);
            
            OnTarget?.Invoke();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        var axe = collision.gameObject.GetComponent<Projectile>();
        
        if (axe)
        {
            var rb = axe.gameObject.GetComponent<Rigidbody>();

            StartCoroutine(CorExitCollision());

            IEnumerator CorExitCollision()
            {
                yield return new WaitForSeconds(2f);
                axe.gameObject.GetComponentInChildren<RotateProjectile>().enabled = true;
                
                rb.constraints = RigidbodyConstraints.FreezeRotationX |
                     RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
                
                rb.useGravity = true;
                score = 0;
                yield return new WaitForSeconds(0.5f);
                if (animator && versus)
                {
                    animator.enabled = true;
                }
            }
        }
        enabled = false;
    }
    public void SetVersus(bool vers)
    {
        versus = vers;
    }
}
