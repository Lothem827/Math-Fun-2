using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HScreenEXP : MonoBehaviour
{
    //Triggers
    public animationHandler[] animate;
    string[] triggers = {
        "buyLevels", "isFailed"
    };


    public int maxEXP, currEXP, baseEXP;
    public Slider EXPBar;
    public TextMeshProUGUI level, coins, difficulty, ops;
    public GameObject popUp;
    private int animCount = 0;

    public jsonConverter updating;
    PlayerInfo playerInfo = null;

    private void getEXP(PlayerInfo i)
    {
        maxEXP = i.baseExp;
        currEXP = i.currExp;
        level.text = i.playerLevel.ToString();
        coins.text = i.coins.ToString();
        difficulty.text = i.currDifficulty;
        if (i.currOperation == "add")
            ops.text = "Addition";
        else if (i.currOperation == "sub")
            ops.text = "Subtraction";
        else if (i.currOperation == "mult")
            ops.text = "Multiplication";
        else if (i.currOperation == "div")
            ops.text = "Division";
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
        playerInfo = Resources.Load<PlayerInfo>("_SO/Player Info/playerInfo");

        if(playerInfo.currDifficulty == "Basic A")
            updating.loadLevels(LevelsArrayDB.getBasicA(), 0);
        else if (playerInfo.currDifficulty == "Basic B")
            updating.loadLevels(LevelsArrayDB.getBasicB(), 1);
        else if (playerInfo.currDifficulty == "Normal A")
            updating.loadLevels(LevelsArrayDB.getNormalA(), 2);
        else if (playerInfo.currDifficulty == "Normal B")
            updating.loadLevels(LevelsArrayDB.getNormalB(), 3);
        else if (playerInfo.currDifficulty == "Hard")
            updating.loadLevels(LevelsArrayDB.getHard(), 4);
        else if (playerInfo.currDifficulty == "Advanced")
            updating.loadLevels(LevelsArrayDB.getAdvanced(), 5);
        else
            updating.loadLevels(LevelsArrayDB.getUltra(), 6);

    }
    public void Start()
    {
        updating.loadPlayerInfo(PlayerInfoScript.getPlayerInfo());
        loadAllLevels();
        playerInfo = Resources.Load<PlayerInfo>("_SO/Player Info/playerInfo");
        updatePlayerEXP();
        getLevelDetails(PlayerInfoScript.getPlayerInfo());
        popUp.SetActive(false);
    }
    public void Update()
    {
        setEXPValues();
        setCurrentEXP(currEXP);
    }

    void runAnim(int trigger, int animHandler)
    {
        animate[animHandler].runTrigger(triggers[trigger]);
    }
    public void unlockLevel()
    {
        if(animCount == 0)
        {
            popUp.SetActive(true);
            runAnim(0, 0);
            animCount++;
        }
    }
    public void closePopup()
    {
        popUp.SetActive(false);
        animCount--;
    }
}