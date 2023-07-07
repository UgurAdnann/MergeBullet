using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBulletSize : DoorController
{
    #region Variables for General
    public LevelManager levelManager;
    #endregion

    #region Varibales for Properties

    public float addBulletSize, minBulletSize, maxBulletSize;
    public Material positiveTextBGMat, negativeTextBGMat;
    public Material positiveDoorBGMat, negativeDoorBGMat;
    #endregion

    void Start()
    {
        levelManager = ObjectManager.LevelManager;
        SetSpecialProperties();
        SetGeneralProperties();
        SetPositiveNegativeDoors(negativeTextBGMat, negativeDoorBGMat, positiveTextBGMat, positiveDoorBGMat);
        addBulletSize = SetInitialValues(0, 150, (int)addBulletSize);
    }

    private void OnEnable()
    {
    }

    private void SetSpecialProperties()
    {
        doorType = DoorType.BulletSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            tempBulletController = other.GetComponent<BulletController>();
            //SetBulletSize
            addBulletSize += tempBulletController.hitValue;
            valueText.text = addBulletSize.ToString();
            ValueTextAnim();

            //SetColor
            if (addBulletSize >= 0)
                SetPositiveColor(positiveTextBGMat, positiveDoorBGMat);

        }

        if (other.CompareTag("Player"))
        {
            CloseCollider();

            //Change BulletSize
            levelManager.bulletSize += (addBulletSize / 1000);
          
            //Set  BulletSize Border
            if (levelManager.bulletSize < minBulletSize)
                levelManager.bulletSize = minBulletSize;
            else if (levelManager.bulletSize > maxBulletSize)
                levelManager.bulletSize = maxBulletSize;
        }
    }
}
