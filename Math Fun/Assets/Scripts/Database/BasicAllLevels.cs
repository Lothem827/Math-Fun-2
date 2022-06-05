using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level Database", menuName = "Assets/Databases/Basic A Levels")]
public class BasicAllLevels : ScriptableObject
{
    public List<BasicLevels> allALevels;
}
