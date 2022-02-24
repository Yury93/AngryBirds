using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttacher : MonoBehaviour
{
    [SerializeField] private GameObject ArmWeapon;
    [SerializeField] private GameObject FreeWeapone;
    [SerializeField] private bool haveAxe;

    public void ToArm()
    {
        if (haveAxe)
        {
            ArmWeapon.SetActive(true);
            FreeWeapone.SetActive(false);
        }
    }
    public void ToFree()
    {
        ArmWeapon.SetActive(false);
        FreeWeapone.SetActive(true);
    }
    public void ViewAxe(bool ourAxe)
    {
        haveAxe = ourAxe;
    }
}
