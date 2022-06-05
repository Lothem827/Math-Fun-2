using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class mainScript : MonoBehaviour
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
    int num1 = 0, num2 = 0;
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
        setLevel(BasicLevelDatabase.getCurrentLevel(pinfo.currLevel)); //returns current level's information
        ops = pinfo.currOperation;
        currentEXP = pinfo.currExp;
        currentCoins = pinfo.coins;
    }
    private void setLevel(BasicLevels i)
    {
        if(difficulty == "Basic A")
        {
            min = i.minNum;
            max = i.maxNum;
        }else if (difficulty == "Basic B")
        {
            min = i.minNum + 4;
            max = i.maxNum + 8;
        }
        else if (difficulty == "Normal A")
        {
            min = i.minNum + 8;
            max = i.maxNum + 16;
        }
        else if (difficulty == "Normal B")
        { 
            min = i.minNum + 12;
            max = i.maxNum + 24;
        }
        else if (difficulty == "Hard")
        {
            min = i.minNum + 24;
            max = i.maxNum + 42;
        }
        else if (difficulty == "Advanced")
        {
            min = i.minNum + 48;
            max = i.maxNum + 84;
        }
        else
        {
            min = i.minNum + 105;
            max = i.maxNum + 150;
        }
        levelRounds = i.rounds;
        level = i.level;
        startTime = i.timer;
        cEXP = i.completionEXP;
        rEXP = i.rewardEXP;

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
        random();
        placeChoices();
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
        randomizer();
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
        randomizer();
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
            randomizer();
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
            randomizer();
            nextRound();
            currentTime = startTime;
        }
        else
        {
            randomizer();
            nextRound();
            currentTime = startTime;
        }
        updateMPSTars();
    }

    void randomizer()
    {
        random();
        placeChoices();
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
    public void getChoiceC()
    {
        verifyAnswer(btnC.text);
    }
    public void getChoiceD()
    {
        verifyAnswer(btnD.text);
    }
    #endregion

    #region Random Placement
    public void random()
    {
        num1 = Random.Range(min, max);
        num2 = Random.Range(min, max);

        if (num2 > num1 && ops == "sub"){
            number1.text = num2.ToString();
            number2.text = num1.ToString();
        }
        else if (ops == "div"){
            number1.text = (num1 * num2).ToString();
            number2.text = num2.ToString();
        }
        else{
            number1.text = num1.ToString();
            number2.text = num2.ToString();
        }

        getAnswer(int.Parse(number1.text), int.Parse(number2.text));
    }
    void getAnswer(int nmb1, int nmb2)
    {
        if (ops == "add")
            correctAns = nmb1 + nmb2;
                else if (ops == "sub")
                    correctAns = nmb1 - nmb2;
                        else if (ops == "mult")
                            correctAns = nmb1 * nmb2;
                                else
                                    correctAns = nmb1 / nmb2;
    }


    void placeChoices()
    {
        List<int> test = new List<int>();
        int[] arrayTest = new int[4];
        test.Add(correctAns);

        for (int i = 1; i < 4; i++)
        {
            int rand = Random.Range((correctAns + 1), (correctAns + 5));
            if (!test.Contains(rand))
            {
                test.Add(rand);
            }
            else
            {
                i--;
            }
        }


        test.Sort((a, b) => 1 - 2 * Random.Range(0, test.Count));

        btnA.text = test[0].ToString();
        btnB.text = test[1].ToString();
        btnC.text = test[2].ToString();
        btnD.text = test[3].ToString();


    }
    #endregion
}
