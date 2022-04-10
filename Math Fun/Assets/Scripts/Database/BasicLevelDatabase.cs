using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BasicLevelDatabase : MonoBehaviour
{
    public BasicAllLevels Alevels; //script with <Lists>
    private static BasicLevelDatabase instance; //this script

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

    public static BasicLevels getCurrentLevel(int level) // Struct for Levels (initializations)
    {
        return instance.Alevels.allALevels.FirstOrDefault(i => i.level == level);
    }
}
