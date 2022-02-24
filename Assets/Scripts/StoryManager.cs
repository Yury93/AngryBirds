using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private string nameScene;
    [SerializeField] private CalculateResults calculateResults;

    public void NextLevel()
    {
        if (calculateResults.Result() >= 777)
        {
            Time.timeScale = 0.3f;
            StartCoroutine(CorNextScene());
            IEnumerator CorNextScene()
            {
                yield return new WaitForSeconds(3);
                Time.timeScale = 1;
                yield return new WaitForSeconds(0.3f);
                SceneManager.LoadScene(nameScene);
            }
        }
    }
    public void EventFinal()
    {
        if (calculateResults.Result() == 777)
        {
            Time.timeScale = 0.3f;
            StartCoroutine(CorNextScene());
            IEnumerator CorNextScene()
            {
                yield return new WaitForSeconds(3);
                Time.timeScale = 1;
                yield return new WaitForSeconds(0.3f);
                SceneManager.LoadScene(nameScene);
            }
        }
    }
}
