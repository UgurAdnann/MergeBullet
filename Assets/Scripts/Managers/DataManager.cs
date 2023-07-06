using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public static void SaveData(DataBase dataBase)
    {
        string dataString = JsonUtility.ToJson(dataBase);
        Debug.Log("Save");
        PlayerPrefs.SetString("data", dataString);
    }
    public static void LoadData(DataBase dataBase)
    {
            Debug.Log("Load");
        if (!PlayerPrefs.HasKey("data"))
        {
            SaveData(dataBase);
            return;
        }
        string dataString = PlayerPrefs.GetString("data");
        JsonUtility.FromJsonOverwrite(dataString, dataBase);
    }
}
