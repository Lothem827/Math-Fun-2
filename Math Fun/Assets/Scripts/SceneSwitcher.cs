using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public jsonConverter updating;
    public void tohomeScreen()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
    }
    public void toGameplay()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
    public void BasicLevels(string _ops)
    {
        Destroy(gameObject);
        updating.updateCategory(PlayerInfoScript.getPlayerInfo(),_ops);
        SceneManager.LoadScene("DifficultyMenu - Classic", LoadSceneMode.Single);
    }
    public void toArcadetoClassic(string _to)
    {
        if(_to == "Classic")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("DifficultyMenu - Classic", LoadSceneMode.Single);
        }
        else
        {
            Destroy(gameObject);
            SceneManager.LoadScene("DifficultyMenu - Arcade", LoadSceneMode.Single);
        }
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
        SceneManager.LoadScene("DifficultyMenu - Classic", LoadSceneMode.Single);
    }
}
