using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabT : MonoBehaviour
{
    [SerializeField] private float timer = 6f;
    private void Update()
    {
        Destroy(gameObject, timer);
    }
}
