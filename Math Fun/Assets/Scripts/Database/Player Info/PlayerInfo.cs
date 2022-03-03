using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "Assets/Databases/Player Info")]
public class PlayerInfo : ScriptableObject
{
    public int id;
    public int basis;
    public int playerLevel;
    public int coins;
    public int baseExp;
    public int currExp;
    public string currCategory;
    public string currOperation;
    public int currLevel;
}
