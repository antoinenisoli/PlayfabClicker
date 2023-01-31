using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] RectTransform panel;
    [SerializeField] CanvasGroup group;
    [SerializeField] TMP_InputField emailInput, passwordInput;
    [SerializeField] TMP_Text messageText;

    public void Register() //called by button
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucess, OnError);
    }

    void OnError(PlayFabError error)
    {
        string errorMessage = error.GenerateErrorReport();
        string[] split = errorMessage.Split("\n");
        messageText.color = Color.red;
        print(errorMessage);
        if (split.Length > 1)
            messageText.text = split[1];
        else
            messageText.text = error.ErrorMessage;
    }

    void OnRegisterSucess(RegisterPlayFabUserResult result)
    {
        print("new account : " + result.PlayFabId);
        messageText.color = Color.green;
        messageText.text = "Accound registered.";
    }

    public void LogIn() //called by button
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        print("user loggin : " + result.PlayFabId);
        messageText.color = Color.green;
        messageText.text = "Logged in. Welcome !";

        Sequence sequence = DOTween.Sequence();
        sequence.Append(panel.DOPunchScale(Vector3.one * 0.5f, 0.3f));
        sequence.AppendInterval(2f);
        sequence.Append(group.DOFade(0, 1f));
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });

        foreach (var item in GetComponentsInChildren<Selectable>())
            item.interactable = false;
    }
}
