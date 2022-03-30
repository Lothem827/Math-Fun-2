using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HScreenEXP : MonoBehaviour
{
    public int maxEXP, currEXP, baseEXP;
    public Slider EXPBar;
    public TextMeshProUGUI level, coins, difficulty;

    public jsonConverter updating;
    PlayerInfo playerInfo = null;

    private void getEXP(PlayerInfo i)
    {
        maxEXP = i.baseExp;
        currEXP = i.currExp;
        level.text = i.playerLevel.ToString();
        coins.text = i.coins.ToString();
        difficulty.text = i.currDifficulty;
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

    void loadAllLevels()
    {
        updating.loadLevels(LevelsArrayDB.getBasicA());
        updating.loadLevels(LevelsArrayDB.getBasicB());
        updating.loadLevels(LevelsArrayDB.getNormalA());
        updating.loadLevels(LevelsArrayDB.getNormalB());
        updating.loadLevels(LevelsArrayDB.getHard());
        updating.loadLevels(LevelsArrayDB.getAdvanced());
        updating.loadLevels(LevelsArrayDB.getUltra());
    }
    public void Start()
    {
        updating.loadPlayerInfo(PlayerInfoScript.getPlayerInfo());
        loadAllLevels();
        playerInfo = Resources.Load<PlayerInfo>("_SO/Player Info/playerInfo");
        updatePlayerEXP();
        getLevelDetails(PlayerInfoScript.getPlayerInfo());
    }
    public void Update()
    {
        setEXPValues();
        setCurrentEXP(currEXP);
    }
    //void loadEachLevels()
    //{
    //    if (difficulty == "Basic A")
    //        setLevel(LevelsArrayDB.getBasicA(), pinfo.currOperation, level); //returns current level's information
    //    else if (difficulty == "Basic B")
    //        setLevel(LevelsArrayDB.getBasicB(), pinfo.currOperation, level); //returns current level's information
    //    else if (difficulty == "Normal A")
    //        setLevel(LevelsArrayDB.getNormalA(), pinfo.currOperation, level); //returns current level's information
    //    else if (difficulty == "Normal B")
    //        setLevel(LevelsArrayDB.getNormalB(), pinfo.currOperation, level); //returns current level's information
    //    else if (difficulty == "Hard")
    //        setLevel(LevelsArrayDB.getHard(), pinfo.currOperation, level); //returns current level's information
    //    else if (difficulty == "Advanced")
    //        setLevel(LevelsArrayDB.getAdvanced(), pinfo.currOperation, level); //returns current level's information
    //    else if (difficulty == "Ultra")
    //        setLevel(LevelsArrayDB.getUltra(), pinfo.currOperation, level); //returns current level's information
    //}
}