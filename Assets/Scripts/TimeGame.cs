using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeGame : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Text textTime;
    public Action OnEndTime;
    private void Start()
    {
        textTime = GetComponentInChildren<Text>();
        OnEndTime += TimeEnd;
    }

    private void TimeEnd()
    {
        textTime.color = Color.red;
        textTime.text = "Time's up!";
        StartCoroutine(CorReturnMenu());
        IEnumerator CorReturnMenu()
        {
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("Menu");
        }
    }

    private void FixedUpdate()
    {
        if (time > 0)
        {
            time -= Time.fixedDeltaTime;

            textTime.text = "Timer: " + (Convert.ToInt32(time)).ToString();
        }

        if (time <= 0)
        {
            OnEndTime?.Invoke();
            time = 0;
            return;
        }
    }
}
