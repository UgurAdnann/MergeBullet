using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public DataBase dataBase;
    public void StartGame()
    {
        DataManager.SaveData(dataBase);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
