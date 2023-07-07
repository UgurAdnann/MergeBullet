using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFireRate : DoorController
{
    #region Variables for General
    private LevelManager levelManager;
    #endregion

    #region Variables for Properties
    public float addFireRate,minFireRate,maxFireRate;
    public Material positiveTextBGMat, negativeTextBGMat;
    public Material positiveDoorBGMat, negativeDoorBGMat;
    #endregion

    void Start()
    {
        levelManager = ObjectManager.LevelManager;
        SetSpecialProperties();
        SetGeneralProperties();
        SetPositiveNegativeDoors(negativeTextBGMat, negativeDoorBGMat, positiveTextBGMat, positiveDoorBGMat);
        addFireRate= SetInitialValues(0,150, (int)addFireRate);
    }

    private void SetSpecialProperties()
    {
        doorType = DoorType.FireRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            tempBulletController = other.GetComponent<BulletController>();
            tempBulletController.ReplaceQue();
           
            //SetFireRate
            addFireRate += tempBulletController.hitValue;
            valueText.text = addFireRate.ToString();
            ValueTextAnim();

            //SetColor
            if (addFireRate >= 0)
                SetPositiveColor(positiveTextBGMat, positiveDoorBGMat);

        }

        if (other.CompareTag("Player"))
        {
            CloseCollider();

            //Change BulletRange
            levelManager.fireRate -=  (addFireRate / 1000);
           
            //Set Border FireRate
            if (levelManager.fireRate < minFireRate)
                levelManager.fireRate = minFireRate;
            else if(levelManager.fireRate > maxFireRate)
                levelManager.fireRate = maxFireRate;
        }
    }
}
