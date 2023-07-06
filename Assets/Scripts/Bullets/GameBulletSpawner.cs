using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBulletSpawner : MonoBehaviour
{
    #region Variables for General
    public DataBase dataBase;
    public LevelEditor levelEditor;
    private WallSpawner wallSpawner;
    #endregion

    #region Variables for Bullets
    private GameObject tempBullet;
    private BulletController tempBulletController, unbeatableBullet;
    private Transform startBulletsParent;
    public Vector3 startPos;
    #endregion

    void Start()
    {
        wallSpawner = ObjectManager.wallSpawner;
        startBulletsParent = GameObject.FindGameObjectWithTag("StartBulletsParent").transform;

        CreateInitialBullets();
    }

    void Update()
    {
        
    }

    private void CreateInitialBullets()
    {
        for (int i = 0; i < dataBase.bulletSaves.Count; i++)
        {
            //Create Bullet
            tempBullet = Instantiate(levelEditor.bulletDatas[dataBase.bulletSaves[i].type - 1].prefab);
            tempBullet.transform.SetParent(startBulletsParent);

            //Set Bullet Properties
            tempBulletController = tempBullet.GetComponent<BulletController>();
            tempBulletController.bulletType = dataBase.bulletSaves[i].type;
            tempBulletController.hp = dataBase.bulletSaves[i].hp;
            tempBulletController.pos = dataBase.bulletSaves[i].pos;
            tempBulletController.hitValue = dataBase.bulletSaves[i].hitValue;
            tempBulletController.isGameBullet = true;

            //Select Unbeatable bullet
            if (unbeatableBullet == null)
                unbeatableBullet = tempBulletController;
            else
            {
                unbeatableBullet = tempBulletController.bulletType >= unbeatableBullet.bulletType ? tempBulletController : unbeatableBullet;
            }

            //Set Bullet Transform
            tempBullet.transform.localScale = tempBullet.transform.localScale * 2;
            tempBullet.transform.position =startPos+ new Vector3(tempBulletController.pos.x *2, 0, tempBulletController.pos.y*2);
            tempBullet.transform.rotation = Quaternion.Euler(90, 0, 0);

            //Start Move
            startBulletsParent.GetComponent<StartBullets>().isMoveForward = true;
        }
        unbeatableBullet.isUnbeatable = true;
        //Set WallLength
        wallSpawner.row = unbeatableBullet.hp + 2;
        wallSpawner.SpawnWall();
    }
}
