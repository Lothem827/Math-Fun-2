using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HScreenEXP : MonoBehaviour
{
    public int maxEXP, currEXP, baseEXP;
    public Slider EXPBar;
    public TextMeshProUGUI level, coins;

    public jsonConverter updating;
    PlayerInfo playerInfo = null;

    private void getEXP(PlayerInfo i)
    {
        maxEXP = i.baseExp;
        currEXP = i.currExp;
        level.text = i.playerLevel.ToString();
        coins.text = i.coins.ToString();
    }
    public void setEXPValues()
    {
        EXPBar.maxValue = maxEXP;
        EXPBar.value = currEXP;
    }
    public void setCurrentEXP(int round)
    {
        EXPBar.value = round;
    }
    private void getLevelDetails(PlayerInfo pinfo)
    {
        getEXP(PlayerInfoScript.getPlayerInfo());
    }
    void updatePlayerEXP()
    {
        baseEXP = playerInfo.baseExp;
        int _exp = playerInfo.currExp;
        //changes
        if (_exp >= baseEXP)
        {
            baseEXP = playerInfo.baseExp;
            if (_exp > baseEXP)
            {
                int temp = _exp - playerInfo.baseExp;
                playerInfo.currExp = temp;
                playerInfo.playerLevel += 1;
                playerInfo.baseExp += playerInfo.baseExp;
            }
            else if (_exp == baseEXP)
            {
                playerInfo.currExp = 0;
                playerInfo.playerLevel += 1;
                playerInfo.baseExp += playerInfo.baseExp;
            }
            else
            {
                playerInfo.currExp = _exp;
            }
        }
    }
    public void Start()
    {
        updating.loadPlayerInfo(PlayerInfoScript.getPlayerInfo());
        updating.loadLevels(LevelsArrayDB.getCurrentLevel());
        playerInfo = Resources.Load<PlayerInfo>("_SO/Player Info/playerInfo");
        updatePlayerEXP();
        getLevelDetails(PlayerInfoScript.getPlayerInfo());
    }
    public void Update()
    {
        setEXPValues();
        setCurrentEXP(currEXP);
    }
}