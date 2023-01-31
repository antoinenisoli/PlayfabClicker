using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class User
{
    public string name;
    public string mailAdress;
    public string playFabId;

    public User(string name, string mailAdress, string playFabId)
    {
        this.name = name;
        this.mailAdress = mailAdress;
        this.playFabId = playFabId;
    }
}

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager Instance;
    public User currentUser = null;
    int latestHighScore;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void LoginUser(string mailAdress, string playFabId)
    {
        currentUser = new User("", mailAdress, playFabId);
    }

    public void OnError(PlayFabError error)
    {
        print(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>()
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScores",
                    Value = score,
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult obj)
    {
        print("success leaderboard sent");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScores",
            StartPosition = 0,
            MaxResultsCount = 10,
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    public void GetUserLeaderboard()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            PlayFabId = currentUser.playFabId,
            StatisticName = "HighScores",
            MaxResultsCount = 1,
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnPlayerLeaderboardGet, OnError);
    }

    private void OnPlayerLeaderboardGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            if (item.PlayFabId == currentUser.playFabId)
            {
                latestHighScore = item.StatValue;
                print("latest user high score is : " + latestHighScore);
                return;
            }
        }
    }

    public int GetLatestHighScore() => latestHighScore;

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        /*foreach (var item in result.Leaderboard)
            print(item.Position + " " + item.PlayFabId + " " + item.StatValue);*/
    }
}
