using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float gameDuration = 180;
    float timer;
    int clickCount;
    bool gameIsRunning;

    private void Start()
    {
        StopGame();
    }

    public void AddClick()
    {
        clickCount++;
        EventManager.Instance.OnNewClick.Invoke(clickCount);
    }

    public void NewGame()
    {
        EventManager.Instance.OnNewGame.Invoke();
        gameIsRunning = true;
        clickCount = 0;
        timer = 0;
    }

    public void StopGame()
    {
        EventManager.Instance.OnGameStopped.Invoke();
        gameIsRunning = false;
        timer = 0;
        if (clickCount > 0)
        {
            PlayfabManager.Instance.SendLeaderboard(clickCount);
            PlayfabManager.Instance.GetLeaderboard();
        }
    }

    private void Update()
    {
        if (gameIsRunning)
        {
            timer += Time.deltaTime;
            EventManager.Instance.OnUpdateTimer.Invoke(timer);
            if (timer > gameDuration)
                StopGame();
        }
    }
}
