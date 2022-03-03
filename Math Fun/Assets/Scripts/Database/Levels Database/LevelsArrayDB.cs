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

    public static LevelsArray getCurrentLevel() // Struct for Levels (initializations)
    {
        return instance._dbLevels.LevelsDB.FirstOrDefault();
    }
}
