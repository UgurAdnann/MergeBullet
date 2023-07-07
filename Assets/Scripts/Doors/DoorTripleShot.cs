using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTripleShot : DoorController
{

    void Start()
    {
        SetSpecialProperties();
        SetGeneralProperties();
    }

    private void SetSpecialProperties()
    {
        doorType = DoorType.TripleShot;
    }
}
