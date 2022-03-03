using UnityEngine;

[CreateAssetMenu(fileName = "Level Status", menuName = "Assets/Levels Stats")]
public class LevelsArray : ScriptableObject
{
    public int[] opStars_add = new int[50];
    public int[] opStars_sub = new int[50];
    public int[] opStars_div = new int[50];
    public int[] opStars_mult = new int[50];

    public bool[] isDone_add = new bool[50];
    public bool[] isDone_sub = new bool[50];
    public bool[] isDone_div = new bool[50];
    public bool[] isDone_mult = new bool[50];

    public bool[] notLocked_add = new bool[50];
    public bool[] notLocked_sub = new bool[50];
    public bool[] notLocked_div = new bool[50];
    public bool[] notLocked_mult = new bool[50];

}
