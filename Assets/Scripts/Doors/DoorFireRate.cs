using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFireRate : DoorController
{
    public float addFireRate;
    public Material positiveTextBGMat, negativeTextBGMat;
    public Material positiveDoorBGMat, negativeDoorBGMat;

    void Start()
    {
        SetSpecialProperties();
        SetGeneralProperties();
        SetPositiveNegativeDoors(negativeTextBGMat, negativeDoorBGMat, positiveTextBGMat, positiveDoorBGMat);
        addFireRate= SetInitialValues(0,150, (int)addFireRate);
    }

    private void SetSpecialProperties()
    {
        doorType = DoorType.FireRate;
    }
}
