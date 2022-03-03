using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class updateHandler : MonoBehaviour
{
    public List<string> categories = new List<string>();
    public List<string> parentFolder = new List<string>();
    int baseEXP;
    PlayerInfo playerInfo = null;

    private void Start()
    {
        playerInfo = Resources.Load<PlayerInfo>("_SO/Player Info/playerInfo");
        
    }

    public void updateCoins(int _cCoins, int _pCoins)
    {
        playerInfo.coins = _cCoins + _pCoins;
    }
    public void updatePlayerEXP(int _exp, int _eExp)
    {
        baseEXP = playerInfo.baseExp;
        if (_exp > baseEXP){
            _exp += _eExp;
            int temp = _exp - playerInfo.baseExp;
            playerInfo.currExp = temp;
            playerInfo.playerLevel += 1;
            playerInfo.baseExp += playerInfo.baseExp;
        }else if (_exp == baseEXP){
            playerInfo.currExp = 0;
            playerInfo.playerLevel += 1;
            playerInfo.baseExp += playerInfo.baseExp;
        }else{
            playerInfo.currExp = _exp + _eExp;
        }
    }

    public void updateLevelDetails(int level, int stars, string ops)
    {
        LevelsArray levelInfo = null;
        levelInfo = Resources.Load<LevelsArray>("_SO/Levels Stats/LevelsStats");

        //changes
        if (level < 50)
        {
            if (ops == "add")
            {
                if (stars == 3)
                {
                    levelInfo.isDone_add[level - 1] = true;
                    levelInfo.opStars_add[level - 1] = stars;
                }
                else
                    levelInfo.opStars_add[level - 1] = stars;
            }
            else if (ops == "sub")
            {
                if (stars == 3)
                {
                    levelInfo.isDone_sub[level - 1] = true;
                    levelInfo.opStars_sub[level - 1] = stars;
                }
                else
                    levelInfo.opStars_sub[level - 1] = stars;
            }
            else if (ops == "mult")
            {
                if (stars == 3)
                {
                    levelInfo.isDone_mult[level - 1] = true;
                    levelInfo.opStars_mult[level - 1] = stars;
                }
                else
                    levelInfo.opStars_mult[level - 1] = stars;
            }
            else
            {
                if (stars == 3)
                {
                    levelInfo.isDone_div[level - 1] = true;
                    levelInfo.opStars_div[level - 1] = stars;
                }
                else
                    levelInfo.opStars_div[level - 1] = stars;
            }
        }
    }
    public void unlockNextLVL(int level, string ops)
    {
        LevelsArray levelInfo = null;
        levelInfo = Resources.Load<LevelsArray>("_SO/Levels Stats/LevelsStats");

        //changes
        if (level < 50)
        {
            if (ops == "add")
            {
                levelInfo.notLocked_add[level] = true;
            }
            else if (ops == "sub")
            {
                levelInfo.notLocked_sub[level] = true;
            }
            else if (ops == "mult")
            {
                levelInfo.notLocked_mult[level] = true;
            }
            else
            {
                levelInfo.notLocked_div[level] = true;
            }
        }
    }

    public void updateCurrLevel(int level)
    {
        //changes
        if (level + 1 >= 50)
            playerInfo.currLevel = level;
                 else
                    playerInfo.currLevel = level + 1;

        
    }



}
