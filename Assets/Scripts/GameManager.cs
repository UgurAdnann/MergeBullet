using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private void Awake()
    {
        ObjectManager.GameManager = this;
    }

    public void Merge(BulletController firstBullet, BulletController secondBullet)
    {
        firstBullet.ResetGrid();
        // Create merge bullet
        //Place merge bullet
        //Set bullet Settings
        //Set Grid Settings
        //Destroy old bullets
    }
}
