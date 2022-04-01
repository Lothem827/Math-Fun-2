using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelsArrayDB : MonoBehaviour
{
    public LevelsSelection _dbLevels; //script with <Lists>
    private static LevelsArrayDB instance; //this script

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static LevelsArray getBasicA() // Struct for Levels (initializations)
    {
        return instance._dbLevels._basicA.FirstOrDefault();
    }
    public static LevelsArray getBasicB() // Struct for Levels (initializations)
    {
        return instance._dbLevels._basicB.FirstOrDefault();
    }
    public static LevelsArray getNormalA() // Struct for Levels (initializations)
    {
        return instance._dbLevels._normalA.FirstOrDefault();
    }
    public static LevelsArray getNormalB() // Struct for Levels (initializations)
    {
        return instance._dbLevels._normalB.FirstOrDefault();
    }
    public static LevelsArray getHard() // Struct for Levels (initializations)
    {
        return instance._dbLevels._hard.FirstOrDefault();
    }
    public static LevelsArray getAdvanced() // Struct for Levels (initializations)
    {
        return instance._dbLevels._advanced.FirstOrDefault();
    }
    public static LevelsArray getUltra() // Struct for Levels (initializations)
    {
        return instance._dbLevels._ultra.FirstOrDefault();
    }
}
