using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public jsonConverter updating;
    public void homeScreen()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
    }
    public void gameplay()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
    public void BasicLevels(string _ops)
    {
        Destroy(gameObject);
        updating.updateCategory(PlayerInfoScript.getPlayerInfo(),_ops);
        SceneManager.LoadScene("DifficultyMenu", LoadSceneMode.Single);
    }
    public void Difficulty(string _diff)
    {
        Destroy(gameObject);
        updating.updateDifficulty(PlayerInfoScript.getPlayerInfo(), _diff);
        SceneManager.LoadScene("LevelMenu", LoadSceneMode.Single);
    }
    public void toLevels()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("LevelMenu", LoadSceneMode.Single);
    }

    public void levelsTodifficulty()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("DifficultyMenu", LoadSceneMode.Single);
    }
}
