using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelUnlocker : MonoBehaviour
{
    PlayerInfo playerInfo = null;
    int pLevel = 0;
    public Button[] diffs;
    public TextMeshProUGUI[] reqLevel;
    int[] levelRequirements = new int[] {0, 3, 6, 12, 24, 48, 60,80};

    void Start()
    {
        playerInfo = Resources.Load<PlayerInfo>("_SO/Player Info/playerInfo");
        pLevel = playerInfo.currLevel;
        activateDiff();
    }

    void Update()
    {
        
    }


// -Extra Functions-----------------------
    void activateDiff()
    {
        if(pLevel < 3)
        {
            for(int i = 1;i < diffs.Length; i++)
            {
                diffs[i].interactable = false;
                reqLevel[i].text = levelRequirements[i].ToString();
            }
        }
        else if (pLevel < 6)
        {
            for (int i = 2; i < diffs.Length; i++)
            {
                diffs[i].interactable = false;
                reqLevel[i].text = levelRequirements[i].ToString();
            }
        }
        else if (pLevel < 12)
        {
            for (int i = 3; i < diffs.Length; i++)
            {
                diffs[i].interactable = false;
                reqLevel[i].text = levelRequirements[i].ToString();
            }
        }
        else if (pLevel < 24)
        {
            for (int i = 4; i < diffs.Length; i++)
            {
                diffs[i].interactable = false;
                reqLevel[i].text = levelRequirements[i].ToString();
            }
        }
        else if (pLevel < 48)
        {
            for (int i = 5; i < diffs.Length; i++)
            {
                diffs[i].interactable = false;
                reqLevel[i].text = levelRequirements[i].ToString();
            }
        }
        else if (pLevel < 60)
        {
            for (int i = 5; i < diffs.Length; i++)
            {
                diffs[i].interactable = false;
                reqLevel[i].text = levelRequirements[i].ToString();
            }
        }
    }
}
