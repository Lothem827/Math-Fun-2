using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class mainScriptTrueFalse : MonoBehaviour
{
    //Triggers
    public animationHandler[] animate;
    string[] triggers = {
        "runCompletePopup", "runFailedPopup"
    };

    //-----------------------
    float currentTime = 0f;
    float startTime;
    int menu_anim = 0;

    int stars = 0;
    int correctAns, correctansCounter = 0;
    string equation;
    int min = 0, max = 0;
    int levelRounds, level, currRound = 1;
    int endofTimer = 0;
    int rEXP, cEXP, currentEXP, currentCoins, coinsReceived;
    string ops, difficulty;

    public Sprite[] gameStars_gp, gameStars_mp;
    public Image mpStars, gpStars;

    public GameObject FailedMenu, CompleteMenu, PauseMenu;

    public updateHandler updating;
    public jsonConverter jsonSaving;

    public Button btn_tryAgain, btn_doubleCoins;

    [SerializeField]
    TextMeshProUGUI gameTimer, btnA, btnB, btnC, btnD,
                          number1, number2, roundsText, gameLevel,
                                menuLevel, menuLevel02,operation;
    [SerializeField]
    TextMeshProUGUI menuFexp, menuCexp, menuFcoins, menuCcoins;

    public Slider roundCounter;

    #region INITIALIZATIONS



    private void setLevelDetails(PlayerInfo pinfo)
    {
        Debug.Log(pinfo.currDifficulty);
        difficulty = pinfo.currDifficulty;
        setLevel(ComparingArrayDB.getBasicA(pinfo.currLevel, pinfo.currCategory), pinfo); //returns current level's information
        ops = pinfo.currOperation;
        currentEXP = pinfo.currExp;
        currentCoins = pinfo.coins;
    }
    private void setLevel(ComparingArray i, PlayerInfo playerInfo)
    {
        equation = i.equation[playerInfo.currLevel];
        level = i.level[playerInfo.currLevel];
        startTime = i.timer[playerInfo.currLevel];
        cEXP = i.completionEXP[playerInfo.currLevel];
        rEXP = i.rewardEXP[playerInfo.currLevel];

    }
    public void setRoundValues()
    {
        roundCounter.maxValue = levelRounds;
        roundCounter.value = currRound;
    }
    public void setRoundValue(int round)
    {
        roundsText.text = round + "/" + levelRounds;
        roundCounter.value = round;
    }
    void displayCurrLevel()
    {
        gameLevel.text = "Level " + level;
        menuLevel.text = "Level " + level;
        menuLevel02.text = "Level " + level;
    }
    void setOps()
    {
        if (ops == "add")
            operation.text = "+";
                else if (ops == "sub")
                    operation.text = "-";
                        else if (ops == "mult")
                            operation.text = "×";
                                else
                                    operation.text = "÷";
    }
    void init()
    {
        disableGameMenu();
        //------------------
        setLevelDetails(PlayerInfoScript.getPlayerInfo());
        setRoundValues();
        setRoundValue(currRound);
        displayCurrLevel();
        gameLevel.text = "Level " + level;
        currentTime = startTime;
    }
    void disableGameMenu()
    {
        PauseMenu.SetActive(false);
        CompleteMenu.SetActive(false);
        FailedMenu.SetActive(false);
    }
    void displayExpFunction()
    {
        if (difficulty == "Basic A")
            displayEXP(LevelsArrayDB.getBasicA());
        else if (difficulty == "Basic B")
            displayEXP(LevelsArrayDB.getBasicB());
        else if (difficulty == "Normal A")
            displayEXP(LevelsArrayDB.getNormalA());
        else if (difficulty == "Normal B")
            displayEXP(LevelsArrayDB.getNormalB());
        else if (difficulty == "Hard")
            displayEXP(LevelsArrayDB.getHard());
        else if (difficulty == "Advanced")
            displayEXP(LevelsArrayDB.getAdvanced());
        else
            displayEXP(LevelsArrayDB.getUltra());
    }

    void success() //open complete menu and animate
    {
        if(menu_anim == 0)
        {
            menu_anim++;
            displayExpFunction();
            displayCurrLevel();
            PauseMenu.SetActive(true);
            CompleteMenu.SetActive(true);
            FailedMenu.SetActive(false);
            runAnim(0, 0);
        }
    }
    void failed() //open failed menu and animate
    {
        if (menu_anim == 0)
        {
            menu_anim++;
            displayCurrLevel();
            PauseMenu.SetActive(true);
            CompleteMenu.SetActive(false);
            FailedMenu.SetActive(true);
            menuFexp.SetText("+ " + 5 + " EXP");
            runAnim(1, 1);
        }
    }

    void runAnim(int trigger, int animHandler)
    {
        animate[animHandler].runTrigger(triggers[trigger]);
    }
    #endregion
    void displayEXP(LevelsArray i)
    {
        if (ops == "add")
        {
            if (!i.isDone_add[level - 1] && stars == 3)
            {
                menuCexp.SetText("+ " + cEXP + " EXP");
                menuCcoins.SetText("+ " + (cEXP / 2) + " COINS");
                coinsReceived = cEXP / 2;
            }
            else
            {
                menuCexp.SetText("+ " + rEXP + " EXP");
                menuCcoins.SetText("+ " + (rEXP / 2) + " COINS");
                coinsReceived = rEXP / 2;
            }
        }
        else if (ops == "sub")
        {
            if (!i.isDone_sub[level] && stars == 3)
            {
                menuCexp.SetText("+ " + cEXP + " EXP");
                menuCcoins.SetText("+ " + (cEXP / 2) + " COINS");
                coinsReceived = cEXP / 2;
            }
            else
            {
                menuCexp.SetText("+ " + rEXP + " EXP");
                menuCcoins.SetText("+ " + (rEXP / 2) + " COINS");
                coinsReceived = rEXP / 2;
            }
        }
        else if (ops == "mult")
        {
            if (!i.isDone_mult[level] && stars == 3)
            {
                menuCexp.SetText("+ " + cEXP + " EXP");
                menuCcoins.SetText("+ " + (cEXP / 2) + " COINS");
                coinsReceived = cEXP / 2;
            }
            else
            {
                menuCexp.SetText("+ " + rEXP + " EXP");
                menuCcoins.SetText("+ " + (rEXP / 2) + " COINS");
                coinsReceived = rEXP / 2;
            }
        }
        else
        {
            if (!i.isDone_div[level] && stars == 3)
            {
                menuCexp.SetText("+ " + cEXP + " EXP");
                menuCcoins.SetText("+ " + (cEXP / 2) + " COINS");
                coinsReceived = cEXP / 2;
            }
            else
            {
                menuCexp.SetText("+ " + rEXP + " EXP");
                menuCcoins.SetText("+ " + (rEXP / 2) + " COINS");
                coinsReceived = rEXP / 2;
            }
        }
    }
    public void receiveExtraCoins()
    {
        updating.updateCoins(currentCoins, coinsReceived);
        setLevelDetails(PlayerInfoScript.getPlayerInfo());
        btn_doubleCoins.interactable = false;
    }
    void expGetter(LevelsArray i)
    {
        if (ops == "add") {
            if (!i.isDone_add[level] && stars == 3)
            {
                updating.updatePlayerEXP(currentEXP, cEXP);
                updating.updateCoins(currentCoins, cEXP / 2);
            }
            else
            {
                updating.updatePlayerEXP(currentEXP, rEXP);
                updating.updateCoins(currentCoins, rEXP / 2);
            }
        }
        else if (ops == "sub")
        {
            if (!i.isDone_sub[level] && stars == 3)
            {
                updating.updatePlayerEXP(currentEXP, cEXP);
                updating.updateCoins(currentCoins, cEXP / 2);
            }
            else
            {
                updating.updatePlayerEXP(currentEXP, rEXP);
                updating.updateCoins(currentCoins, rEXP / 2);
            }
        }
        else if (ops == "mult")
        {
            if (!i.isDone_mult[level] && stars == 3)
            {
                updating.updatePlayerEXP(currentEXP, cEXP);
                updating.updateCoins(currentCoins, cEXP / 2);
            }
            else
            {
                updating.updatePlayerEXP(currentEXP, rEXP);
                updating.updateCoins(currentCoins, rEXP / 2);
            }
        }
        else
        {
            if (!i.isDone_div[level] && stars == 3)
            {
                updating.updatePlayerEXP(currentEXP, cEXP);
                updating.updateCoins(currentCoins, cEXP / 2);
            }
            else
            {
                updating.updatePlayerEXP(currentEXP, rEXP);
                updating.updateCoins(currentCoins, rEXP / 2);
            }
        }
        currentEXP = 0;
    }
    void expGetFunction()
    {
        if (difficulty == "Basic A")
            expGetter(LevelsArrayDB.getBasicA());
        else if (difficulty == "Basic B")
            expGetter(LevelsArrayDB.getBasicB());
        else if (difficulty == "Normal A")
            expGetter(LevelsArrayDB.getNormalA());
        else if (difficulty == "Normal B")
            expGetter(LevelsArrayDB.getNormalB());
        else if (difficulty == "Hard")
            expGetter(LevelsArrayDB.getHard());
        else if (difficulty == "Advanced")
            expGetter(LevelsArrayDB.getAdvanced());
        else
            expGetter(LevelsArrayDB.getUltra());
    }
    void saveLevelJson()
    {
        if (difficulty == "Basic A")
            jsonSaving.saveLevels(LevelsArrayDB.getBasicA(), difficulty);
        else if (difficulty == "Basic B")
            jsonSaving.saveLevels(LevelsArrayDB.getBasicB(), difficulty);
        else if (difficulty == "Normal A")
            jsonSaving.saveLevels(LevelsArrayDB.getNormalA(), difficulty);
        else if (difficulty == "Normal B")
            jsonSaving.saveLevels(LevelsArrayDB.getNormalB(), difficulty);
        else if (difficulty == "Hard")
            jsonSaving.saveLevels(LevelsArrayDB.getHard(), difficulty);
        else if (difficulty == "Advanced")
            jsonSaving.saveLevels(LevelsArrayDB.getAdvanced(), difficulty);
        else
            jsonSaving.saveLevels(LevelsArrayDB.getUltra(), difficulty);
    }
    public void nextLevel() //fuctions when next level button is pressed
    {
        disableGameMenu();
        expGetFunction(); //add and save EXP
        updating.updateLevelDetails(level, stars, ops, difficulty); //update level scriptable object LEVEL Nth
        updating.unlockNextLVL(level, ops, difficulty);
        updating.updateCurrLevel(level); // update pInfo level
        jsonSaving.savePinfotoJSON(PlayerInfoScript.getPlayerInfo());
        saveLevelJson();
        setLevelDetails(PlayerInfoScript.getPlayerInfo());
        displayCurrLevel();
        setRoundValues();
        setRoundValue(currRound);
        currentTime = startTime;
        currRound = 1;
        correctansCounter = 0;
        stars = 0;
        gpStars.sprite = gameStars_gp[0];
        mpStars.sprite = gameStars_mp[0];
        btn_tryAgain.interactable = true;
        btn_doubleCoins.interactable = true;
        menu_anim = 0;
    }
    public void tryAgain()
    {
        disableGameMenu();
        expGetFunction(); //add and save EXP
        jsonSaving.savePinfotoJSON(PlayerInfoScript.getPlayerInfo());
        saveLevelJson();
        setLevelDetails(PlayerInfoScript.getPlayerInfo());
        displayCurrLevel();
        setRoundValues();
        setRoundValue(currRound);
        currentTime = startTime;
        currRound = 1;
        //stars = 0;
        correctansCounter = 0;
        gpStars.sprite = gameStars_gp[0];
        mpStars.sprite = gameStars_mp[0];
        menu_anim = 0;
    }
    void nextRound() // after every round
    {
        if(currRound != levelRounds)
        {
            currRound++;
        }
        else
        {
            if (correctansCounter == 0)
                failed();
                    else
                        success();
        }
    }
    
    void updateMPSTars() // updates game stars 
    {
        if (correctansCounter < 1)
        {
            mpStars.sprite = gameStars_mp[0];
            gpStars.sprite = gameStars_gp[0];
            stars = 0;
        }
        else if (correctansCounter < (levelRounds * .5))
        {
            mpStars.sprite = gameStars_mp[1];
            gpStars.sprite = gameStars_gp[1];
            stars = 1;
        }
        else if (correctansCounter <= (levelRounds * .75))
        {
            mpStars.sprite = gameStars_mp[2];
            gpStars.sprite = gameStars_gp[2];
            stars = 2;
        }
        else
        {
            mpStars.sprite = gameStars_mp[3];
            gpStars.sprite = gameStars_gp[3];
            stars = 3;
        }
        displayExpFunction();
        if (stars == 3)
        {
            btn_tryAgain.interactable = false;
        }
        //else if(correctansCounter == levelRounds)
        //{
        //    mpStars.sprite = gameStars_mp[3];
        //    gpStars.sprite = gameStars_gp[3];
        //    stars = 3;
        //}   
    }

    void Start()
    {
        init();
        setOps();
    }

    void Update()
    {
        setRoundValue(currRound);
        runTimer();
        if (currentTime <= endofTimer)
        {
            nextRound();
            currentTime = startTime;
        }
        //updateMPSTars();
    }

    public void runTimer()
    {
        currentTime -= 1 * Time.deltaTime;
        gameTimer.text = currentTime.ToString("00:00");
    }

    void verifyAnswer(string answer)
    {
        if (answer == correctAns + "")
        {
            correctansCounter++;
            nextRound();
            currentTime = startTime;
        }
        else
        {
            nextRound();
            currentTime = startTime;
        }
        updateMPSTars();
    }

    #region Get Choice Values
    public void getChoiceA()
    {
        verifyAnswer(btnA.text);
    }
    public void getChoiceB()
    {
        verifyAnswer(btnB.text);
    }
    #endregion

}
