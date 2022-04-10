using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level Database", menuName = "Assets/Databases/ComparingDB")]
public class ComparingSelection : ScriptableObject
{
    public List<LevelsArray> _basicA;
    public List<LevelsArray> _basicB;
    public List<LevelsArray> _normalA;
    public List<LevelsArray> _normalB;
    public List<LevelsArray> _hard;
    public List<LevelsArray> _advanced;
    public List<LevelsArray> _ultra;
}
