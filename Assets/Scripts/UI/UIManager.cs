using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text clickCountText, timerText;
    [SerializeField] Button clickButton;

    private void Start()
    {
        EventManager.Instance.OnNewClick.AddListener(UpdateText);
        EventManager.Instance.OnUpdateTimer.AddListener(UpdateTimer);

        EventManager.Instance.OnGameStopped.AddListener(() => { clickButton.interactable = false; });
        EventManager.Instance.OnNewGame.AddListener(() => { clickButton.interactable = true; });
    }

    void SetGameState(bool state)
    {
        clickButton.interactable = false;
        clickCountText.text = "0";
    }

    void UpdateTimer(float value)
    {
        timerText.text = value + "";
    }

    void UpdateText(float value)
    {
        clickCountText.text = value + "";
    }
}
