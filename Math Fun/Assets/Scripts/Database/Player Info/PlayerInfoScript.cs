using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInfoScript : MonoBehaviour
{
    public Pinfo pInfo;

    private static PlayerInfoScript instance;

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

    public static PlayerInfo getPlayerInfo()
    {
        //return instance.pInfo.playerInformation.FirstOrDefault(i => i.id == 1);
        return instance.pInfo.playerInformation.FirstOrDefault(i => i.basis == 10);
    }
}
