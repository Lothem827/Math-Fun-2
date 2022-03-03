using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class jsonConverter : MonoBehaviour
{
    public string playerInfo_JSON, levels_JSON;
    //public string[] level;
    public void Start()
    {
    }
    string paths(string _filename)
    {
        string path = Application.persistentDataPath + _filename;
        return path;
    }
    public void savePinfotoJSON(PlayerInfo pInfo)
    {
        string Json = JsonUtility.ToJson(pInfo);
        File.WriteAllText(paths("/playerInfo.json"), Json);
    }

    public void loadPlayerInfo(PlayerInfo pInfo)
    {
        if (File.Exists(paths("/playerInfo.json")))
        {
            playerInfo_JSON = File.ReadAllText(paths("/playerInfo.json"));
            JsonUtility.FromJsonOverwrite(playerInfo_JSON, pInfo);
        }
    }
    public void loadLevels(LevelsArray levels)
    {
        if (File.Exists(paths("/basic_levels.json")))
        {
            levels_JSON = File.ReadAllText(paths("/basic_levels.json"));
            JsonUtility.FromJsonOverwrite(levels_JSON, levels);
        }
    }

    public void updateCategory(PlayerInfo pInfo, string _ops)
    {
        pInfo.currOperation = _ops;
        string Json = JsonUtility.ToJson(pInfo);
        File.WriteAllText(paths("/playerInfo.json"), Json);
    }

    public void saveLevels(LevelsArray levels)
    {
        string Json = JsonUtility.ToJson(levels);
        File.WriteAllText(paths("/basic_levels.json"), Json);
    }

}
