using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float gameDuration = 180;
    float timer;
    int clickCount;
    bool gameIsRunning;

    private void Start()
    {
        EventManager.Instance.OnUpdateTimer.Invoke(gameDuration);
    }

    public void AddClick() //called by button
    {
        clickCount++;
        EventManager.Instance.OnNewClick.Invoke(clickCount);
    }

    public void NewGame() //called by button
    {
        EventManager.Instance.OnNewGame.Invoke();
        EventManager.Instance.OnNewClick.Invoke(0);
        gameIsRunning = true;
        clickCount = 0;
        timer = 0;
    }

    public void StopGame()
    {
        EventManager.Instance.OnGameStopped.Invoke();
        gameIsRunning = false;
        timer = 0;
        PlayfabManager.Instance.SendLeaderboard(clickCount);
        PlayfabManager.Instance.GetLeaderboard();
    }

    private void Update()
    {
        if (gameIsRunning)
        {
            timer += Time.deltaTime;
            EventManager.Instance.OnUpdateTimer.Invoke(gameDuration - timer);
            if (timer > gameDuration)
                StopGame();
        }
    }
}
