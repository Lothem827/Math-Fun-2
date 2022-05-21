using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ComparingArrayDB : MonoBehaviour
{
    public ComparingSelection _dbLevels; //script with <Lists>
    private static ComparingArrayDB instance; //this script

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

    public static ComparingArray getBasicA(int _diff) // Struct for Levels (initializations)
    {
        return instance._dbLevels._basicA.FirstOrDefault(i => i.id == _diff);
        //return instance._dbLevels._basicA.FirstOrDefault();
    }
}