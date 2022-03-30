using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HScreenEXP : MonoBehaviour
{
    public animationHandler[] animate;
    public int maxEXP, currEXP, baseEXP;
    public Slider EXPBar;
    public TextMeshProUGUI level, coins, difficulty, ops;
    public GameObject popUp;
    string[] triggers = {
        "buyLevelEntry", "isFailed"
    };
    private int animCount = 0;

    public jsonConverter updating;
    PlayerInfo playerInfo = null;

    private void getEXP(PlayerInfo i)
    {
        string operation = i.currOperation;
        maxEXP = i.baseExp;
        currEXP = i.currExp;
        level.text = i.playerLevel.ToString();
        coins.text = i.coins.ToString();
        difficulty.text = i.currDifficulty;

        if(operation == "add")
            ops.text = "Addition";
        else if (operation == "sub")
            ops.text = "Subtraction";
        else if (operation == "div")
            ops.text = "Division";
        else
            ops.text = "Multiplication";

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
<<<<<<< HEAD
    
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
=======
>>>>>>> parent of e9cae31 (Merge branch 'main' of https://github.com/Lothem827/Math-Fun-2)
    public void Start()
    {
        updating.loadPlayerInfo(PlayerInfoScript.getPlayerInfo());

        updating.loadLevels(LevelsArrayDB.getBasicA());
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
    public void runAnimation()
    {
        popUp.SetActive(true);
        if (animCount == 0)
        {
            runAnim(0, 0);
            animCount++;
        }
    }
    public void hidePopUp()
    {
        popUp.SetActive(false);
        animCount--;
    }

}