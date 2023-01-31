using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject endScreen;
    [SerializeField] CanvasGroup endScreenGroup;
    [SerializeField] Text clickCountText, timerText;
    [SerializeField] Button clickButton;
    [SerializeField] GameObject highScoreText;
    float clickValue;

    private void Start()
    {
        EventManager.Instance.OnNewClick.AddListener(UpdateClickText);
        EventManager.Instance.OnUpdateTimer.AddListener(UpdateTimer);

        EventManager.Instance.OnGameStopped.AddListener(GameStopped);
        EventManager.Instance.OnNewGame.AddListener(NewGame);
        EventManager.Instance.OnNewHighScore.AddListener(()=>
        {
            highScoreText.SetActive(true);
        });

        SetGameState(false);
        ResetEndScreen();
    }

    void ResetEndScreen()
    {
        highScoreText.SetActive(false);
        endScreenGroup.alpha = 0;
        endScreen.SetActive(false);
        endScreenGroup.blocksRaycasts = false;
    }

    void NewGame()
    {
        clickButton.gameObject.SetActive(true);
        ResetEndScreen();
    }

    void GameStopped()
    {
        clickButton.gameObject.SetActive(false);
        endScreenGroup.DOComplete();
        endScreen.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(1.5f);
        sequence.Append(endScreenGroup.DOFade(1, 0.2f));
        sequence.OnComplete(() =>
        {
            endScreenGroup.blocksRaycasts = true;
            if (clickValue > PlayfabManager.Instance.GetLatestHighScore())
            {
                PlayfabManager.Instance.SendLeaderboard((int)clickValue);
                EventManager.Instance.OnNewHighScore.Invoke();
                print("new high score !! ");
            }
        });
    }

    void SetGameState(bool state)
    {
        clickButton.gameObject.SetActive(state);
        clickCountText.text = "0";
    }

    void UpdateTimer(float value)
    {
        timerText.text = "Remaining time : " + string.Format("{0:0.##}", value); 
    }

    void UpdateClickText(float value)
    {
        clickValue = value;
        clickCountText.text = value + "";
    }
}
