using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class UsernamePanel : MonoBehaviour
{
    [SerializeField] CanvasGroup group;
    [SerializeField] InputField usernameField;

    public void SubmitUsername() //called by button
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = usernameField.text,
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, PlayfabManager.Instance.OnError);
    }

    void Close(float fadeDelay)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(fadeDelay);
        sequence.Append(group.DOFade(0, 1f));
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });

        foreach (var item in GetComponentsInChildren<Selectable>())
            item.interactable = false;
    }

    private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        print(result.DisplayName + " has updated his username successfully");
        Close(1f);
    }
}
