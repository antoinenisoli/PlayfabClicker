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
    [SerializeField] GameObject usernameRequestPanel;
    Selectable[] mySelectables;

    private void Start()
    {
        mySelectables = GetComponentsInChildren<Selectable>();
        if (PlayfabManager.Instance.currentUser != null)
            Close(0.2f);
    }

    public void Register() //called by button
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucess, OnLoginError);
    }

    void OnLoginError(PlayFabError error)
    {
        string errorMessage = error.GenerateErrorReport();
        string[] split = errorMessage.Split("\n");
        messageText.color = Color.red;
        print(errorMessage);
        if (split.Length > 1)
            messageText.text = split[1];
        else
            messageText.text = error.ErrorMessage;

        foreach (var item in mySelectables)
            item.interactable = true;
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
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        foreach (var item in mySelectables)
            item.interactable = false;
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginError);
    }

    void Close(float fadeDelay)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(panel.DOPunchScale(Vector3.one * 0.25f, 0.3f));
        sequence.AppendInterval(fadeDelay);
        sequence.Append(group.DOFade(0, 1f));
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void OnLoginSuccess(LoginResult result)
    {
        print("user loggin : " + result.PlayFabId);
        messageText.color = Color.green;
        messageText.text = "Logged in. Welcome !";
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        PlayfabManager.Instance.LoginUser(name, emailInput.text, result.PlayFabId);
        if (name == null)
        {
            usernameRequestPanel.SetActive(true);
            Close(0.5f);
        }
        else
            Close(1.5f);
    }
}
