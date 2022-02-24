using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateResults : MonoBehaviour
{
    [SerializeField] private List<Target> targets;
    [SerializeField] private int countScore;
    [SerializeField] private bool final = false;
    [SerializeField] private StoryManager storyManager;
    private void Start()
    {
        Target.OnTarget += SelectScore;
    }
    public void SelectScore()
    {
        if (final == false)
        {
            foreach (var target in targets)
            {
                if (target.Score != 0)
                {
                    countScore += target.Score;
                    return;
                }
            }
        }
        if(final == true)
        {
            if(countScore == 777)
            {
                if(storyManager)
                storyManager.EventFinal();
            }
            else if (countScore < 777)
            {
                foreach (var target in targets)
                {
                    if (target.Score != 0)
                    {
                        countScore += target.Score;
                        return;
                    }
                }
            }
            else if(countScore > 777)
            {
                foreach (var target in targets)
                {
                    if (target.Score != 0)
                    {
                        countScore -= 100;
                        return;
                    }
                }
            }
        }
    }
    public void ResetResult()
    {
        countScore = 0;
    }
    public int Result()
    {
        return countScore;
    }
}
