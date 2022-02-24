using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var axe = collision.gameObject.GetComponent<Projectile>();
        if(axe)
        {
            axe.gameObject.GetComponentInChildren<RotateProjectile>().enabled = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        var axe = collision.gameObject.GetComponent<Projectile>();
        if (axe)
        {
            axe.gameObject.GetComponentInChildren<RotateProjectile>().enabled = true;
        }
    }
}
