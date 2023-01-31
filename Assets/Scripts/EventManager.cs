using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public class NumberEvent : UnityEvent<float> { }

    public NumberEvent OnNewClick = new NumberEvent();
    public NumberEvent OnUpdateTimer = new NumberEvent();

    public UnityEvent OnNewGame = new UnityEvent();
    public UnityEvent OnGameStopped = new UnityEvent();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
