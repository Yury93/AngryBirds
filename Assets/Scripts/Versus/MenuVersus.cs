using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuVersus : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsScene;
    [SerializeField] private Text countPlayerTxt,distanceTargetTxt;
    [SerializeField] private GameObject target;
    private float distanceTarget = 20;
    [SerializeField] private Animator animatorTarget;
    [SerializeField] private Toggle toggle;

    [SerializeField] private List<Target> targets;
    [SerializeField] private Text PlayerScore1, PlayerScore2, PlayerScore3, PlayerScore4, TextWin;
    [SerializeField] private LevelCondition levelCondition;
    void Start()
    {
        countPlayerTxt.text = CountPlayers.Instance.CountPlayer.ToString();
        distanceTargetTxt.text = distanceTarget.ToString();

        for (int i = 0; i < objectsScene.Count; i++)
        {
            objectsScene[i].SetActive(false);
        }
        animatorTarget.enabled = false;
    }

    public void StartGame()
    {
        for (int i = 0; i < objectsScene.Count; i++)
        {
            objectsScene[i].SetActive(true);
        }
        gameObject.SetActive(false);
        PlayerScore1.text = "Player 1: " + 0 + "/" + levelCondition.Score.ToString();
        PlayerScore2.text = "Player 2: " + 0 + "/" + levelCondition.Score.ToString();
        PlayerScore3.text = "Player 3: " + 0 + "/" + levelCondition.Score.ToString();
        PlayerScore4.text = "Player 4: " + 0 + "/" + levelCondition.Score.ToString();

        //StartCoroutine(CorWin());
        //IEnumerator CorWin()
        //{
        //    yield return new WaitForSeconds(1.5f);
        //    TextWin.text = "Throws player 1";
        //    TextWin.gameObject.SetActive(true);
        //    yield return new WaitForSeconds(1.5f);
        //    TextWin.gameObject.SetActive(false);
        //}
    }
    #region CountPlayers
    public void PlusPlayer()
    {
        if (CountPlayers.Instance.CountPlayer < 4)
        {
            CountPlayers.Instance.SetCountPlayerPlus(1);
            countPlayerTxt.text = CountPlayers.Instance.CountPlayer.ToString();
        }
    }
    public void MinusPlayer()
    {
        if (CountPlayers.Instance.CountPlayer > 1)
        {
            CountPlayers.Instance.SetCountPlayerMinus(1);
            countPlayerTxt.text = CountPlayers.Instance.CountPlayer.ToString();
        }
    }
    #endregion
    #region DistanceTarget
    public void PlusDistance()
    {
        if (distanceTarget < 40)
        {
            distanceTarget += 10;
            distanceTargetTxt.text = distanceTarget.ToString();
            target.gameObject.transform.position = new Vector3(target.transform.position.x - 10,
                target.transform.position.y, target.transform.position.z);
        }
    }
    public void MinusDistance()
    {
        if (distanceTarget >= 20)
        {
            distanceTarget -= 10;
            distanceTargetTxt.text = distanceTarget.ToString();
            target.gameObject.transform.position = new Vector3(target.transform.position.x + 10,
                target.transform.position.y, target.transform.position.z);
        }
    }
    #endregion

    public void PlayAnimationTarget()
    {
        if (toggle.isOn)
        {
            animatorTarget.enabled = true;
            foreach (var targ in targets)
            {
                targ.SetVersus(true);
            }
        }
        if(!toggle.isOn)
        {
            animatorTarget.enabled = false;
            foreach (var targ in targets)
            {
                targ.SetVersus(false);
            }
        }
    }
}
