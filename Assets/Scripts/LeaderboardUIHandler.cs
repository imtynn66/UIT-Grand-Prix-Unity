using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIHandler : MonoBehaviour
{
    public GameObject leaderboardItemPrefab;

    SetLeaderboardItemInfo[] setLeaderboardItemInfo;

    void Awake()
    {
        VerticalLayoutGroup leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        //Get all Car lap counters in the scene. 
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        //Allocate the array
        setLeaderboardItemInfo = new SetLeaderboardItemInfo[carLapCounterArray.Length];

        //Create the leaderboard items
        for (int i = 0; i < carLapCounterArray.Length; i++)
        {
            //Set the position
            GameObject leaderboardInfoGameObject = Instantiate(leaderboardItemPrefab, leaderboardLayoutGroup.transform);

            setLeaderboardItemInfo[i] = leaderboardInfoGameObject.GetComponent<SetLeaderboardItemInfo>();

            setLeaderboardItemInfo[i].SetPositionText($"{i + 1}.");
        }

        Canvas.ForceUpdateCanvases();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateList(List<CarLapCounter> lapCounters)
    {
        //Create the leaderboard items
        for (int i = 0; i < lapCounters.Count; i++)
        {
            setLeaderboardItemInfo[i].SetDriverNameText(lapCounters[i].gameObject.name);
        }
    }
}
