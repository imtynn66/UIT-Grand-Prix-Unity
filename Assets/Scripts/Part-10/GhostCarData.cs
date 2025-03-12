using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// GhostCarDataListItem class
public class GhostCarData
{
    [Serialized]
    List<GhostCarDataListItem> ghostCarRecorderList = new List<GhostCarDataListItem>();


    public void AddDataItem)(GhostCarDataListItem ghostDataListItem)
    {
        ghostCarRecorderList.Add(ghostDataListItem);
    }
    
    public List<GhostCarDataListItem> GetDataList()
    {
        return ghostCarRecorderList;
    }
}