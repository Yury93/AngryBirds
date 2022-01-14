using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : SingletonBase<LevelController>
{
    [SerializeField] private List <Enemy> enemies;
    public int EnemyIndex;
    [SerializeField] private int indexScene;
    [SerializeField]private int startCountEnemy;
    private void Start()
    {
        startCountEnemy = enemies.Count;
    }
    public void EnemySetIndex(int index)
    {
        EnemyIndex = index;
    }
    public void ClearListEnemy()
    {
        if (enemies.Contains(enemies[EnemyIndex]))
        {
            enemies.RemoveAt(EnemyIndex);
        }
        LoadNextScene();
    }
    public void LoadNextScene()
    {
        StartCoroutine(CorTimerLoadScene());
        IEnumerator CorTimerLoadScene()
        {
            yield return new WaitForSeconds(10f);
            if (UIManager.Instance.Score == startCountEnemy)
            {
                SceneManager.LoadScene(indexScene);
            }
        }
    }
    public void ResetGame()
    {
       if( UIManager.Instance.Goat <= -1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
