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
        if (File.Exists(paths("/BasicA.json")))
            loadJsonLevels("/BasicA.json", levels);

        if (File.Exists(paths("/BasicB.json")))
            loadJsonLevels("/BasicB.json", levels);

        if (File.Exists(paths("/NormalA.json")))
            loadJsonLevels("/NormalA.json", levels);

        if (File.Exists(paths("/NormalB.json")))
            loadJsonLevels("/NormalB.json", levels);

        if (File.Exists(paths("/Hard.json")))
            loadJsonLevels("/Hard.json", levels);

        if (File.Exists(paths("/Advanced.json")))
            loadJsonLevels("/Advanced.json", levels);

        if (File.Exists(paths("/Ultra.json")))
            loadJsonLevels("/Ultra.json", levels);
    }
    private void loadJsonLevels(string path, LevelsArray levels)
    {
        levels_JSON = File.ReadAllText(paths(path));
        JsonUtility.FromJsonOverwrite(levels_JSON, levels);
    }
    public void updateCategory(PlayerInfo pInfo, string _ops)
    {
        pInfo.currOperation = _ops;
        string Json = JsonUtility.ToJson(pInfo);
        File.WriteAllText(paths("/playerInfo.json"), Json);
    }
    public void updateDifficulty(PlayerInfo pInfo, string _diff)
    {
        pInfo.currDifficulty = _diff;
        string Json = JsonUtility.ToJson(pInfo);
        File.WriteAllText(paths("/playerInfo.json"), Json);
    }

    public void saveLevels(LevelsArray levels, string pathName)
    {
        string Json = JsonUtility.ToJson(levels);
        File.WriteAllText(paths("/"+ pathName +".json"), Json);
    }

}
