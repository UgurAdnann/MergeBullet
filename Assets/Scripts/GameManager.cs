using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelEditor levelEditor;

    private GameObject currentBullet;
    private BulletController currentBulletController;

    private void Awake()
    {
        ObjectManager.GameManager = this;
    }

    public void Merge(BulletController firstBullet, BulletController secondBullet)
    {
        firstBullet.ResetGrid();
        currentBullet = Instantiate(levelEditor.bulletDatas[firstBullet.bulletType].prefab);
        currentBulletController = currentBullet.GetComponent<BulletController>();

        currentBulletController.pos = secondBullet.pos;
        currentBulletController.bulletType = levelEditor.bulletDatas[firstBullet.bulletType].type;
        currentBulletController.hitValue = levelEditor.bulletDatas[firstBullet.bulletType].hitValue;

        currentBulletController.transform.SetParent(secondBullet.transform.parent);
        currentBulletController.transform.localPosition = Vector3.zero;

        secondBullet.currentGridController.bulletType = currentBulletController.bulletType;

        Destroy(firstBullet.gameObject);
        Destroy(secondBullet.gameObject);
    }
}
