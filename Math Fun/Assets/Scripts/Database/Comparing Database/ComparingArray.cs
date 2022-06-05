using UnityEngine;

[CreateAssetMenu(fileName = "Comparing Status", menuName = "Assets/Comparing Stats")]
public class ComparingArray : ScriptableObject
{
    public int id;
    public int[] level = new int[50];
    public int[] timer = new int[50];
    public string[] equation = new string[50];
    public bool[] answer = new bool[50];
    public bool[] isDone = new bool[50];
    public int[] stars = new int[50];
    public bool[] notLocked = new bool[50];
    public int[] completionEXP = new int[50];
    public int[] rewardEXP = new int[50];

}
