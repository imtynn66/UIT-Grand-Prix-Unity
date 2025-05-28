using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLeaderboardItemInfo : MonoBehaviour
{
    public Text positionText;
    public Text driverNameText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPositionText(string newPosition)
    {
        positionText.text = newPosition;
    }

    public void SetDriverNameText(string newDriverName)
    {
        driverNameText.text = newDriverName;
    }
}
