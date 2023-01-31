using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntryRow : MonoBehaviour
{
    [SerializeField] Text positionText, nameText, scoreText;

    public void AssignEntry(PlayerLeaderboardEntry entry)
    {
        positionText.text = (entry.Position + 1) + "";
        nameText.text = entry.DisplayName + "";
        scoreText.text = entry.StatValue + "";
    }
}
