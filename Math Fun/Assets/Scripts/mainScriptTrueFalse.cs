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
    bool correctAns;
    int level = 0, score = 0;
    int endofTimer = 0;
    int min = 1, max = 3;
    int rEXP, cEXP, currentEXP, currentCoins, coinsReceived;
    int numberCount = 2, eqAns;
    string _equations;
    string ops, difficulty;

    public GameObject FailedMenu, CompleteMenu, PauseMenu;

    public updateHandler updating;
    public jsonConverter jsonSaving;

    public Button btn_tryAgain, btn_doubleCoins;

    [SerializeField]
    TextMeshProUGUI equation, gameTimer, scoreTXT, menuLevel, menuLevel02;
    [SerializeField]
    TextMeshProUGUI menuCexp, menuCcoins;


    #region INITIALIZATIONS
    void init()
    {
        disableGameMenu();
        //------------------
        randomize();
        scoreTXT.text = score.ToString();
        startTime = 10;
        currentTime = startTime;
    }
    void disableGameMenu()
    {
        PauseMenu.SetActive(false);
        CompleteMenu.SetActive(false);
        FailedMenu.SetActive(false);
    }

    void success() //open complete menu and animate
    {
        if(menu_anim == 0)
        {
            menu_anim++;
            PauseMenu.SetActive(true);
            CompleteMenu.SetActive(true);
            FailedMenu.SetActive(false);
            runAnim(0, 0);
        }
    }
    void runAnim(int trigger, int animHandler)
    {
        //animate[animHandler].runTrigger(triggers[trigger]);
    }
    #endregion

    #region======= RANDOMIZER ================
    void randomize()
    {
        int[] nums = new int[numberCount];
        for(int i = 0; i < numberCount; i++)
        {
            nums[i] = Random.Range(min, max);
            eqAns += nums[i];
        }
        int randomAns = Random.Range(eqAns, eqAns + 3);
        if (randomAns == eqAns)
            correctAns = true;
        else
            correctAns = false;

        if (numberCount == 2)
        {
            _equations = nums[0] + " + " + nums[1] + " = " + randomAns;
        }
        else if (numberCount == 3)
        {
            _equations = nums[0] + " + " + nums[1] + " + " + nums[2] + " = " + randomAns;
        }
        else if (numberCount == 4)
        {
            _equations = nums[0] + " + " + nums[1] + " + " + nums[2] + " + " + nums[3] + " = " + randomAns;
        }

        equation.text = _equations;

    }
    void increaseNumbers()
    {
        if (score % 30 == 0)
        {
            if (numberCount < 4)
                numberCount++;
        }
        if (score % 3 == 0)
        {
            min++;
            max+= 2;
        }
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
        btn_doubleCoins.interactable = false;
    }
    public void nextLevel() //fuctions when next level button is pressed
    {
        updating.updateTruelseScore(score);
        jsonSaving.savePinfotoJSON(PlayerInfoScript.getPlayerInfo());
    }
    public void tryAgain()
    {
        disableGameMenu();
        jsonSaving.savePinfotoJSON(PlayerInfoScript.getPlayerInfo());
        currentTime = startTime;
        //gpStars.sprite = gameStars_gp[0];
        //mpStars.sprite = gameStars_mp[0];
        menu_anim = 0;
        score = 0;
        numberCount = 2;
        eqAns = 0;
        randomize();
    } 

    void Start()
    {
        init();
    }

    void Update()
    {
        runTimer();
        if (currentTime <= endofTimer)
        {
            success();
            currentTime = startTime;
        }
        //updateMPSTars();
    }

    public void runTimer()
    {
        currentTime -= 1 * Time.deltaTime;
        gameTimer.text = currentTime.ToString("00:00");
    }

    void verifyAnswer(bool answer)
    {
        if (answer == correctAns)
        {
            //success();
            menu_anim = 0;
            level++;
            score++;
            eqAns = 0;
            increaseNumbers();
            randomize();
            scoreTXT.text = score.ToString();
            currentTime = startTime;
        }
        else
        {
            success();
            currentTime = startTime;
        }
    }

    #region Get Choice Values
    public void returnTrue()
    {
        verifyAnswer(true);
    }
    public void returnFalse()
    {
        verifyAnswer(false);
    }
    #endregion

}
