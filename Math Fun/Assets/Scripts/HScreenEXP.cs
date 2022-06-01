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
    public TextMeshProUGUI _truelseCaption;
    public TextMeshProUGUI[] _lvlsButtons;
    public GameObject popUp;
    private int animCount = 0;
    private int[] compLevels, l_stars;
    public Button truelse;

    public jsonConverter updating;
    PlayerInfo playerInfo = null;
    
    void getLevelsCompleted()
    {

    }

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
        {
            ops.text = "Subtraction";
            truelse.interactable = false;
            _truelseCaption.text = "Disabled in Subtraction";
        }
        else if (i.currOperation == "mult")
            ops.text = "Multiplication";
        else if (i.currOperation == "div")
        {
            ops.text = "Division";
            truelse.interactable = false;
            _truelseCaption.text = "Disabled in Division";
        }
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
        {
            updating.loadLevels(LevelsArrayDB.getBasicA(), 0);
            //updating.loadComparison(ComparingArrayDB.getBasicA(1), "Basic A");
        }
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
        l_stars = new int[99];
        compLevels = new int[99];
        updating.loadPlayerInfo(PlayerInfoScript.getPlayerInfo());
        loadAllLevels();
        playerInfo = Resources.Load<PlayerInfo>("_SO/Player Info/playerInfo");
        updatePlayerEXP();
        getLevelDetails(PlayerInfoScript.getPlayerInfo());
        popUp.SetActive(false);

        for (int i = 0; i < 50; i++)
        {
            setLevelDetails(i, PlayerInfoScript.getPlayerInfo()); //call method to retrieve level information
            for(int j = 0;j < 7;j++)
                setLevels(l_stars[j], j);
        }
        for (int j = 0; j < 7; j++)
            _lvlsButtons[j].text = compLevels[j] + "/50";
    }
    void setLevelDetails(int level, PlayerInfo pinfo)
    {
        setLevel(LevelsArrayDB.getBasicA(), pinfo.currOperation, level,0); //returns current level's information
        setLevel(LevelsArrayDB.getBasicB(), pinfo.currOperation, level,1); //returns current level's information
        setLevel(LevelsArrayDB.getNormalA(), pinfo.currOperation, level,2); //returns current level's information
        setLevel(LevelsArrayDB.getNormalB(), pinfo.currOperation, level,3); //returns current level's information
        setLevel(LevelsArrayDB.getHard(), pinfo.currOperation, level,4); //returns current level's information
        setLevel(LevelsArrayDB.getAdvanced(), pinfo.currOperation, level,5); //returns current level's information
        setLevel(LevelsArrayDB.getUltra(), pinfo.currOperation, level,6); //returns current level's information
    }
    public void setLevels(int _stars, int index)
    {
        if (_stars == 3)
            compLevels[index]++;
    }
    void setLevel(LevelsArray i, string ops, int level, int index) //stores Level Information
    {
        if (ops == "add")
        {
            l_stars[index] = i.opStars_add[level];
            //isDone = i.notLocked_add[level];
        }
        else if (ops == "sub")
        {
            l_stars[index] = i.opStars_sub[level];
            //isDone = i.notLocked_sub[level];
        }
        else if (ops == "mult")
        {
            l_stars[index] = i.opStars_mult[level];
            //isDone = i.notLocked_mult[level];
        }
        else
        {
            l_stars[index] = i.opStars_div[level];
           // isDone = i.notLocked_div[level];
        }
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