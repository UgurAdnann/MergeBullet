using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBulletRange : DoorController
{
    public int addRange, minRange;
    public Material positiveTextBGMat, negativeTextBGMat;
    public Material positiveDoorBGMat, negativeDoorBGMat;
    private BulletRangeSettings bulletRangeSettings;



    void Start()
    {
        bulletRangeSettings = ObjectManager.BulletRangeSettings;
        SetSpecialProperties();
        SetGeneralProperties();
        SetPositiveNegativeDoors(negativeTextBGMat, negativeDoorBGMat, positiveTextBGMat, positiveDoorBGMat);
        addRange = SetInitialValues(0, 150, addRange);
    }

    private void SetSpecialProperties()
    {
        doorType = DoorType.BulletRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            tempBulletController = other.GetComponent<BulletController>();
            //SetRange
            addRange += tempBulletController.hitValue;
            valueText.text = addRange.ToString();
            ValueTextAnim();

            //SetColor
            if (addRange >= 0)
                SetPositiveColor(positiveTextBGMat, positiveDoorBGMat);

        }

        if (other.CompareTag("Player"))
        {
            print("Player");
            //Change BulletRange
            bulletRangeSettings.transform.localPosition += Vector3.forward * (addRange / 10);
            //Set Min Range
            if (bulletRangeSettings.transform.localPosition.z < minRange)
                bulletRangeSettings.transform.localPosition = new Vector3(0, 0, minRange);
        }
    }
}
