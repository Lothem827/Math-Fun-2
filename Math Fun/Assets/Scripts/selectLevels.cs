using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class selectLevels : MonoBehaviour
{
    //Notes: isLocked [0] Add [1] Sub [2] Mult [3] Div

    public Image[] buttons;
    public TextMeshProUGUI[] btn_Texts;
    public updateHandler UpHandler;
    public Sprite[] level_stars_sprite;
    public updateHandler updating;
    public PlayerInfo pInfo;
    int l_stars = 0;
    bool isDone;
    string difficulty;

    void Start()
    {
        difficulty = pInfo.currDifficulty;   
        for (int i = 0; i < buttons.Length; i++)
        {
            btn_Texts[i].text = (i + 1).ToString();
            setLevelDetails(i , PlayerInfoScript.getPlayerInfo()); //call method to retrieve level information
            setLevels(l_stars, i,isDone);
        }

    }

    public void setLevels(int _stars, int index, bool isDone)
    {
            if(_stars == 3 && isDone)
                buttons[index].sprite = level_stars_sprite[3];
            else if (_stars == 2 && isDone)
                buttons[index].sprite = level_stars_sprite[2];
            else if (_stars == 1 && isDone)
                buttons[index].sprite = level_stars_sprite[1];
            else if (_stars == 0 && isDone)
                buttons[index].sprite = level_stars_sprite[0];
            else
            {
                buttons[index].sprite = level_stars_sprite[4];
                buttons[index].GetComponent<Button>().interactable = false;
            }
                

    }

    void setLevelDetails(int level, PlayerInfo pinfo)
    {
        if(difficulty == "Basic A")
            setLevel(LevelsArrayDB.getBasicA(), pinfo.currOperation, level); //returns current level's information
        else if(difficulty == "Basic B")
            setLevel(LevelsArrayDB.getBasicB(), pinfo.currOperation, level); //returns current level's information
        else if (difficulty == "Normal A")
            setLevel(LevelsArrayDB.getNormalA(), pinfo.currOperation, level); //returns current level's information
        else if (difficulty == "Normal B")
            setLevel(LevelsArrayDB.getNormalB(), pinfo.currOperation, level); //returns current level's information
        else if (difficulty == "Hard")
            setLevel(LevelsArrayDB.getHard(), pinfo.currOperation, level); //returns current level's information
        else if (difficulty == "Advanced")
            setLevel(LevelsArrayDB.getAdvanced(), pinfo.currOperation, level); //returns current level's information
        else
            setLevel(LevelsArrayDB.getUltra(), pinfo.currOperation, level); //returns current level's information
    }

    void setLevel(LevelsArray i, string ops, int level) //stores Level Information
    {
        if (ops == "add") 
        { 
            l_stars = i.opStars_add[level];
            isDone = i.notLocked_add[level];
        }
        else if (ops == "sub")
        {
            l_stars = i.opStars_sub[level];
            isDone = i.notLocked_sub[level];
        }
        else if (ops == "mult")
        {
            l_stars = i.opStars_mult[level];
            isDone = i.notLocked_mult[level];
        }
        else
        {
            l_stars = i.opStars_div[level];
            isDone = i.notLocked_div[level];
        }
    }

    public void levelSelector(int level)
    {
        updating.updateCurrLevel(level);
    }

    void lockLevel()
    {
        //if()
    }
}
