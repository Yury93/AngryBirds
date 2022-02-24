using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountPlayers : SingletonBase<CountPlayers>
{
    //[SerializeField] private SlingShot slingShot;
    [SerializeField] private CalculateResults calculateResults;
    [SerializeField] private Text PlayerScore1, PlayerScore2, PlayerScore3, PlayerScore4, TextWin;
    [SerializeField] private LevelCondition levelCondition;

    [Range(1,4)]
    [SerializeField] private int countPlayers = 1;
    public int CountPlayer => countPlayers;

    [SerializeField] private int idPlayer;

    private int countThrow;
    [SerializeField] private bool storyMode;
    public bool StoryMode => storyMode;
    [SerializeField] private StoryManager storyManager;
    private bool win = false;


    private void Start()
    {
        idPlayer = 1;
        if (!storyMode)
        {
            //HowMuchPlayers();
            PlayerScore1.text = "Player 1: " + 0+ "/" + levelCondition.Score.ToString();
            PlayerScore2.text = "Player 2: " + 0 + "/" + levelCondition.Score.ToString();
            PlayerScore3.text = "Player 3: " + 0 + "/" + levelCondition.Score.ToString();
            PlayerScore4.text = "Player 4: " + 0 + "/" + levelCondition.Score.ToString();
            countThrow = 0;
            TextWin.gameObject.SetActive(false);
        }
        else
        {
            PlayerScore1.text = "Player 1: " + 0 + "/777";
            countThrow = 0;
        }
        
    }

    public void HowMuchPlayers()
    {
        if (countPlayers >= 4)
        {
            countPlayers = 4;
        }
        if (countPlayers <= 0)
        {
            countPlayers = 1;
        }

        if (countPlayers == 1)
        {
            PlayerScore1.gameObject.SetActive(true);
            PlayerScore2.gameObject.SetActive(false);
            PlayerScore3.gameObject.SetActive(false);
            PlayerScore4.gameObject.SetActive(false);
        }
        if (countPlayers == 2)
        {
            PlayerScore1.gameObject.SetActive(true);
            PlayerScore2.gameObject.SetActive(true);
            PlayerScore3.gameObject.SetActive(false);
            PlayerScore4.gameObject.SetActive(false);
        }
        if (countPlayers == 3)
        {
            PlayerScore1.gameObject.SetActive(true);
            PlayerScore2.gameObject.SetActive(true);
            PlayerScore3.gameObject.SetActive(true);
            PlayerScore4.gameObject.SetActive(false);
        }
        if (countPlayers == 4)
        {
            PlayerScore1.gameObject.SetActive(true);
            PlayerScore2.gameObject.SetActive(true);
            PlayerScore3.gameObject.SetActive(true);
            PlayerScore4.gameObject.SetActive(true);
        }
    }

    public void ThrowCalculate()
    {
        if (idPlayer == 1)
        {
            if (!storyMode)
            {
                PlayerScore1.text = $"Player 1: {calculateResults.Result()}/{levelCondition.Score}";
            }
            else
            {
                PlayerScore1.text = "Player 1: " + calculateResults.Result();
            }
            if (storyMode)
            {
                PlayerScore1.text = "Player 1: " + calculateResults.Result() + "/777";
                storyManager.NextLevel();
               
            }
            if (!storyMode && levelCondition.Score <= calculateResults.Result())
            {
                win = true;
                StartCoroutine(CorWin());
                IEnumerator CorWin()
                {
                    yield return new WaitForSeconds(0.5f);
                    TextWin.text = "Player 1 won!";
                    TextWin.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1.5f);
                    TextWin.gameObject.SetActive(false);
                    yield return new WaitForSeconds(1f);
                    SceneManager.LoadScene("Menu");

                }
            }
        }
        if (idPlayer == 2 && !storyMode)
        {
            PlayerScore2.text = $"Player 2: {calculateResults.Result()}/{levelCondition.Score}";
            if (levelCondition.Score <= calculateResults.Result())
            {
                win = true;
                StartCoroutine(CorWin());
                IEnumerator CorWin()
                {
                    yield return new WaitForSeconds(1.5f);
                    TextWin.text = "Player 2 won!";
                    TextWin.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1.5f);
                    TextWin.gameObject.SetActive(false);
                    yield return new WaitForSeconds(1f);
                    SceneManager.LoadScene("Menu");

                }
            }

        }
        if (idPlayer == 3 && !storyMode)
        {

            PlayerScore3.text = $"Player 3: {calculateResults.Result()}/{levelCondition.Score}";
            if (levelCondition.Score <= calculateResults.Result())
            {
                win = true;
                StartCoroutine(CorWin());
                IEnumerator CorWin()
                {
                    yield return new WaitForSeconds(1.5f);
                    TextWin.text = "Player 3 won!";
                    TextWin.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1.5f);
                    TextWin.gameObject.SetActive(false);
                    yield return new WaitForSeconds(1f);
                    SceneManager.LoadScene("Menu");
                }
            }
        }
        if (idPlayer == 4 && !storyMode)
        {

            PlayerScore4.text = $"Player 4: {calculateResults.Result()}/{levelCondition.Score}";
            if (levelCondition.Score <= calculateResults.Result())
            {
                win = true;
                StartCoroutine(CorWin());
                IEnumerator CorWin()
                {
                    yield return new WaitForSeconds(1.5f);
                    TextWin.text = "Player 4 won!";
                    TextWin.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1.5f);
                    TextWin.gameObject.SetActive(false);
                    yield return new WaitForSeconds(1f);
                    SceneManager.LoadScene("Menu");
                }
            }
        }

        countThrow += 1;

        if (countThrow == 3 && !storyMode)
        {
            NextPlayer();
        }
    }

    private void NextPlayer()
    {
        if(idPlayer == countPlayers && win == false)
        {
            StartCoroutine(CorWin());
            IEnumerator CorWin()
            {
                yield return new WaitForSeconds(0.5f);
                TextWin.gameObject.SetActive(true);
                TextWin.color = Color.red;
                TextWin.text = "You could not score the required number of points...";
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("Menu");
            }

            TextWin.gameObject.SetActive(true);
            TextWin.text = "You could not score the required number of points...";
            return;
        }
        if (idPlayer != 5)
        {
            IEnumerator CorWin()
            {
                yield return new WaitForSeconds(2f);
                TextWin.gameObject.SetActive(true);
                TextWin.color = Color.red;
                TextWin.text = $"Next player!!!";
                yield return new WaitForSeconds(1f);
                TextWin.gameObject.SetActive(false);
            }
            idPlayer += 1;
            StartCoroutine(CorWin());
            calculateResults.ResetResult();
        }
        countThrow = 0;
    }
    public void SetCountPlayerPlus( int count)
    {
        countPlayers += count;
        if (countPlayers < 1)
        {
            count = 1;
        }
        if (countPlayers > 4)
        {
            count = 4;
        }
    }
    public void SetCountPlayerMinus(int count)
    {
        countPlayers -= count;
        if (countPlayers < 1)
        {
            count = 1;
        }
        if (countPlayers > 4)
        {
            count = 4;
        }
    }
}
