using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class updateHandler : MonoBehaviour
{
    public List<string> categories = new List<string>();
    public List<string> parentFolder = new List<string>();
    string[] diffs = new string[]{ "Basic A", "Basic B", "Normal A", "Normal B"
                                    , "Hard", "Advanced", "Ultra"};
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
    LevelsArray levelinfo(string diff)
    {
        if(diff == "Basic A")
            return Resources.Load<LevelsArray>("_SO/Levels Stats/" + diffs[0]);
        else if (diff == "Basic B")
            return Resources.Load<LevelsArray>("_SO/Levels Stats/" + diffs[1]);
        else if (diff == "Normal A")
            return Resources.Load<LevelsArray>("_SO/Levels Stats/" + diffs[2]);
        else if (diff == "Normal B")
            return Resources.Load<LevelsArray>("_SO/Levels Stats/" + diffs[3]);
        else if (diff == "Hard")
            return Resources.Load<LevelsArray>("_SO/Levels Stats/" + diffs[4]);
        else if (diff == "Advanced")
            return Resources.Load<LevelsArray>("_SO/Levels Stats/" + diffs[5]);
        else
            return Resources.Load<LevelsArray>("_SO/Levels Stats/" + diffs[6]);
    }
    public void updateLevelDetails(int level, int stars, string ops, string diff)
    {
        LevelsArray levelInfo = levelinfo(diff);

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
                {
                    if (levelInfo.opStars_add[level - 1] != 3)
                        levelInfo.opStars_add[level - 1] = stars;
                }
            }
            else if (ops == "sub")
            {
                if (stars == 3)
                {
                    levelInfo.isDone_sub[level - 1] = true;
                    levelInfo.opStars_sub[level - 1] = stars;
                }
                else
                {
                    if (levelInfo.opStars_sub[level - 1] != 3)
                        levelInfo.opStars_sub[level - 1] = stars;
                }
            }
            else if (ops == "mult")
            {
                if (stars == 3)
                {
                    levelInfo.isDone_mult[level - 1] = true;
                    levelInfo.opStars_mult[level - 1] = stars;
                }
                else
                {
                    if (levelInfo.opStars_mult[level - 1] != 3)
                        levelInfo.opStars_mult[level - 1] = stars;
                }
            }
            else
            {
                if (stars == 3)
                {
                    levelInfo.isDone_div[level - 1] = true;
                    levelInfo.opStars_div[level - 1] = stars;
                }
                else
                {
                    if (levelInfo.opStars_div[level - 1] != 3)
                        levelInfo.opStars_div[level - 1] = stars;
                }
            }
        }
    }

    public void updateTruelseScore(int score, string ops)
    {
        if(ops == "add")
            playerInfo.truelseScoreAdd = score;
        else
            playerInfo.truelseScoreMult = score;

    }
    public void unlockNextLVL(int level, string ops, string diff)
    {
        LevelsArray levelInfo = levelinfo(diff);

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
