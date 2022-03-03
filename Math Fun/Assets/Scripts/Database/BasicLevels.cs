using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Assets/Basic Levels")]
public class BasicLevels : ScriptableObject
{
    public string levelCategory;
    public float timer;
    public int rounds;
    public int level;
    public int minNum;
    public int maxNum;
    public int completionEXP;
    public int rewardEXP;

}
