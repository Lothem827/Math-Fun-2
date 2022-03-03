using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pinfo", menuName = "Assets/Databases/playerInfo")]
public class Pinfo : ScriptableObject
{
    public List<PlayerInfo> playerInformation;
}
