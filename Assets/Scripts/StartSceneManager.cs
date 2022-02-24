using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private string story, versus;
    public void NextSceneStory()
    {
        SceneManager.LoadScene(story);
    }
    public void NextSceneVersus()
    {
        SceneManager.LoadScene(versus);
    }
}
