using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] GameObject entryLinePrefab;
    [SerializeField] GameObject waitingScreen;
    [SerializeField] Transform content;

    private void OnEnable()
    {
        StartCoroutine(Open());
    }

    public IEnumerator Open()
    {
        foreach (Transform item in content)
            Destroy(item.gameObject);

        waitingScreen.SetActive(true);
        PlayfabManager.Instance.GetLeaderboard();
        yield return new WaitForSeconds(1f); //wait for the leaderboard to be up to date
        waitingScreen.SetActive(false);
        
        foreach (var entry in PlayfabManager.Instance.GetLeaderboardEntries())
        {
            GameObject newRow = Instantiate(entryLinePrefab, content);
            LeaderboardEntryRow leaderboardRow = newRow.GetComponent<LeaderboardEntryRow>();
            leaderboardRow.AssignEntry(entry);
        }
    }
}
