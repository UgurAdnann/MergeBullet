using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Variables for General
    public LevelEditor levelEditor;
    public DataBase dataBase;
    private GridCreator gridCreator;
    #endregion

    #region Variables for Bullets
    private GameObject currentBullet;
    private BulletController currentBulletController;
    public List<BulletController> bullets = new List<BulletController>();
    #endregion

    private void Awake()
    {
        ObjectManager.GameManager = this;
        DataManager.LoadData(dataBase);
    }

    private void Start()
    {
        gridCreator = ObjectManager.GridCreator;
        CreateBullets();
    }

    private void Update()
    {
        ResetData();
    }

    #region Variables for Bullets
    public void CreateBullets()
    {
        for (int i = 0; i < dataBase.bulletSaves.Count; i++)
        {
            //Create Bullet
            currentBullet = Instantiate(levelEditor.bulletDatas[dataBase.bulletSaves[i].type - 1].prefab);
            currentBulletController = currentBullet.GetComponent<BulletController>();
            currentBullet.transform.SetParent(gridCreator.grids[dataBase.bulletSaves[i].GridNum].transform);
            currentBullet.transform.localPosition = Vector3.zero;

            //Set bullet properties
            bullets.Add(currentBulletController);
            currentBulletController.bulletType = levelEditor.bulletDatas[dataBase.bulletSaves[i].type - 1].type;
            currentBulletController.hitValue = levelEditor.bulletDatas[dataBase.bulletSaves[i].type - 1].hitValue;
            currentBulletController.gridNum = dataBase.bulletSaves[i].GridNum;

            //Set Grid Settings
            gridCreator.grids[dataBase.bulletSaves[i].GridNum].GetComponent<GridController>().bulletType = currentBulletController.bulletType;
            gridCreator.grids[dataBase.bulletSaves[i].GridNum].GetComponent<GridController>().gridSit = GridSit.Fill;
            gridCreator.emptyGrids.Remove(gridCreator.grids[dataBase.bulletSaves[i].GridNum]);
        }
    }

    public void Merge(BulletController firstBullet, BulletController secondBullet)
    {
        firstBullet.ResetGrid();

        //Create Bullet
        currentBullet = Instantiate(levelEditor.bulletDatas[firstBullet.bulletType].prefab);
        currentBulletController = currentBullet.GetComponent<BulletController>();
        bullets.Add(currentBulletController);

        //Set bullet properties
        currentBulletController.gridNum = secondBullet.gridNum;
        currentBulletController.bulletType = levelEditor.bulletDatas[firstBullet.bulletType].type;
        currentBulletController.hitValue = levelEditor.bulletDatas[firstBullet.bulletType].hitValue;

        //Set bullet pos
        currentBulletController.transform.SetParent(secondBullet.transform.parent);
        currentBulletController.transform.localPosition = Vector3.zero;

        //Set current grid properties
        secondBullet.currentGridController.bulletType = currentBulletController.bulletType;

        //Merged bullet destroy events
        bullets.Remove(firstBullet);
        bullets.Remove(secondBullet);
        Destroy(firstBullet.gameObject);
        Destroy(secondBullet.gameObject);

        SaveSystem();
    }
    #endregion

    #region Variables for SaveSytem
    private void ResetData()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            dataBase.bulletSaves.Clear();
            dataBase.money = levelEditor.StartMoney;
            DataManager.SaveData(dataBase);
        }
    }

    public void SaveSystem()
    {
        dataBase.bulletSaves.Clear();
        for (int i = 0; i < bullets.Count; i++)
        {
            dataBase.bulletSaves.Add(new DataBase.BulletSave { type = bullets[i].bulletType, GridNum = bullets[i].gridNum });
        }

        DataManager.SaveData(dataBase);
    }
    #endregion
}
