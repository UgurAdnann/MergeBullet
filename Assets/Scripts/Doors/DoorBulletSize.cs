using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBulletSize : DoorController
{
    public float addBulletSize;
    public Material positiveTextBGMat, negativeTextBGMat;
    public Material positiveDoorBGMat, negativeDoorBGMat;
    void Start()
    {
        SetSpecialProperties();
        SetGeneralProperties();
        SetPositiveNegativeDoors(negativeTextBGMat, negativeDoorBGMat, positiveTextBGMat, positiveDoorBGMat);
        addBulletSize= SetInitialValues(0,150, (int)addBulletSize);
    }

    private void SetSpecialProperties()
    {
        doorType = DoorType.BulletSize;
    }
}
