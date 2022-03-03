using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level Database", menuName = "Assets/Databases/LevelsDB")]
public class LevelsSelection : ScriptableObject
{
    public List<LevelsArray> LevelsDB;
}
