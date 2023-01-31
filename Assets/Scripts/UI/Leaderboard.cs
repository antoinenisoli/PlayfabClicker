using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] GameObject entryLinePrefab;
    [SerializeField] Transform content;

    private void OnEnable()
    {
        if (PlayfabManager.Instance)
            PlayfabManager.Instance.GetLeaderboard();

        Open();
    }

    public void Open()
    {
        foreach (Transform item in content)
            Destroy(item.gameObject);

        foreach (var entry in PlayfabManager.Instance.GetLeaderboardEntries())
        {
            GameObject newRow = Instantiate(entryLinePrefab, content);
            LeaderboardEntryRow leaderboardRow = newRow.GetComponent<LeaderboardEntryRow>();
            leaderboardRow.AssignEntry(entry);
        }
    }
}
