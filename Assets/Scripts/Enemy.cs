using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int index;
   
    
    private void OnCollisionEnter(Collision collision)
    {
        UIManager.Instance.UpdateScore();
        LevelController.Instance.EnemySetIndex(index);
        LevelController.Instance.ClearListEnemy();
        Destroy(gameObject);
    }
}
