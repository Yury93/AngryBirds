using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    [SerializeField] private Text scoreTxt;
    [SerializeField]private int score = 0;
    public int Score => score;

    [SerializeField] private Text livesTxt;
    [SerializeField] private int goat = 3;
    public int Goat => goat;

    private void Start()
    {
        goat = 3;
        scoreTxt.text = "Score: " + score;
        livesTxt.text = "Goat: " + goat;
    }
    public void UpdateScore()
    {
        score += 1;
        scoreTxt.text = "Score: " + score;
    }
    public void UpdateLives()
    {
        goat -= 1;
        livesTxt.text = "Goat: " + goat;
    }
}
