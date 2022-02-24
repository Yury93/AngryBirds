using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCondition : MonoBehaviour
{
    [SerializeField] private Button button7, button77, button777, button7777;

    private int score = 100;
    public int Score => score;

    public void Button7()
    {
        score = 100;
    }
    public void Button77()
    {
        score = 200;
    }
    public void Button777()
    {
        score = 250;
    }
    public void Button7777()
    {
        score = 300;
    }
}
