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
    User currentUser = null;
    int latestHighScore;
    List<PlayerLeaderboardEntry> leaderboardEntries;

    public User GetCurrentUser() => currentUser;
    public int GetLatestHighScore() => latestHighScore;
    public List<PlayerLeaderboardEntry> GetLeaderboardEntries() => leaderboardEntries;

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

    public void LoginUser(string name, string mailAdress, string playFabId)
    {
        currentUser = new User(name, mailAdress, playFabId);
        GetLeaderboard();
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
            MaxResultsCount = 15,
        };

        print("CALL LEADERBOARD UPDATE");
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        leaderboardEntries = result.Leaderboard;
        print("leaderboard data received.");

        foreach (var item in result.Leaderboard)
        {
            print(item.Position + " " + item.PlayFabId + " " + item.StatValue);
            if (item.PlayFabId == currentUser.playFabId) //get last high score of the user in order to compare it
            {
                latestHighScore = item.StatValue;
                return;
            }
        }
    }
}
